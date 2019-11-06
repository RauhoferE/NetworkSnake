//-----------------------------------------------------------------------
// <copyright file="PlayerClient.cs" company="FH Wiener Neustadt">
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
    using System.Timers;

    /// <summary>
    /// The <see cref="PlayerClient"/> class.
    /// </summary>
    public class PlayerClient
    {
        /// <summary>
        /// Represents the receive thread.
        /// </summary>
        private Thread thread;

        /// <summary>
        /// The endpoint.
        /// </summary>
        private IPEndPoint ipAdress;

        /// <summary>
        /// The message builder.
        /// </summary>
        private MessageBuilder messageBuilder;

        /// <summary>
        /// The networks stream.
        /// </summary>
        private NetworkStream stream;

        /// <summary>
        /// The client.
        /// </summary>
        private TcpClient tcpClient;

        /// <summary>
        /// The ping send timer.
        /// </summary>
        private System.Timers.Timer automaticPingSenderTimer;

        /// <summary>
        /// The server timer.
        /// </summary>
        private System.Timers.Timer serverTimer;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerClient"/> class.
        /// </summary>
        /// <param name="ipAdress"> The <see cref="IPEndPoint"/>. </param>
        public PlayerClient(IPEndPoint ipAdress)
        {
            this.ipAdress = ipAdress;
            this.tcpClient = new TcpClient();
            this.messageBuilder = new MessageBuilder();
            this.automaticPingSenderTimer = new System.Timers.Timer(5000);
            this.serverTimer = new System.Timers.Timer(10000);
            this.OnMessageReceived += this.messageBuilder.BuildMessage;
            this.messageBuilder.OnMessageCompleted += this.CheckCompletedMessage;
            this.automaticPingSenderTimer.Elapsed += this.OnTimeout;
            this.serverTimer.Elapsed += this.AutomaticClientClose;
        }

        /// <summary>
        /// This event should fire when a message has been received.
        /// </summary>
        public event EventHandler<ByteMessageEventArgs> OnMessageReceived;

        /// <summary>
        /// This event should fire when a field message has been received.
        /// </summary>
        public event EventHandler<FieldMessageEventArgs> OnFieldMessageReceived;

        /// <summary>
        /// This event should fire when a object list has been received.
        /// </summary>
        public event EventHandler<ObjectPrintEventArgs> OnObjectListReceived;

        /// <summary>
        /// This event should fire when a error message has been received.
        /// </summary>
        public event EventHandler<MessageContainerEventArgs> OnErrorMessageReceived;

        /// <summary>
        /// This event should fire when a text message has been received.
        /// </summary>
        public event EventHandler<MessageContainerEventArgs> OnNormalTextReceived;

        /// <summary>
        /// This event should fire when the server disconnects.
        /// </summary>
        public event EventHandler OnServerDisconnect;

        /// <summary>
        /// Gets a value indicating whether the connection is alive.
        /// </summary>
        /// <value> A normal boolean. </value>
        public bool IsAlive
        {
            get;
            private set;
        }

        /// <summary>
        /// Starts the client.
        /// </summary>
        public void Start()
        {
            if (this.thread != null && this.thread.IsAlive)
            {
                throw new ArgumentException("Error thread is already running.");
            }

            try
            {
                this.tcpClient.Connect(this.ipAdress);
            }
            catch (Exception)
            {
                throw new ArgumentException("Error cant connect.");
            }

            this.IsAlive = true;
            this.automaticPingSenderTimer.Start();
            this.serverTimer.Start();
            this.thread = new Thread(this.Worker);
            this.thread.IsBackground = true;
            this.thread.Start();
        }

        /// <summary>
        /// Stops the client.
        /// </summary>
        public void Stop()
        {
            if (this.thread == null || !this.thread.IsAlive)
            {
                throw new ArgumentException("Error thread is already dead. ");
            }

            this.automaticPingSenderTimer.Close();
            this.serverTimer.Close();
            this.IsAlive = false;
            this.thread.Join();
            this.tcpClient.Close();
            this.stream.Close();
            this.tcpClient = new TcpClient();
        }

        /// <summary>
        /// Sends a message to the server.
        /// </summary>
        /// <param name="message"> The message. </param>
        public void SendMessage(byte[] message)
        {
            this.ResetTimer();

            if (this.tcpClient == null || !this.tcpClient.GetStream().CanWrite || !this.tcpClient.Connected)
            {
                throw new ArgumentException("Error");
            }
            else
            {
                try
                {
                    this.stream = this.tcpClient.GetStream();
                    this.stream.Write(message, 0, message.Length);
                }
                catch (Exception)
                {
                    this.FireServerDisconnect();
                }
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
        /// This method fires the <see cref="OnFieldMessageReceived"/> event.
        /// </summary>
        /// <param name="e"> The <see cref="FieldMessageEventArgs"/>. </param>
        protected virtual void FireOnFieldMessageReceived(FieldMessageEventArgs e)
        {
            if (this.OnFieldMessageReceived != null)
            {
                this.OnFieldMessageReceived(this, e);
            }
        }

        /// <summary>
        /// This method fires the <see cref="OnObjectListReceived"/> event.
        /// </summary>
        /// <param name="e"> The <see cref="ObjectPrintContainer"/>. </param>
        protected virtual void FireOnObjectListReceived(ObjectPrintEventArgs e)
        {
            if (this.OnObjectListReceived != null)
            {
                this.OnObjectListReceived(this, e);
            }
        }

        /// <summary>
        /// This method fires the <see cref="OnNormalTextReceived"/> event.
        /// </summary>
        /// <param name="e"> The <see cref="MessageContainerEventArgs"/>. </param>
        protected virtual void FireNormalMessageReceived(MessageContainerEventArgs e)
        {
            if (this.OnNormalTextReceived != null)
            {
                this.OnNormalTextReceived(this, e);
            }
        }

        /// <summary>
        /// This method fires the <see cref="OnErrorMessageReceived"/> event.
        /// </summary>
        /// <param name="e"> The <see cref="MessageContainerEventArgs"/>. </param>
        protected virtual void FireErrorMessageReceived(MessageContainerEventArgs e)
        {
            if (this.OnErrorMessageReceived != null)
            {
                this.OnErrorMessageReceived(this, e);
            }
        }

        /// <summary>
        /// This method fires the <see cref="OnServerDisconnect"/> event.
        /// </summary>
        protected virtual void FireServerDisconnect()
        {
            this.OnServerDisconnect?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Starts the client.
        /// </summary>
        private void Worker()
        {
            if (this.tcpClient == null || this.tcpClient.GetStream() == null)
            {
                this.FireErrorMessageReceived(new MessageContainerEventArgs(new MessageContainer("Error client is null.")));
            }

            this.stream = this.tcpClient.GetStream();

            while (this.IsAlive)
            {
                if (this.tcpClient == null)
                {
                    this.FireErrorMessageReceived(new MessageContainerEventArgs(new MessageContainer("Error client is null.")));
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
                        this.FireErrorMessageReceived(new MessageContainerEventArgs(new MessageContainer("Error Message couldnt be received. Please try to reconnect with the server.")));
                    }
                }
            }
        }

        /// <summary>
        /// This method resets the ping timer.
        /// </summary>
        private void ResetTimer()
        {
            this.automaticPingSenderTimer.Stop();
            this.automaticPingSenderTimer.Start();
        }

        /// <summary>
        /// This method resets the server timer.
        /// </summary>
        private void ResetServerTimer()
        {
            this.serverTimer.Stop();
            this.serverTimer.Start();
        }

        /// <summary>
        /// This method stops the client.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="ElapsedEventArgs"/>. </param>
        private void AutomaticClientClose(object sender, ElapsedEventArgs e)
        {
            this.Stop();
        }

        /// <summary>
        /// This method sends a ping.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="ElapsedEventArgs"/>. </param>
        private void OnTimeout(object sender, ElapsedEventArgs e)
        {
            this.ResetTimer();
            this.SendMessage(NetworkSerealizer.SerealizePing());
        }

        /// <summary>
        /// This method checks the completed message.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The byte event args. </param>
        private void CheckCompletedMessage(object sender, ByteMessageEventArgs e)
        {
            MessageTypePing pingType = new MessageTypePing();
            MessageTypePrintErrorMessage errorMessage = new MessageTypePrintErrorMessage();
            MessageTypePrintField fieldMessage = new MessageTypePrintField();
            MessageTypePrintObject printObject = new MessageTypePrintObject();
            MessageTypePrintString printMessage = new MessageTypePrintString();

            if (e.Message[0] == (byte)pingType.Id)
            {
                this.ResetServerTimer();
            }
            else if (e.Message[0] == (byte)errorMessage.Id)
            {
                List<byte> message = e.Message.ToList();
                this.RemoveUnusedBytes(message);
                MessageContainer messageContainer = NetworkDeSerealizer.DeSerealizedMessage(message.ToArray());
                this.ResetServerTimer();
                this.FireErrorMessageReceived(new MessageContainerEventArgs(messageContainer));
            }
            else if (e.Message[0] == (byte)fieldMessage.Id)
            {
                List<byte> message = e.Message.ToList();
                this.RemoveUnusedBytes(message);
                FieldPrintContainer container = NetworkDeSerealizer.DeSerealizedFieldMessage(message.ToArray());
                this.ResetServerTimer();
                this.FireOnFieldMessageReceived(new FieldMessageEventArgs(container));
            }
            else if (e.Message[0] == (byte)printObject.Id)
            {
                List<byte> message = e.Message.ToList();
                this.RemoveUnusedBytes(message);
                ObjectListContainer container = NetworkDeSerealizer.DeSerealizedObjectList(message.ToArray());
                this.ResetServerTimer();
                this.FireOnObjectListReceived(new ObjectPrintEventArgs(container));
            }
            else if (e.Message[0] == (byte)printMessage.Id)
            {
                List<byte> message = e.Message.ToList();
                this.RemoveUnusedBytes(message);
                MessageContainer messageContainer = NetworkDeSerealizer.DeSerealizedMessage(message.ToArray());
                this.ResetServerTimer();
                this.FireNormalMessageReceived(new MessageContainerEventArgs(messageContainer));
            }
        }

        /// <summary>
        /// This method removes the unused bytes from the message.
        /// </summary>
        /// <param name="list"> The byte message. </param>
        /// <returns> It returns a new byte message. </returns>
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
    }
}