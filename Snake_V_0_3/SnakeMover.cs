//-----------------------------------------------------------------------
// <copyright file="SnakeMover.cs" company="FH Wiener Neustadt">
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
    using System.Timers;

    /// <summary>
    /// The <see cref="SnakeMover"/> class.
    /// </summary>
    public class SnakeMover
    {
        /// <summary>
        /// The mover thread.
        /// </summary>
        private Thread thread;

        /// <summary>
        /// The object locker.
        /// </summary>
        private object locker;

        /// <summary>
        /// The snake speed.
        /// </summary>
        private int speed;

        /// <summary>
        /// The make snake faster timer.
        /// </summary>
        private System.Timers.Timer timer;

        /// <summary>
        /// Is true when the snake has been moved.
        /// </summary>
        private bool recentlyMoved;

        /// <summary>
        /// Initializes a new instance of the <see cref="SnakeMover"/> class.
        /// </summary>
        public SnakeMover()
        {
            this.locker = new object();
            this.Snake = new List<SnakeSegment>()
            {
                new SnakeSegment(new Position(2, 0), new Icon('X'), new Color(ConsoleColor.White, ConsoleColor.Black)),
                new SnakeSegment(new Position(1, 0), new Icon('+'), new Color(ConsoleColor.White, ConsoleColor.Black)),
                new SnakeSegment(new Position(0, 0), new Icon('+'), new Color(ConsoleColor.White, ConsoleColor.Black)),
            };

            this.IsRunning = false;
            this.speed = 1000;
            this.CurrentDirection = new DirectionRight();
            this.timer = new System.Timers.Timer(60000);
            this.timer.Elapsed += this.MakeSnakeFaster;
            this.recentlyMoved = false;
        }

        /// <summary>
        /// This event should fire when the snake has been moved.
        /// </summary>
        public event EventHandler<SnakeEventArgs> OnSnakeMoved;

        /// <summary>
        /// This event fires when the speed has been changed.
        /// </summary>
        public event EventHandler OnSpeedChanged;

        /// <summary>
        /// This event fires when the last segment is passed.
        /// </summary>
        public event EventHandler<StaticObjectEventArgs> OnLastSegmentPassed;

        /// <summary>
        /// Gets the list of <see cref="SnakeSegment"/>.
        /// </summary>
        /// <value> A normal list of <see cref="SnakeSegment"/>. </value>
        public List<SnakeSegment> Snake
        {
            get;
        }

        /// <summary>
        /// Gets the <see cref="CurrentDirection"/>.
        /// </summary>
        /// <value> A normal <see cref="IDirection"/>. </value>
        public IDirection CurrentDirection
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether the thread is running or not.
        /// </summary>
        /// <value> Is true when the thread is running. </value>
        public bool IsRunning
        {
            get;
            private set;
        }

        /// <summary>
        /// This method changes the color of the segment.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="CollisionEventArgs"/>. </param>
        public void ChangeColor(object sender, CollisionEventArgs e)
        {
            lock (this.locker)
            {
                if (this.Snake.Where(x => x.Pos.X == e.Segment.Pos.X).FirstOrDefault(x => x.Pos.Y == e.Segment.Pos.Y) == null)
                {
                    return;
                }

                this.Snake.Where(x => x.Pos.X == e.Segment.Pos.X).FirstOrDefault(x => x.Pos.Y == e.Segment.Pos.Y).Color.ForeGroundColor = PowerUpHelper.GetRandomColor(new Random());

                if (e.Segment.Pos.X == this.Snake.LastOrDefault().Pos.X && e.Segment.Pos.Y == this.Snake.LastOrDefault().Pos.Y)
                {
                    this.FireOnLastSegmentPassed(new StaticObjectEventArgs(e.PowerUp));
                }
            }
        }

        /// <summary>
        /// This method adds a segment.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="StaticObjectEventArgs"/>. </param>
        public void AddSegment(object sender, StaticObjectEventArgs e)
        {
            lock (this.locker)
            {
                this.Snake.Add(new SnakeSegment(new Position(this.Snake.LastOrDefault().Pos.X, this.Snake.LastOrDefault().Pos.Y), new Icon('+'), new Color(ConsoleColor.White, ConsoleColor.Black)));
            }
        }

        /// <summary>
        /// This method removes the segment.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="CollisionEventArgs"/>. </param>
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

        /// <summary>
        /// This method changes the direction of the snake.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="DirectionEventArgs"/>. </param>
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

        /// <summary>
        /// This method starts the mover.
        /// </summary>
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

        /// <summary>
        /// This method stops the mover.
        /// </summary>
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

        /// <summary>
        /// This method gets the current snake segment list.
        /// </summary>
        /// <returns> It returns a list of <see cref="SnakeSegment"/>. </returns>
        public List<SnakeSegment> GetCurrentSnakeSegmentList()
        {
            lock (this.locker)
            {
                return this.Snake;
            }
        }

        /// <summary>
        /// This method fires the <see cref="OnSnakeMoved"/> event.
        /// </summary>
        /// <param name="e"> The <see cref="SnakeEventArgs"/>. </param>
        protected virtual void FireOnSnakeMoved(SnakeEventArgs e)
        {
            this.OnSnakeMoved?.Invoke(this, e);
        }

        /// <summary>
        /// This method fires the <see cref="OnSpeedChanged"/> event.
        /// </summary>
        protected virtual void FireOnSpeedChanged()
        {
            this.OnSpeedChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This event fires when the <see cref="OnLastSegmentPassed"/> event.
        /// </summary>
        /// <param name="e"> The <see cref="StaticObjectEventArgs"/>. </param>
        protected virtual void FireOnLastSegmentPassed(StaticObjectEventArgs e)
        {
            this.OnLastSegmentPassed?.Invoke(this, e);
        }

        /// <summary>
        /// This method moves the snake.
        /// </summary>
        private void MoveSnake()
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

        /// <summary>
        /// This makes the snake faster.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="ElapsedEventArgs"/>. </param>
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
    }
}