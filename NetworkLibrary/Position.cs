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
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Error value cant be smaller than 0.");
                }

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
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Error value cant be smaller than 0.");
                }

                this.y = value;
            }
        }
    }
}