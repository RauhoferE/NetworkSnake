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

        private System.Timers.Timer serverTimer;

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

        public event EventHandler<ByteMessageEventArgs> OnMessageReceived;

        public event EventHandler<FieldMessageEventArgs> OnFieldMessageReceived;

        public event EventHandler<ObjectPrintEventArgs> OnObjectListReceived;

        public event EventHandler<MessageContainerEventArgs> OnErrorMessageReceived;

        public event EventHandler<MessageContainerEventArgs> OnNormalTextReceived;

        public event EventHandler OnServerDisconnect;

        public bool IsAlive
        {
            get;
            private set;
        }

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

        public void Worker()
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
                        throw new ArgumentException("Error Message couldnt be received." + ex);
                    }
                }
            }
        }

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
                catch (Exception e)
                {
                    this.FireServerDisconnect();
                }
            }
        }

        protected virtual void FireOnMessageReceived(ByteMessageEventArgs e)
        {
            if (this.OnMessageReceived != null)
            {
                this.OnMessageReceived(this, e);
            }
        }

        protected virtual void FireOnFieldMessageReceived(FieldMessageEventArgs e)
        {
            if (this.OnFieldMessageReceived != null)
            {
                this.OnFieldMessageReceived(this, e);
            }
        }

        protected virtual void FireOnObjectListReceived(ObjectPrintEventArgs e)
        {
            if (this.OnObjectListReceived != null)
            {
                this.OnObjectListReceived(this, e);
            }
        }

        protected virtual void FireNormalMessageReceived(MessageContainerEventArgs e)
        {
            if (this.OnNormalTextReceived != null)
            {
                this.OnNormalTextReceived(this, e);
            }
        }

        protected virtual void FireErrorMessageReceived(MessageContainerEventArgs e)
        {
            if (this.OnErrorMessageReceived != null)
            {
                this.OnErrorMessageReceived(this, e);
            }
        }

        protected virtual void FireServerDisconnect()
        {
            OnServerDisconnect?.Invoke(this, EventArgs.Empty);
        }

        private void ResetTimer()
        {
            this.automaticPingSenderTimer.Stop();
            this.automaticPingSenderTimer.Start();
        }

        private void ResetServerTimer()
        {
            this.serverTimer.Stop();
            this.serverTimer.Start();
        }

        private void AutomaticClientClose(object sender, ElapsedEventArgs e)
        {
            this.Stop();
        }

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