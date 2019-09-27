using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake_V_0_3
{
    public class SnakeEventArgs
    {
        public SnakeEventArgs(List<SnakeSegment> list)
        {
            this.Snake = list;
        }

        public List<SnakeSegment> Snake
        {
            get;
            private set;
        }
    }
}