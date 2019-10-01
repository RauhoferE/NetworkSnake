//-----------------------------------------------------------------------
// <copyright file="ServerHost.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This is a network library.
// </summary>
//-----------------------------------------------------------------------
namespace NetworkLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    /// <summary>
    /// The <see cref="ServerHost"/> class.
    /// </summary>
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

        /// <summary>
        /// The locker object.
        /// </summary>
        private object locker;

        /// <summary>
        /// Represents the current number of connected clients.
        /// </summary>
        private int currentID;

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
            this.currentID = 0;
        }

        /// <summary>
        /// This event fires when a client has been disconnected.
        /// </summary>
        public event EventHandler<ClientIDEventArgs> OnClientDisconnect;

        /// <summary>
        /// This event fires when a snake movement has been received.
        /// </summary>
        public event EventHandler<SnakeMoveEventArgs> OnSnakeMovementReceived;

        /// <summary>
        /// This event fires when no client has been connected.
        /// </summary>
        public event EventHandler OnNoClientConnected;

        /// <summary>
        /// This event fires when a ping has been received.
        /// </summary>
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
            lock (this.locker)
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
                        catch (Exception)
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

        /// <summary>
        /// This method send the message to the client with the id.
        /// </summary>
        /// <param name="message"> The byte message. </param>
        /// <param name="clientId"> The client id. </param>
        public void SendToClient(byte[] message, int clientId)
        {
            lock (this.locker)
            {
                if (!this.CheckIfClientsAreConnected())
                {
                    return;
                }

                bool areClosedConnectionsInList = false;

                if (this.clients.Where(x => x.ClientId == clientId).FirstOrDefault() != null)
                {
                    var client = this.clients.Where(x => x.ClientId == clientId).FirstOrDefault();
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
                        areClosedConnectionsInList = true;

                        try
                        {
                            client.CloseConnection();
                        }
                        catch (Exception)
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
        /// <param name="e"> The <see cref="ClientIDEventArgs"/>. </param>
        protected virtual void FireOnClientConnected(ClientIDEventArgs e)
        {
            if (this.OnClientConnected != null)
            {
                this.OnClientConnected(this, e);
            }
        }

        /// <summary>
        /// This method fires when a client has been disconnected.
        /// </summary>
        /// <param name="e"> The <see cref="ClientIDEventArgs"/>. </param>
        protected virtual void FireOnClientDisconnect(ClientIDEventArgs e)
        {
            if (this.OnClientDisconnect != null)
            {
                this.OnClientDisconnect(this, e);
            }
        }

        /// <summary>
        /// This method fires when snake movement has been received.
        /// </summary>
        /// <param name="e"> The <see cref="SnakeMoveEventArgs"/>. </param>
        protected virtual void FireOnClientSnakeMovementReceived(SnakeMoveEventArgs e)
        {
            if (this.OnSnakeMovementReceived != null)
            {
                this.OnSnakeMovementReceived(this, e);
            }
        }

        /// <summary>
        /// This method fires when no client is connected.
        /// </summary>
        protected virtual void FireOnNoClientConnected()
        {
            this.OnNoClientConnected?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires when a ping has been received.
        /// </summary>
        protected virtual void FireOnPingReceived()
        {
            this.OnPingReceived?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method checks if any clients are still connected.
        /// </summary>
        /// <returns> It returns true if the clients are still connected. </returns>
        private bool CheckIfClientsAreConnected()
        {
            lock (this.locker)
            {
                if (this.clients.Count == 0)
                {
                    this.FireOnNoClientConnected();
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// This method fires when a snake movement from a client has been received.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="SnakeMoveEventArgs"/>. </param>
        private void ReceiveFromClient(object sender, SnakeMoveEventArgs e)
        {
            this.FireOnClientSnakeMovementReceived(e);
        }

        /// <summary>
        /// This method fires when a ping from a client has been received.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="SnakeMoveEventArgs"/>. </param>
        private void ReceivePingFromClient(object sender, EventArgs e)
        {
            this.FireOnPingReceived();
        }

        /// <summary>
        /// This method fires when a client has been disconnected.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="ClientIDEventArgs"/>. </param>
        private void ClientDisconnected(object sender, ClientIDEventArgs e)
        {
            this.FireOnClientDisconnect(e);
        }

        /// <summary>
        /// This method accepts the clients.
        /// </summary>
        private void AcceptClients()
        {
            while (this.IsRunning == true)
            {
                ServerClient client = new ServerClient(this.listener.AcceptTcpClient(), this.currentID);
                client.OnClientDisconnect += this.ClientDisconnected;
                client.OnSnakeMovementReceived += this.ReceiveFromClient;
                client.OnPingReceived += this.ReceivePingFromClient;
                client.Start();

                lock (this.locker)
                {
                    this.clients.Add(client);
                }
                
                this.FireOnClientConnected(new ClientIDEventArgs(this.currentID));
                this.currentID++;
            }
        }

        /// <summary>
        /// This method removes the closed connections from the list.
        /// </summary>
        private void RemoveClosedConnectionsFromList()
        {
            List<ServerClient> oldClients = new List<ServerClient>();

            lock (this.locker)
            {
                foreach (var item in this.clients)
                {
                    oldClients.Add(item);
                }
            }

            lock (this.locker)
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
    }
}