﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake_V_0_3
{
    public class ScoreEventArgs
    {
        public ScoreEventArgs(int score)
        {
            this.Score = score;
        }

        public int Score
        {
            get;
            private set;
        }
    }
}