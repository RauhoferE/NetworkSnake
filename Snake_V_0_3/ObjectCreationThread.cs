//-----------------------------------------------------------------------
// <copyright file="ObjectCreationThread.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This is a network library.
// </summary>
//-----------------------------------------------------------------------
namespace Snake_V_0_3
{
    using System;
    using System.Threading;

    /// <summary>
    /// The <see cref="ObjectCreationThread"/> class.
    /// </summary>
    public class ObjectCreationThread
    {
        /// <summary>
        /// The creation thread.
        /// </summary>
        private Thread thread;

        /// <summary>
        /// A normal random.
        /// </summary>
        private Random rnd;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectCreationThread"/> class.
        /// </summary>
        public ObjectCreationThread()
        {
            this.IsRunning = false;
            this.rnd = new Random();
            this.Factory = new StaticGameObjectFactory(this.rnd);
        }

        /// <summary>
        /// This event should fire when the thread starts.
        /// </summary>
        public event EventHandler OnStart;

        /// <summary>
        /// This event should fire when the thread stops.
        /// </summary>
        public event EventHandler OnStop;

        /// <summary>
        /// Gets the <see cref="StaticGameObjectFactory"/>.
        /// </summary>
        /// <value> A normal <see cref="StaticGameObjectFactory"/>. </value>
        public StaticGameObjectFactory Factory
        {
            get;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the thread is running or not.
        /// </summary>
        /// <value> Is true if the thread is running. </value>
        public bool IsRunning
        {
            get;
            set;
        }

        /// <summary>
        /// This method starts the factory.
        /// </summary>
        public void Start()
        {
            if (this.thread != null && this.thread.IsAlive)
            {
                throw new ArgumentException();
            }

            this.IsRunning = true;
            this.thread = new Thread(this.Worker);
            this.thread.Start();
        }

        /// <summary>
        /// This method stops the factory.
        /// </summary>
        public void Stop()
        {
            if (this.thread == null && !this.thread.IsAlive)
            {
                throw new ArgumentException();
            }

            this.IsRunning = false;
            this.thread.Join();
        }

        /// <summary>
        /// This method stops the factory.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        public void StopAll(object sender, EventArgs e)
        {
            this.Stop();
        }

        /// <summary>
        /// This method starts the factory.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        public void StartAll(object sender, EventArgs e)
        {
            this.Start();
        }

        /// <summary>
        /// This method fires the <see cref="OnStart"/> event.
        /// </summary>
        protected virtual void FireOnStart()
        {
            if (this.OnStart != null)
            {
                this.OnStart(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// This method fires the <see cref="OnStop"/> event.
        /// </summary>
        protected virtual void FireOnStop()
        {
            if (this.OnStop != null)
            {
                this.OnStop(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// The worker method.
        /// </summary>
        private void Worker()
        {
            this.FireOnStart();
            while (this.IsRunning)
            {
                this.Factory.CreatePowerUp();
                Thread.Sleep(1500);
            }

            this.FireOnStop();
        }
    }
}