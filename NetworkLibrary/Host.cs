﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetworkLibrary
{
    using System.ComponentModel;

    public class ServerHost
    {
        /// <summary>
        /// The listener for the clients.
        /// </summary>
        private TcpListener listener;

        /// <summary>
        /// The thread for accepting the clients.
        /// </summary>
        private Thread thread;

        /// <summary>
        /// The list of clients.
        /// </summary>
        private List<ServerClient> clients;

        private object locker;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerHost"/> class.
        /// </summary>
        /// <param name="port"> The number of the port. </param>
        public ServerHost(int port)
        {
            this.clients = new List<ServerClient>();
            this.listener = new TcpListener(IPAddress.Any, port);
            this.IsRunning = false;
            this.locker = new object();
        }

        public event EventHandler<ClientIDEventArgs> OnClientDisconnect;

        public event EventHandler<SnakeMoveEventArgs> OnSnakeMovementReceived;

        public event EventHandler OnNoClientConnected;

        public event EventHandler OnPingReceived;

        /// <summary>
        /// The event for when a new client has been connected.
        /// </summary>
        public event EventHandler<ClientIDEventArgs> OnClientConnected;

        /// <summary>
        /// Gets or sets a value indicating whether the listener is running or not.
        /// </summary>
        /// <value> Is true if the host is running. </value>
        public bool IsRunning
        {
            get;
            set;
        }

        /// <summary>
        /// This method starts the host and the thread.
        /// </summary>
        public void Start()
        {
            if (this.thread != null && this.thread.IsAlive)
            {
                throw new ArgumentException("Error thread is already running.");
            }

            this.listener.Start();
            this.IsRunning = true;
            this.thread = new Thread(this.AcceptClients);
            this.thread.Start();
        }

        /// <summary>
        /// This method sends the byte message to all the clients.
        /// </summary>
        /// <param name="message"> The byte message. </param>
        public void SendToClients(byte[] message)
        {
            lock (locker)
            {
                if (!this.CheckIfClientsAreConnected())
                {
                    return;
                }
                

                bool areClosedConnectionsInList = false;
                foreach (var item in this.clients)
                {
                    if (item.IsConnectionClosed)
                    {
                        item.Strikes = 2;
                    }

                    for (int i = item.Strikes; i < 2; i++)
                    {
                        if (item.SendMessage(message).Result)
                        {
                            item.Strikes = 0;
                            break;
                        }
                    }

                    if (item.Strikes >= 2)
                    {
                        areClosedConnectionsInList = true;

                        try
                        {
                            item.CloseConnection();
                        }
                        catch (Exception e)
                        {
                        }

                    }
                }

                if (areClosedConnectionsInList)
                {
                    this.RemoveClosedConnectionsFromList();
                }
            }
        }

        private bool CheckIfClientsAreConnected()
        {
            lock (locker)
            {
                if (this.clients.Count == 0)
                {
                    this.FireOnNoClientConnected();
                    return false;
                }

                return true;
            }
        }

        public void SendToClient(byte[] message, int clientId)
        {
            lock (locker)
            {
                if (!this.CheckIfClientsAreConnected())
                {
                    return;
                }

                bool areClosedConnectionsInList = false;

                if (this.clients.Where(x => x.clientId == clientId).FirstOrDefault() != null)
                {
                    var client = this.clients.Where(x => x.clientId == clientId).FirstOrDefault();
                    if (client.IsConnectionClosed)
                    {
                        client.Strikes = 2;
                    }

                    for (int i = client.Strikes; i < 2; i++)
                    {
                        if (client.SendMessage(message).Result)
                        {
                            client.Strikes = 0;
                            break;
                        }
                    }

                    if (client.Strikes >= 2)
                    {
                        try
                        {
                            areClosedConnectionsInList = true;
                            client.CloseConnection();
                        }
                        catch (Exception e)
                        {
                        }
                        
                    }

                    if (areClosedConnectionsInList)
                    {
                        this.RemoveClosedConnectionsFromList();
                    }
                    
                }
            }

        }

        public void ReceiveFromClient(object sender, SnakeMoveEventArgs e)
        {
            this.FireOnClientSnakeMovementReceived(e);
        }

        public void ReceivePingFromClient(object sender, EventArgs e)
        {
            this.FireOnPingReceived();
        }

        public void ClientDisconnected(object sender, ClientIDEventArgs e)
        {
            this.FireOnClientDisconnect(e);
        }

        /// <summary>
        /// This method stops the host.
        /// </summary>
        public void Stop()
        {
            if (this.thread == null || !this.thread.IsAlive)
            {
                throw new ArgumentException("Error thread is already running.");
            }

            this.listener.Stop();
            this.IsRunning = false;
            this.thread.Join();

            foreach (var item in this.clients)
            {
                item.CloseConnection();
            }

            this.clients.Clear();
        }

        /// <summary>
        /// This method fires when a client has been connected.
        /// </summary>
        protected virtual void FireOnClientConnected(ClientIDEventArgs e)
        {
            if (this.OnClientConnected != null)
            {
                this.OnClientConnected(this, e);
            }
        }

        protected virtual void FireOnClientDisconnect(ClientIDEventArgs e)
        {
            if (this.OnClientDisconnect != null)
            {
                this.OnClientDisconnect(this, e);
            }
        }

        protected virtual void FireOnClientSnakeMovementReceived(SnakeMoveEventArgs e)
        {
            if (this.OnSnakeMovementReceived != null)
            {
                this.OnSnakeMovementReceived(this, e);
            }
        }

        /// <summary>
        /// This method accepts the clients.
        /// </summary>
        private void AcceptClients()
        {
            int currentID = 0;

            while (this.IsRunning == true)
            {
                ServerClient client = new ServerClient(this.listener.AcceptTcpClient(), currentID);
                client.OnClientDisconnect += this.ClientDisconnected;
                client.OnSnakeMovementReceived += this.ReceiveFromClient;
                client.OnPingReceived += this.ReceivePingFromClient;
                client.Start();

                lock (locker)
                {
                    this.clients.Add(client);
                }
                
                this.FireOnClientConnected(new ClientIDEventArgs(currentID));
                currentID++;


            }
        }

        /// <summary>
        /// This method removes the closed connections from the list.
        /// </summary>
        private void RemoveClosedConnectionsFromList()
        {
            List<ServerClient> oldClients = new List<ServerClient>();

            lock (locker)
            {
                foreach (var item in this.clients)
                {
                    oldClients.Add(item);
                }
            }

            lock (locker)
            {
                foreach (var item in oldClients)
                {
                    if (item.IsConnectionClosed)
                    {
                        this.clients.Remove(item);
                    }
                }
            }

        }

        protected virtual void FireOnNoClientConnected()
        {
            OnNoClientConnected?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void FireOnPingReceived()
        {
            OnPingReceived?.Invoke(this, EventArgs.Empty);
        }
    }
}