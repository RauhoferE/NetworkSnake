//-----------------------------------------------------------------------
// <copyright file="ScoreBoard.cs" company="FH Wiener Neustadt">
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

    /// <summary>
    /// The <see cref="ScoreBoard"/> class.
    /// </summary>
    public class ScoreBoard
    {
        /// <summary>
        /// The locker object.
        /// </summary>
        private object locker;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreBoard"/> class.
        /// </summary>
        public ScoreBoard()
        {
            this.Score = 0;
            this.Multiplicator = 1;
            this.locker = new object();
        }

        /// <summary>
        /// This event fires when the score changes.
        /// </summary>
        public event EventHandler<ScoreEventArgs> OnScoreChange;

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
        /// Gets or sets the multi.
        /// </summary>
        /// <value> A normal integer. </value>
        public int Multiplicator
        {
            get;
            set;
        }

        /// <summary>
        /// This method changes the multi.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        public void ChangeMultiplicator(object sender, EventArgs e)
        {
            lock (this.locker)
            {
                this.Multiplicator++;
            }
        }

        /// <summary>
        /// This method changes the score.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="StaticObjectEventArgs"/>. </param>
        public void ChangeScore(object sender, StaticObjectEventArgs e)
        {
            lock (this.locker)
            {
                this.Score = this.Score + (e.GameObject.Points * this.Multiplicator);
            }
            
            this.FireOnScoreChanged(new ScoreEventArgs(this.Score));
        }

        /// <summary>
        /// This method gets the move points.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="SnakeEventArgs"/>. </param>
        public void GetMovePoints(object sender, SnakeEventArgs e)
        {
            lock (this.locker)
            {
                this.Score = this.Score + this.Multiplicator;
            }
            
            this.FireOnScoreChanged(new ScoreEventArgs(this.Score));
        }

        /// <summary>
        /// This method fires the <see cref="OnScoreChange"/> event.
        /// </summary>
        /// <param name="e"> The <see cref="ScoreEventArgs"/>. </param>
        protected virtual void FireOnScoreChanged(ScoreEventArgs e)
        {
            if (this.OnScoreChange != null)
            {
                this.OnScoreChange(this, e);
            }
        }
    }
}