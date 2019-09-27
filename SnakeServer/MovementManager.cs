using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeServer
{
    using System.Runtime.Remoting.Channels;
    using System.Threading;
    using NetworkLibrary;
    using Snake_V_0_3;

    public class MovementManager
    {
        private object locker;

        private Thread thread;
        private bool isRunning;

        public MovementManager()
        {
            this.MovementActions = new List<IInputType>();
            this.locker = new object();
        }

        public event System.EventHandler<DirectionEventArgs> OnSendMovement;

        public event EventHandler OnAnyKeyReceived;

        public List<IInputType> MovementActions
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

            this.isRunning = true;
            this.thread = new Thread(this.Worker);
            this.thread.Start();
        }

        public void Stop()
        {
            if (this.thread == null || !this.thread.IsAlive)
            {
                throw new ArgumentException("Error thread is already dead.");
            }

            this.isRunning = false;
            this.thread.Join();
        }

        private void Worker()
        {
            while (this.isRunning)
            {
                lock (this.locker)
                {
                    if (this.MovementActions.Count != 0)
                    {
                        if (this.MovementActions.FirstOrDefault() != null)
                        {
                            switch (this.MovementActions.FirstOrDefault().Id)
                            {
                                case 0:
                                    this.FireOnSendMovement(new DirectionEventArgs(new DirectionUp()));
                                    break;
                                case 1:
                                    this.FireOnSendMovement(new DirectionEventArgs(new DirectionDown()));
                                    break;
                                case 2:
                                    this.FireOnSendMovement(new DirectionEventArgs(new DirectionRight()));
                                    break;
                                case 3:
                                    this.FireOnSendMovement(new DirectionEventArgs(new DirectionLeft()));
                                    break;
                                    default:
                                    break;
                            }

                            this.MovementActions.RemoveAt(0);
                        }

                    }
                }

                Thread.Sleep(50);
            }
        }

        public void GetMovement(object sender, SnakeMoveEventArgs e)
        {
            lock (this.locker)
            {
                if (this.isRunning == false)
                {
                    this.FireOnAnyKeyReceived();
                }
                else
                {
                    this.MovementActions.Add(e.MoveSnakeContainer.SnakeMoveCommand);
                }
            }
        }

        protected virtual void FireOnSendMovement(DirectionEventArgs e)
        {
            OnSendMovement?.Invoke(this, e);
        }

        public void ClearEntrys()
        {
            lock (this.locker)
            {
                this.MovementActions.Clear();
            }
        }

        protected virtual void FireOnAnyKeyReceived()
        {
            OnAnyKeyReceived?.Invoke(this, EventArgs.Empty);
        }
    }
}