//-----------------------------------------------------------------------
// <copyright file="ServerClient.cs" company="FH Wiener Neustadt">
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
    using System.Net.Sockets;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Timers;

    /// <summary>
    /// The <see cref="ServerClient"/> class.
    /// </summary>
    public class ServerClient
    {
        /// <summary>
        /// Represents the client id.
        /// </summary>
        public readonly int ClientId;

        /// <summary>
        /// Represents the receive message thread.
        /// </summary>
        private Thread thread;

        /// <summary>
        /// Represents the client.
        /// </summary>
        private TcpClient tcpClient;

        /// <summary>
        /// Represents the network stream.
        /// </summary>
        private NetworkStream stream;

        /// <summary>
        /// Represents the message builder.
        /// </summary>
        private MessageBuilder messageBuilder;

        /// <summary>
        /// Represents the timeout timer.
        /// </summary>
        private System.Timers.Timer timeoutTimer;

        /// <summary>
        /// Represents the ping timer.
        /// </summary>
        private System.Timers.Timer pingTimer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerClient"/> class.
        /// </summary>
        /// <param name="client"> The <see cref="TcpClient"/>. </param>
        /// <param name="clientID"> The client id. </param>
        public ServerClient(TcpClient client, int clientID)
        {
            this.tcpClient = client;
            this.stream = this.tcpClient.GetStream();
            this.ClientId = clientID;
            this.messageBuilder = new MessageBuilder();
            this.timeoutTimer = new System.Timers.Timer(10000);
            this.pingTimer = new System.Timers.Timer(5000);
            this.OnMessageReceived += this.messageBuilder.BuildMessage;
            this.messageBuilder.OnMessageCompleted += this.CheckCompletedMessage;
            this.timeoutTimer.Elapsed += this.OnTimeout;
            this.pingTimer.Elapsed += this.SendPing;
        }

        /// <summary>
        /// This event fires after a message has been received.
        /// </summary>
        public event EventHandler<ByteMessageEventArgs> OnMessageReceived;

        /// <summary>
        /// This event fires after a snake movement has been received.
        /// </summary>
        public event EventHandler<SnakeMoveEventArgs> OnSnakeMovementReceived;

        /// <summary>
        /// This event fires after a client has been disconnected.
        /// </summary>
        public event EventHandler<ClientIDEventArgs> OnClientDisconnect;

        /// <summary>
        /// This event fires after a ping has been received.
        /// </summary>
        public event EventHandler OnPingReceived;

        /// <summary>
        /// Gets or sets the number of strikes.
        /// </summary>
        /// <value> A normal integer. </value>
        public int Strikes
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a value indicating whether the client is still alive.
        /// </summary>
        /// <value> A normal boolean. </value>
        public bool IsAlive
        {
            get;
            private set;    
        }

        /// <summary>
        /// Gets a value indicating whether the client is still connected.
        /// </summary>
        /// <value> A normal boolean. </value>
        public bool IsConnectionClosed
        {
            get;
            private set;
        }

        /// <summary>
        /// This method closes the connection on timeout.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="ElapsedEventArgs"/>. </param>
        public void OnTimeout(object sender, ElapsedEventArgs e)
        {
            this.CloseConnection();
        }

        /// <summary>
        /// This method starts the client.
        /// </summary>
        public void Start()
        {
            if (this.thread != null && this.thread.IsAlive)
            {
                throw new ArgumentException("Error thread is already running.");
            }

            this.thread = new Thread(this.ReceiverWorker);
            this.thread.IsBackground = true;
            this.IsAlive = true;
            this.timeoutTimer.Start();
            this.pingTimer.Start();
            this.thread.Start();
        }

        /// <summary>
        /// This method stops the client.
        /// </summary>
        public void Stop()
        {
            if (this.thread == null || !this.thread.IsAlive)
            {
                throw new ArgumentException("Error thread is dead.");
            }

            this.IsAlive = false;
            this.thread.Join();
            this.timeoutTimer.Close();
            this.pingTimer.Close();
            this.FireOnClientDisconnected(new ClientIDEventArgs(this.ClientId));
        }

        /// <summary>
        /// This method closes the connection.
        /// </summary>
        public void CloseConnection()
        {
            this.Stop();
            this.stream.Close();
            this.tcpClient.Close();
            this.IsConnectionClosed = true;
        }

        /// <summary>
        /// This method sends a message.
        /// </summary>
        /// <param name="message"> The message to send. </param>
        /// <returns> It returns a <see cref="Task{TResult}"/>. </returns>
        public async Task<bool> SendMessage(byte[] message)
        {
            try
            {
                if (this.tcpClient == null || !this.tcpClient.Connected || this.tcpClient.GetStream() == null)
                {
                    throw new ArgumentException("Error");
                }
                else
                {
                    this.stream = this.tcpClient.GetStream();
                    await this.stream.WriteAsync(message, 0, message.Length);
                    this.ResetPingTimer();
                    return true;
                }
            }
            catch (Exception)
            {
                this.Strikes++;
                return false;
            }
        }

        /// <summary>
        /// This method fires the <see cref="OnMessageReceived"/> event.
        /// </summary>
        /// <param name="e"> The <see cref="ByteMessageEventArgs"/>. </param>
        protected virtual void FireOnMessageReceived(ByteMessageEventArgs e)
        {
            if (this.OnMessageReceived != null)
            {
                this.OnMessageReceived(this, e);
            }
        }

        /// <summary>
        /// This method fires the <see cref="OnSnakeMovementReceived"/> event.
        /// </summary>
        /// <param name="e"> The <see cref="SnakeMoveEventArgs"/>. </param>
        protected virtual void FireOnSnakeMovementReceived(SnakeMoveEventArgs e)
        {
            if (this.OnSnakeMovementReceived != null)
            {
                this.OnSnakeMovementReceived(this, e);
            }
        }

        /// <summary>
        /// This method fires the <see cref="OnClientDisconnect"/> event.
        /// </summary>
        /// <param name="e"> The <see cref="ClientIDEventArgs"/>. </param>
        protected virtual void FireOnClientDisconnected(ClientIDEventArgs e)
        {
            if (this.OnClientDisconnect != null)
            {
                this.OnClientDisconnect(this, e);
            }
        }

        /// <summary>
        /// This method fires the <see cref="OnPingReceived"/> event.
        /// </summary>
        protected virtual void FireOnPingReceived()
        {
            this.OnPingReceived?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method sends a ping after the timer ran out.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="ElapsedEventArgs"/>. </param>
        private async void SendPing(object sender, ElapsedEventArgs e)
        {
            this.ResetPingTimer();
            await this.SendMessage(NetworkSerealizer.SerealizePing());
        }

        /// <summary>
        /// This method resets the ping timer.
        /// </summary>
        private void ResetPingTimer()
        {
            this.pingTimer.Stop();
            this.pingTimer.Start();
        }

        /// <summary>
        /// This method receives a byte message.
        /// </summary>
        private void ReceiverWorker()
        {
            if (this.tcpClient == null || this.tcpClient.GetStream() == null)
            {
                throw new ArgumentNullException("Error client is null.");
            }

            this.stream = this.tcpClient.GetStream();

            while (this.IsAlive)
            {
                if (this.tcpClient == null)
                {
                    throw new ArgumentNullException("Error client is null.");
                }

                if (this.tcpClient.Available == 0)
                {
                    Thread.Sleep(100);
                    continue;
                }
                else
                {
                    byte[] searchBuffer = new byte[8192];
                    try
                    {
                        this.stream.Read(searchBuffer, 0, searchBuffer.Length);
                        this.FireOnMessageReceived(new ByteMessageEventArgs(searchBuffer));
                    }
                    catch (Exception ex)
                    {
                        this.IsAlive = false;
                        this.IsConnectionClosed = true;
                    }
                }
            }
        }

        /// <summary>
        /// This method checks the completed message.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="ByteMessageEventArgs"/>. </param>
        private void CheckCompletedMessage(object sender, ByteMessageEventArgs e)
        {
            MessageTypePing ping = new MessageTypePing();

            MessageTypeMoveSnake typeMoveSnake = new MessageTypeMoveSnake();

            if (e.Message[0] == (byte)ping.Id)
            {
                this.ResetTimer();
                this.FireOnPingReceived();
            }
            else if (e.Message[0] == (byte)typeMoveSnake.Id)
            {
                List<byte> message = e.Message.ToList();
                this.RemoveUnusedBytes(message);

                MoveSnakeContainer sn = NetworkDeSerealizer.DeSerealizeSnakeMovement(message.ToArray());
                this.ResetTimer();
                this.FireOnSnakeMovementReceived(new SnakeMoveEventArgs(sn, this.ClientId));
            }
        }

        /// <summary>
        /// This method removes the unused bytes.
        /// </summary>
        /// <param name="list"> The list of bytes. </param>
        /// <returns> It returns a list with the unused bytes removed. </returns>
        private List<byte> RemoveUnusedBytes(List<byte> list)
        {
            List<byte> hostname = new List<byte>();

            list.RemoveAt(0);
            int hostNameLength = list[0];

            for (int i = 0; i < hostNameLength; i++)
            {
                hostname.Add(list[i + 1]);
            }

            list.RemoveRange(0, hostname.Count + 1);

            return list;
        }

        /// <summary>
        /// This method resets the timeout timer.
        /// </summary>
        private void ResetTimer()
        {
            this.timeoutTimer.Stop();
            this.timeoutTimer.Start();
        }
    }
}