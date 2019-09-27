using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake_V_0_3
{
    public class Position
    {
        public Position()
        {
            this.X = 0;
            this.Y = 0;
        }

        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X
        {
            get;
            set;
        }

        public int Y
        {
            get;
            set;
        }
    }
}