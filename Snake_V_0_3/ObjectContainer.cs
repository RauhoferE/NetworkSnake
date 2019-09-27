using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake_V_0_3
{
    using System.Threading;

    public class ObjectContainer
    {
        private Thread thread;

        private object locker;

        private bool IsRunning;

        public event EventHandler<GameOBjectListEventArgs> OnPrintGameList;

        public ObjectContainer()
        {
            this.locker = new object();
            this.OldSnake = new List<SnakeSegment>();
            this.NewSnake = new List<SnakeSegment>();
            this.NewPowerUp = new List<StaticObjects>();
            this.OldPowerUps = new List<StaticObjects>();
            this.Score = 0;
            this.IsRunning = false;
        }

        public int Score
        {
            get;
            set;
        }

        public List<SnakeSegment> OldSnake
        {
            get;
            private set;
        }

        public List<StaticObjects> OldPowerUps
        {
            get;
            private set;
        }

        public List<SnakeSegment> NewSnake
        {
            get;
            private set;
        }

        public List<StaticObjects> NewPowerUp
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

            this.IsRunning = true;
            this.thread = new Thread(this.Worker);
            this.thread.Start();
        }

        public void Stop()
        {
            if (this.thread == null || !this.thread.IsAlive)
            {
                throw new ArgumentException("Error thread is already dead.");
            }

            this.IsRunning = false;
            this.thread.Join();
        }

        public void Worker()
        {
            while (this.IsRunning)
            {
                lock (locker)
                {
                    List<GameObjects> newObj = new List<GameObjects>();


                    foreach (var element in NewSnake)
                    {
                        newObj.Add(element);
                    }
                    foreach (var element in NewPowerUp)
                    {
                        newObj.Add(element);
                    }

                    List<GameObjects> oldObj = new List<GameObjects>();
                    foreach (var element in OldPowerUps)
                    {
                        oldObj.Add(element);
                    }

                    foreach (var element in OldSnake)
                    {
                        oldObj.Add(element);
                    }

                    this.FireOnPrintGameList(new GameOBjectListEventArgs(newObj, oldObj, this.Score, this.NewSnake.Count));

                    this.OldPowerUps.Clear();
                }

                Thread.Sleep(50);
            }
        }

        public void GetNewPowerUp(object sender, StaticObjectEventArgs e)
        {
            lock (locker)
            {
                this.NewPowerUp.Add(e.GameObject);
            }
        }

        public void RemoveOldPowerup(object sender, StaticObjectEventArgs e)
        {
            lock (locker)
            {
                StaticObjects powerUpFound = null;

                foreach (var powerUp in NewPowerUp)
                {
                    if (e.GameObject.Pos.X == powerUp.Pos.X && e.GameObject.Pos.Y == powerUp.Pos.Y)
                    {
                        powerUpFound = powerUp;
                        break;
                    }
                }

                this.NewPowerUp.Remove(this.NewPowerUp.Where(x => x.Pos.X == powerUpFound.Pos.X)
                    .Where(x => x.Pos.Y == powerUpFound.Pos.Y).FirstOrDefault());


                this.OldPowerUps.Add(powerUpFound);
            }
        }

        public void GetNewSnakePosition(object sender, SnakeEventArgs e)
        {
            lock (this.locker)
            {
                this.OldSnake.Clear();

                foreach (var segment in this.NewSnake)
                {
                    this.OldSnake.Add(segment.Clone());
                }

                this.NewSnake = e.Snake;
            }
        }

        public void GetNewScore(object sender, ScoreEventArgs e)
        {
            lock (this.locker)
            {
                this.Score = e.Score;
            }
        }

        protected virtual void FireOnPrintGameList(GameOBjectListEventArgs e)
        {
            OnPrintGameList?.Invoke(this, e);
        }
    }
}