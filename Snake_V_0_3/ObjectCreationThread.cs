using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Snake_V_0_3
{
    public class ObjectCreationThread
    {
        private Thread thread;
        private System.Random rnd;
        

        public event System.EventHandler OnStart;
        public event System.EventHandler OnStop;

        public ObjectCreationThread()
        {
            this.IsRunning = false;
            this.rnd = new Random();
            this.Factory = new StaticGameObjectFactory(this.rnd);
        }

        public StaticGameObjectFactory Factory
        {
            get;
            private set;
        }

        public bool IsRunning
        {
            get;
            set;
        }

        protected virtual void FireOnStart()
        {
            if (this.OnStart != null)
            {
                this.OnStart(this, EventArgs.Empty);
            }
        }

        protected virtual void FireOnStop()
        {
            if (this.OnStop != null)
            {
                this.OnStop(this, EventArgs.Empty);
            }
        }

        public void Worker()
        {
            this.FireOnStart();
            while (this.IsRunning)
            {
                this.Factory.CreatePowerUp();
                Thread.Sleep(1500);
            }

            this.FireOnStop();
        }

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

        public void Stop()
        {
            if (this.thread == null && !this.thread.IsAlive)
            {
                throw new ArgumentException();
            }

            this.IsRunning = false;
            this.thread.Join();
        }

        public void StopAll(object sender, EventArgs e)
        {
            this.Stop();
        }

        public void StartAll(object sende, EventArgs e)
        {
            this.Start();
        }
    }
}