//-----------------------------------------------------------------------
// <copyright file="PowerupManager.cs" company="FH Wiener Neustadt">
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The <see cref="PowerupManager"/> class.
    /// </summary>
    public class PowerupManager
    {
        /// <summary>
        /// The locker object.
        /// </summary>
        private object locker;

        /// <summary>
        /// Initializes a new instance of the <see cref="PowerupManager"/> class.
        /// </summary>
        public PowerupManager()
        {
            this.Powerups = new List<StaticObjects>();
            this.locker = new object();
            this.ArePowerUpsBeingProduced = true;
        }

        /// <summary>
        /// This method should fire when a rainbow is touched.
        /// </summary>
        public event EventHandler<CollisionEventArgs> OnRainbowTouched;

        /// <summary>
        /// This method should fire when a apple is touched.
        /// </summary>
        public event EventHandler<StaticObjectEventArgs> OnAppleTouched;

        /// <summary>
        /// This method should fire when a segment destroyer is touched.
        /// </summary>
        public event EventHandler<CollisionEventArgs> OnSegmentDestroyerTouched;

        /// <summary>
        /// This method should fire when a power up is added.
        /// </summary>
        public event EventHandler<StaticObjectEventArgs> OnPowerUpAdded;

        /// <summary>
        /// This method should fire when a power up is removed.
        /// </summary>
        public event EventHandler<StaticObjectEventArgs> OnPowerUpRemoved;

        /// <summary>
        /// This method should fire when the power up production is stopped.
        /// </summary>
        public event EventHandler StopPowerUpProduction;

        /// <summary>
        /// This method should fire when the power up production is started.
        /// </summary>
        public event EventHandler StartPowerUpProduction;

        /// <summary>
        /// Gets the list of power ups.
        /// </summary>
        /// <value> A normal list of <see cref="StaticObjects"/>. </value>
        public List<StaticObjects> Powerups
        {
            get;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the power ups are being produced.
        /// </summary>
        /// <value> Is true if power ups being produced. </value>
        public bool ArePowerUpsBeingProduced
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the power ups watcher is working.
        /// </summary>
        /// <value> Is true if power ups watcher is working. </value>
        public bool IsPowerUpWatcherWorking
        {
            get;
            set;
        }

        /// <summary>
        /// This method adds power ups to the list.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="StaticObjectEventArgs"/>. </param>
        public void AddPowerup(object sender, StaticObjectEventArgs e)
        {
            lock (this.locker)
            {
                this.Powerups.Add(e.GameObject);
            }

            this.FireOnPowerUpAdded(e);
        }

        /// <summary>
        /// This method removes power ups from the list.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="StaticObjectEventArgs"/>. </param>
        public void RemovePowerup(object sender, StaticObjectEventArgs e)
        {
            lock (this.locker)
            {
                this.Powerups.Remove(this.Powerups.Where(x => x.Pos.X == e.GameObject.Pos.X).FirstOrDefault(x => x.Pos.Y == e.GameObject.Pos.Y));
            }

            this.FireOnPowerUpRemoved(e);
        }

        /// <summary>
        /// This method gets the current power ups.
        /// </summary>
        /// <returns> It returns a list of <see cref="StaticObjects"/>. </returns>
        public List<StaticObjects> GetPowerUps()
        {
            lock (this.locker)
            {
                List<StaticObjects> l = new List<StaticObjects>();

                foreach (var powerup in this.Powerups)
                {
                    l.Add(powerup);
                }

                return l;
            }
        }

        /// <summary>
        /// This method checks the collision.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="CollisionEventArgs"/>. </param>
        public void CollisionHandler(object sender, CollisionEventArgs e)
        {
            lock (this.locker)
            {
                switch (e.PowerUp.Icon.Character)
                {
                    case 'R':
                        this.FireOnRainbowTouched(e);
                        break;
                    case 'A':
                        this.RemovePowerup(this, new StaticObjectEventArgs(e.PowerUp));
                        this.FireOnAppleTouched(new StaticObjectEventArgs(e.PowerUp));
                        break;
                    case 'Ω':
                        this.FireOnSegmentDestroyerTouched(e);
                        break;
                }
            }
        }

        /// <summary>
        /// This method starts the Power ups watcher.
        /// </summary>
        public void StartPowerUpWatcher()
        {
            this.IsPowerUpWatcherWorking = true;
            TaskFactory ts = new TaskFactory();
            ts.StartNew(() => this.PowerupWatcher());
        }

        /// <summary>
        /// This method stops the Power ups watcher.
        /// </summary>
        public void StopPowerUpWatcher()
        {
            this.IsPowerUpWatcherWorking = false;
        }

        /// <summary>
        /// This method fires the <see cref="OnRainbowTouched"/> event.
        /// </summary>
        /// <param name="e"> The <see cref="CollisionEventArgs"/>. </param>
        protected virtual void FireOnRainbowTouched(CollisionEventArgs e)
        {
            this.OnRainbowTouched?.Invoke(this, e);
        }

        /// <summary>
        /// This method fires the <see cref="OnAppleTouched"/> event.
        /// </summary>
        /// <param name="e"> The <see cref="StaticObjectEventArgs"/>. </param>
        protected virtual void FireOnAppleTouched(StaticObjectEventArgs e)
        {
            this.OnAppleTouched?.Invoke(this, e);
        }

        /// <summary>
        /// This method fires the <see cref="OnSegmentDestroyerTouched"/> event.
        /// </summary>
        /// <param name="e"> The <see cref="CollisionEventArgs"/>. </param>
        protected virtual void FireOnSegmentDestroyerTouched(CollisionEventArgs e)
        {
            this.OnSegmentDestroyerTouched?.Invoke(this, e); 
        }

        /// <summary>
        /// This method fires the <see cref="OnPowerUpAdded"/> event.
        /// </summary>
        /// <param name="e"> The <see cref="StaticObjectEventArgs"/>. </param>
        protected virtual void FireOnPowerUpAdded(StaticObjectEventArgs e)
        {
            this.OnPowerUpAdded?.Invoke(this, e);
        }

        /// <summary>
        /// This method fires the <see cref="OnPowerUpRemoved"/> event.
        /// </summary>
        /// <param name="e"> The <see cref="StaticObjectEventArgs"/>. </param>
        protected virtual void FireOnPowerUpRemoved(StaticObjectEventArgs e)
        {
            this.OnPowerUpRemoved?.Invoke(this, e);
        }

        /// <summary>
        /// This method fires the <see cref="StopPowerUpProduction"/>.
        /// </summary>
        protected virtual void FireStopPowerUpProduction()
        {
            this.StopPowerUpProduction?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="StartPowerUpProduction"/>.
        /// </summary>
        protected virtual void FireStartPowerUpProduction()
        {
            this.StartPowerUpProduction?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method is the watcher.
        /// </summary>
        private void PowerupWatcher()
        {
            while (this.IsPowerUpWatcherWorking)
            {
                lock (this.locker)
                {
                    if (this.Powerups.Count > 30 && this.ArePowerUpsBeingProduced)
                    {
                        this.ArePowerUpsBeingProduced = false;
                        this.FireStopPowerUpProduction();
                    }
                    else if (this.Powerups.Count < 20 && !this.ArePowerUpsBeingProduced)
                    {
                        this.ArePowerUpsBeingProduced = true;
                        this.FireStartPowerUpProduction();
                    }
                }

                Thread.Sleep(1000);
            }
        }
    }
}