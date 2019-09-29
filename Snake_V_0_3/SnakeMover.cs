using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake_V_0_3
{
    using System.Threading;
    using System.Timers;

    public class SnakeMover
    {
        private Thread thread;

        private object locker;

        private int speed;

        private System.Timers.Timer timer;

        private bool recentlyMoved;


        public SnakeMover()
        {
            this.locker = new object();
            this.Snake = new List<SnakeSegment>()
            {   new SnakeSegment(new Position(2, 0), new Icon('X'), new Color(ConsoleColor.White, ConsoleColor.Black)),
                new SnakeSegment(new Position(1, 0), new Icon('+'), new Color(ConsoleColor.White, ConsoleColor.Black)),
                new SnakeSegment(new Position(0, 0), new Icon('+'), new Color(ConsoleColor.White, ConsoleColor.Black)),
            };

            this.IsRunning = false;
            this.speed = 1000;
            this.CurrentDirection = new DirectionRight();
            this.timer = new System.Timers.Timer(10000);
            this.timer.Elapsed += this.MakeSnakeFaster;
            this.recentlyMoved = false;
        }

        private void MakeSnakeFaster(object sender, ElapsedEventArgs e)
        {
            if (this.speed <= 400)
            {
                return;
            }

            this.speed = this.speed - 100;
            this.timer.Stop();
            this.timer.Start();
            this.FireOnSpeedChanged();
        }

        public event EventHandler<SnakeEventArgs> OnSnakeMoved;

        public event EventHandler OnSpeedChanged;

        public event EventHandler<StaticObjectEventArgs> OnLastSegmentPassed;

        public List<SnakeSegment> Snake
        {
            get;
            private set;
        }

        public IDirection CurrentDirection
        {
            get;
            private set;
        }

        public bool IsRunning { get; private set; }

        public void MoveSnake()
        {
            while (this.IsRunning)
            {
                lock (this.locker)
                {
                    List<SnakeSegment> cloned = new List<SnakeSegment>();

                    foreach (var element in this.Snake)
                    {
                        cloned.Add(element.Clone());
                    }

                    switch (this.CurrentDirection.ID)
                    {
                        case 0:
                            this.Snake.ElementAt(0).Pos = new Position(this.Snake.ElementAt(0).Pos.X + 1, this.Snake.ElementAt(0).Pos.Y);

                            for (int i = 0; i < this.Snake.Count; i++)
                            {
                                if (i != 0)
                                {
                                    this.Snake[i].Pos = cloned[i - 1].Pos;
                                }
                            }
                            break;
                        case 1:
                            this.Snake.ElementAt(0).Pos = new Position(this.Snake.ElementAt(0).Pos.X, this.Snake.ElementAt(0).Pos.Y + 1);

                            for (int i = 0; i < this.Snake.Count; i++)
                            {
                                if (i != 0)
                                {
                                    this.Snake[i].Pos = cloned[i - 1].Pos;
                                }
                            }
                            break;
                        case 2:
                            this.Snake.ElementAt(0).Pos = new Position(this.Snake.ElementAt(0).Pos.X - 1, this.Snake.ElementAt(0).Pos.Y);

                            for (int i = 0; i < this.Snake.Count; i++)
                            {
                                if (i != 0)
                                {
                                    this.Snake[i].Pos = cloned[i - 1].Pos;
                                }
                            }
                            break;
                        case 3:
                            this.Snake.ElementAt(0).Pos = new Position(this.Snake.ElementAt(0).Pos.X, this.Snake.ElementAt(0).Pos.Y - 1);

                            for (int i = 0; i < this.Snake.Count; i++)
                            {
                                if (i != 0)
                                {
                                    this.Snake[i].Pos = cloned[i - 1].Pos;
                                }
                            }
                            break;
                    }
                }

                List<SnakeSegment> temp = new List<SnakeSegment>();

                foreach (var el in this.Snake)
                {
                    temp.Add(el.Clone());
                }

                this.recentlyMoved = true;
                this.FireOnSnakeMoved(new SnakeEventArgs(temp));

                Thread.Sleep(this.speed);
            }
        }

        public void ChangeColor(object sender, CollisionEventArgs e)
        {
            lock (locker)
            {
                this.Snake.Where(x => x.Pos.X == e.Segment.Pos.X).FirstOrDefault(x => x.Pos.Y == e.Segment.Pos.Y).Color.ForeGroundColor = PowerUpHelper.GetRandomColor(new Random());

                if (e.Segment.Pos.X == this.Snake.LastOrDefault().Pos.X && e.Segment.Pos.Y == this.Snake.LastOrDefault().Pos.Y)
                {
                    this.FireOnLastSegmentPassed(new StaticObjectEventArgs(e.PowerUp));
                }
            }
        }

        public void AddSegment(object sender, StaticObjectEventArgs e)
        {
            lock (this.locker)
            {
                this.Snake.Add(new SnakeSegment(new Position(this.Snake.LastOrDefault().Pos.X, this.Snake.LastOrDefault().Pos.Y), new Icon('+'), new Color(ConsoleColor.White, ConsoleColor.Black)));
            }
        }

        public void RemoveSegment(object sender, CollisionEventArgs e)
        {
            lock (this.locker)
            {
                if (e.Segment.Pos.X == this.Snake.LastOrDefault().Pos.X && e.Segment.Pos.Y == this.Snake.LastOrDefault().Pos.Y && this.Snake.Count > 1)
                {
                    this.Snake.Remove(this.Snake.LastOrDefault());
                    this.FireOnLastSegmentPassed(new StaticObjectEventArgs(e.PowerUp));
                }
            }
        }

        public void ChangeDirection(object sender, DirectionEventArgs e)
        {
            lock (this.locker)
            {
                if (this.CurrentDirection.ID == e.Direction.ID)
                {
                    return;
                }
                else if (this.CurrentDirection.ID == 3 && e.Direction.ID == 1)
                {
                    return;
                }
                else if (this.CurrentDirection.ID == 1 && e.Direction.ID == 3)
                {
                    return;
                }
                else if (this.CurrentDirection.ID == 0 && e.Direction.ID == 2)
                {
                    return;
                }
                else if (this.CurrentDirection.ID == 2 && e.Direction.ID == 0)
                {
                    return;
                }

                if (this.recentlyMoved)
                {
                    this.recentlyMoved = false;
                    this.CurrentDirection = e.Direction;
                }
            }
        }

        public void Start()
        {
            if (this.thread != null && this.thread.IsAlive)
            {
                throw new ArgumentException("Error thread has started.");
            }

            this.thread = new Thread(this.MoveSnake);
            this.IsRunning = true;
            this.timer.Start();
            this.thread.Start();
        }

        public void Stop()
        {
            if (this.thread == null || !this.thread.IsAlive)
            {
                throw new ArgumentException("Error thread is already dead.");
            }

            this.IsRunning = false;
            this.timer.Stop();
            this.thread.Join();
        }

        public List<SnakeSegment> GetCurrentSnakeSegmentList()
        {
            lock (this.locker)
            {
                return this.Snake;
            }
        }

        protected virtual void FireOnSnakeMoved(SnakeEventArgs e)
        {
            OnSnakeMoved?.Invoke(this, e);
        }

        protected virtual void FireOnSpeedChanged()
        {
            OnSpeedChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void FireOnLastSegmentPassed(StaticObjectEventArgs e)
        {
            OnLastSegmentPassed?.Invoke(this, e);
        }
    }
}