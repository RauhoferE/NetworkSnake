using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Timers;

namespace NetworkLibrary
{
    using System.ComponentModel;
    using System.Threading.Tasks;

    public class ServerClient
    {
        private Thread thread;
        private TcpClient tcpClient;
        private NetworkStream stream;
        private MessageBuilder messageBuilder;
        private System.Timers.Timer timeoutTimer;
        private System.Timers.Timer pingTimer;
        public readonly int clientId;

        public ServerClient(TcpClient client, int clientID)
        {
            this.tcpClient = client;
            this.stream = this.tcpClient.GetStream();
            this.clientId = clientID;
            this.messageBuilder = new MessageBuilder();
            this.timeoutTimer = new System.Timers.Timer(10000);
            this.pingTimer = new System.Timers.Timer(5000);
            this.OnMessageReceived += this.messageBuilder.BuildMessage;
            this.messageBuilder.OnMessageCompleted += this.CheckCompletedMessage;
            this.timeoutTimer.Elapsed += this.OnTimeout;
            this.pingTimer.Elapsed += this.SendPing;
        }

        private async void SendPing(object sender, ElapsedEventArgs e)
        { 
            this.ResetPingTimer();
            await this.SendMessage(NetworkSerealizer.SerealizePing());
        }

        private void ResetPingTimer()
        {
            this.pingTimer.Stop();
            this.pingTimer.Start();
        }

        public event EventHandler<ByteMessageEventArgs> OnMessageReceived;

        public event EventHandler<SnakeMoveEventArgs> OnSnakeMovementReceived;

        public event EventHandler<ClientIDEventArgs> OnClientDisconnect;

        public event EventHandler OnPingReceived;

        public int Strikes
        {
            get;
            set;
        }

        public bool IsAlive
        {
            get;
            private set;    
        }

        public bool IsConnectionClosed
        {
            get;
            private set;
        }

        public void OnTimeout(object sender, ElapsedEventArgs e)
        {
            this.CloseConnection();
        }

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
            this.FireOnClientDisconnected(new ClientIDEventArgs(this.clientId));
        }

        public void ReceiverWorker()
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

        public void CloseConnection()
        {
            this.Stop();
            this.stream.Close();
            this.tcpClient.Close();
            this.IsConnectionClosed = true;
        }

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
            catch (Exception e)
            {
                this.Strikes++;
                return false;
            }
        }

        protected virtual void FireOnMessageReceived(ByteMessageEventArgs e)
        {
            if (this.OnMessageReceived != null)
            {
                this.OnMessageReceived(this, e);
            }
        }

        protected virtual void FireOnSnakeMovementReceived(SnakeMoveEventArgs e)
        {
            if (this.OnSnakeMovementReceived != null)
            {
                this.OnSnakeMovementReceived(this, e);
            }
        }

        protected virtual void FireOnClientDisconnected(ClientIDEventArgs e)
        {
            if (this.OnClientDisconnect != null)
            {
                this.OnClientDisconnect(this, e);
            }
        }

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
                this.FireOnSnakeMovementReceived(new SnakeMoveEventArgs(sn, this.clientId));
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

        private void ResetTimer()
        {
            this.timeoutTimer.Stop();
            this.timeoutTimer.Start();
        }

        protected virtual void FireOnPingReceived()
        {
            OnPingReceived?.Invoke(this, EventArgs.Empty);
        }
    }
}