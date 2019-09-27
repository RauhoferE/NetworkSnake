using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake_V_0_3
{
    using System.Threading;
    using System.Threading.Tasks;

    public class PowerupManager
    {
        private object locker;

        public PowerupManager()
        {
            this.Powerups = new List<StaticObjects>();
            this.locker = new object();
            this.ArePowerUpsBeingProduced = true;
        }

        public event EventHandler<CollisionEventArgs> OnRainbowTouched;

        public event EventHandler<StaticObjectEventArgs> OnAppleTouched;

        public event EventHandler<CollisionEventArgs> OnSegmentDestroyerTouched;

        public event EventHandler<StaticObjectEventArgs> OnPowerUpAdded;

        public event EventHandler<StaticObjectEventArgs> OnPowerUpRemoved;

        public event EventHandler StopPowerUpProduction;

        public event EventHandler StartPowerUpProduction;

        public List<StaticObjects> Powerups
        {
            get;
            private set;
        }

        public bool ArePowerUpsBeingProduced
        {
            get;
            set;
        }

        public bool IsPowerUpWatcherWorking
        {
            get;
            set;
        }

        public void AddPowerup(object sender, StaticObjectEventArgs e)
        {
            lock (this.locker)
            {
                this.Powerups.Add(e.GameObject);
            }

            this.FireOnPowerUpAdded(e);
        }

        public void RemovePowerup(object sender, StaticObjectEventArgs e)
        {
            lock (this.locker)
            {
                this.Powerups.Remove(this.Powerups.Where(x => x.Pos.X == e.GameObject.Pos.X).FirstOrDefault(x => x.Pos.Y == e.GameObject.Pos.Y));
            }

            this.FireOnPowerUpRemoved(e);
        }

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

        public void StartPowerUpWatcher()
        {
            this.IsPowerUpWatcherWorking = true;
            TaskFactory ts = new TaskFactory();
            ts.StartNew(() => this.PowerupWatcher());
        }

        public void StopPowerUpWatcher()
        {
            this.IsPowerUpWatcherWorking = false;
        }

        private void PowerupWatcher()
        {
            while (IsPowerUpWatcherWorking)
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

        protected virtual void FireOnRainbowTouched(CollisionEventArgs e)
        {
            OnRainbowTouched?.Invoke(this, e);
        }

        protected virtual void FireOnAppleTouched(StaticObjectEventArgs e)
        {
            OnAppleTouched?.Invoke(this, e);
        }

        protected virtual void FireOnSegmentDestroyerTouched(CollisionEventArgs e)
        {
            OnSegmentDestroyerTouched?.Invoke(this, e); 
        }

        protected virtual void FireOnPowerUpAdded(StaticObjectEventArgs e)
        {
            OnPowerUpAdded?.Invoke(this, e);
        }

        protected virtual void FireOnPowerUpRemoved(StaticObjectEventArgs e)
        {
            OnPowerUpRemoved?.Invoke(this, e);
        }

        protected virtual void FireStopPowerUpProduction()
        {
            StopPowerUpProduction?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void FireStartPowerUpProduction()
        {
            StartPowerUpProduction?.Invoke(this, EventArgs.Empty);
        }
    }
}