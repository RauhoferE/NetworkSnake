using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkLibrary
{
    [Serializable]
    public class GameInformationContainer
    {
        public GameInformationContainer(int snakeLength, int points)
        {
            this.SnakeLength = snakeLength;
            this.Points = points;
        }

        public int SnakeLength
        {
            get;
            private set;
        }

        public int Points
        {
            get;
            private set;
        }
    }
}