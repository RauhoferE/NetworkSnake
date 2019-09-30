//-----------------------------------------------------------------------
// <copyright file="MovementManager.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This is a network library.
// </summary>
//-----------------------------------------------------------------------
namespace SnakeServer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using NetworkLibrary;
    using Snake_V_0_3;

    /// <summary>
    /// The <see cref="MovementManager"/> class.
    /// </summary>
    public class MovementManager
    {
        /// <summary>
        /// The locker object.
        /// </summary>
        private object locker;

        /// <summary>
        /// The manager thread.
        /// </summary>
        private Thread thread;

        /// <summary>
        /// Is true if the thread is running.
        /// </summary>
        private bool isRunning;

        /// <summary>
        /// Initializes a new instance of the <see cref="MovementManager"/> class.
        /// </summary>
        public MovementManager()
        {
            this.MovementActions = new List<IInputType>();
            this.locker = new object();
        }

        /// <summary>
        /// This event should fire when the movement should be send to the game.
        /// </summary>
        public event EventHandler<DirectionEventArgs> OnSendMovement;

        /// <summary>
        /// This event should fire any key has been received.
        /// </summary>
        public event EventHandler OnAnyKeyReceived;

        /// <summary>
        /// Gets the list of <see cref="IInputType"/>.
        /// </summary>
        /// <value> A list of <see cref="IInputType"/> </value>
        public List<IInputType> MovementActions
        {
            get;
        }

        /// <summary>
        /// Starts the manager.
        /// </summary>
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

        /// <summary>
        /// Stops the manager.
        /// </summary>
        public void Stop()
        {
            if (this.thread == null || !this.thread.IsAlive)
            {
                throw new ArgumentException("Error thread is already dead.");
            }

            this.isRunning = false;
            this.thread.Join();
        }

        /// <summary>
        /// Adds the next movement to the list.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="SnakeMoveEventArgs"/>. </param>
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

        /// <summary>
        /// This method clears the list.
        /// </summary>
        public void ClearEntrys()
        {
            lock (this.locker)
            {
                this.MovementActions.Clear();
            }
        }

        /// <summary>
        /// This method fires the <see cref="OnSendMovement"/> event.
        /// </summary>
        /// <param name="e"> The <see cref="DirectionEventArgs"/>. </param>
        protected virtual void FireOnSendMovement(DirectionEventArgs e)
        {
            this.OnSendMovement?.Invoke(this, e);
        }

        /// <summary>
        /// This method fires the <see cref="OnAnyKeyReceived"/> event.
        /// </summary>
        protected virtual void FireOnAnyKeyReceived()
        {
            this.OnAnyKeyReceived?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// The worker method.
        /// </summary>
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
    }
}