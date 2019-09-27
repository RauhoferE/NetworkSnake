using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake_V_0_3
{
    public class GameOBjectListEventArgs
    {
        public GameOBjectListEventArgs(List<GameObjects> newObj, List<GameObjects> oldObj, int score, int snakeLength)
        {
            this.NewObjects = newObj;
            this.OldObjects = oldObj;
            this.Score = score;
            this.SnakeLength = snakeLength;
        }

        public List<GameObjects> NewObjects
        {
            get;
            private set;
        }

        public List<GameObjects> OldObjects
        {
            get;
            private set;
        }

        public int Score
        {
            get;
            private set;
        }

        public int SnakeLength
        {
            get;
            private set;
        }
    }
}