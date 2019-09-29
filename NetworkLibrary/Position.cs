using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkLibrary
{
    [Serializable]
    public class Position
    {
        private int x;
        private int y;

        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X
        {
            get
            {
                return this.x;
            }
            private set
            {

                this.x = value;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }
            private set
            {

                this.y = value;
            }
        }
    }
}