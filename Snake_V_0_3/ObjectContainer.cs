//-----------------------------------------------------------------------
// <copyright file="ObjectContainer.cs" company="FH Wiener Neustadt">
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

    /// <summary>
    /// The <see cref="ObjectContainer"/> class.
    /// </summary>
    public class ObjectContainer
    {
        /// <summary>
        /// The container thread.
        /// </summary>
        private Thread thread;

        /// <summary>
        /// The locker.
        /// </summary>
        private object locker;

        /// <summary>
        /// Says if the thread is running or not.
        /// </summary>
        private bool isRunning;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectContainer"/> class.
        /// </summary>
        public ObjectContainer()
        {
            this.locker = new object();
            this.OldSnake = new List<SnakeSegment>();
            this.NewSnake = new List<SnakeSegment>();
            this.NewPowerUp = new List<StaticObjects>();
            this.OldPowerUps = new List<StaticObjects>();
            this.Score = 0;
            this.isRunning = false;
        }

        /// <summary>
        /// This event should fire when the game object list should be printed.
        /// </summary>
        public event EventHandler<GameOBjectListEventArgs> OnPrintGameList;

        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        /// <value> A normal integer. </value>
        public int Score
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the old snake.
        /// </summary>
        /// <value> A normal list of <see cref="SnakeSegment"/>. </value>
        public List<SnakeSegment> OldSnake
        {
            get;
        }

        /// <summary>
        /// Gets the old power ups.
        /// </summary>
        /// <value> A normal list of <see cref="StaticObjects"/>. </value>
        public List<StaticObjects> OldPowerUps
        {
            get;
        }

        /// <summary>
        /// Gets the new snake.
        /// </summary>
        /// <value> A normal list of <see cref="SnakeSegment"/>. </value>
        public List<SnakeSegment> NewSnake
        {
            get;
        }

        /// <summary>
        /// Gets the old power ups.
        /// </summary>
        /// <value> A normal list of <see cref="StaticObjects"/>. </value>
        public List<StaticObjects> NewPowerUp
        {
            get;
        }

        /// <summary>
        /// This method starts the thread.
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
        /// This method stops the thread.
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
        /// This method gets the new power ups.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="StaticObjectEventArgs"/>. </param>
        public void GetNewPowerUp(object sender, StaticObjectEventArgs e)
        {
            lock (this.locker)
            {
                this.NewPowerUp.Add(e.GameObject);
            }
        }

        /// <summary>
        /// This method removes the old power ups.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="StaticObjectEventArgs"/>. </param>
        public void RemoveOldPowerup(object sender, StaticObjectEventArgs e)
        {
            lock (this.locker)
            {
                StaticObjects powerUpFound = null;

                foreach (var powerUp in this.NewPowerUp)
                {
                    if (e.GameObject.Pos.X == powerUp.Pos.X && e.GameObject.Pos.Y == powerUp.Pos.Y)
                    {
                        powerUpFound = powerUp;
                        break;
                    }
                }

                if (this.NewPowerUp.Where(x => x.Pos.X == powerUpFound.Pos.X).Where(x => x.Pos.Y == powerUpFound.Pos.Y).FirstOrDefault() != null)
                {
                    this.NewPowerUp.Remove(this.NewPowerUp.Where(x => x.Pos.X == powerUpFound.Pos.X)
                        .Where(x => x.Pos.Y == powerUpFound.Pos.Y).FirstOrDefault());
                }

                this.OldPowerUps.Add(powerUpFound);
            }
        }

        /// <summary>
        /// This method gets the new snake position.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="SnakeEventArgs"/>. </param>
        public void GetNewSnakePosition(object sender, SnakeEventArgs e)
        {
            lock (this.locker)
            {
                this.OldSnake.Clear();

                foreach (var segment in this.NewSnake)
                {
                    this.OldSnake.Add(segment.Clone());
                }

                this.NewSnake.Clear();

                foreach (var element in e.Snake)
                {
                    this.NewSnake.Add(element);
                }
            }
        }

        /// <summary>
        /// This method gets the new score.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="ScoreEventArgs"/>. </param>
        public void GetNewScore(object sender, ScoreEventArgs e)
        {
            lock (this.locker)
            {
                this.Score = e.Score;
            }
        }

        /// <summary>
        /// This method fires the <see cref="OnPrintGameList"/> event.
        /// </summary>
        /// <param name="e"> The <see cref="GameOBjectListEventArgs"/>. </param>
        protected virtual void FireOnPrintGameList(GameOBjectListEventArgs e)
        {
            this.OnPrintGameList?.Invoke(this, e);
        }

        /// <summary>
        /// This method creates <see cref="ObjectContainer"/>.
        /// </summary>
        private void Worker()
        {
            while (this.isRunning)
            {
                lock (this.locker)
                {
                    List<GameObjects> newObj = new List<GameObjects>();

                    foreach (var element in this.NewSnake)
                    {
                        newObj.Add(element);
                    }

                    foreach (var element in this.NewPowerUp)
                    {
                        newObj.Add(element);
                    }

                    List<GameObjects> oldObj = new List<GameObjects>();
                    foreach (var element in this.OldPowerUps)
                    {
                        oldObj.Add(element);
                    }

                    foreach (var element in this.OldSnake)
                    {
                        oldObj.Add(element);
                    }

                    this.FireOnPrintGameList(new GameOBjectListEventArgs(newObj, oldObj, this.Score, this.NewSnake.Count));

                    this.OldPowerUps.Clear();
                }

                Thread.Sleep(50);
            }
        }
    }
}