using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake_V_0_3
{
    using System.Security.Cryptography.X509Certificates;

    public class ScoreBoard
    {
        public event System.EventHandler<ScoreEventArgs> OnScoreChange;

        private object locker;

        public ScoreBoard()
        {
            this.Score = 0;
            this.Multiplicator = 1;
            this.locker = new object();
        }

        public string PlayerName
        {
            get;
            set;
        }

        public int Score
        {
            get;
            set;
        }

        public int Multiplicator
        {
            get;
            set;
        }

        public void ChangeMultiplicator(object sender, EventArgs e)
        {
            lock (this.locker)
            {
                this.Multiplicator++;
            }
        }

        public void ChangeScore(object sender, StaticObjectEventArgs e)
        {
            lock (this.locker)
            {
                this.Score = this.Score + e.GameObject.Points * this.Multiplicator;
            }
            
            this.FireOnScoreChanged(new ScoreEventArgs(this.Score));
        }

        public void GetMovePoints(object sender, SnakeEventArgs e)
        {
            lock (this.locker)
            {
                this.Score = this.Score + this.Multiplicator;
            }
            
            this.FireOnScoreChanged(new ScoreEventArgs(this.Score));
        }

        protected virtual void FireOnScoreChanged(ScoreEventArgs e)
        {
            if (OnScoreChange != null)
            {
                this.OnScoreChange(this, e);
            }
        }
    }
}