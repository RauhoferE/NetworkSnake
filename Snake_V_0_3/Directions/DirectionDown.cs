using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake_V_0_3
{
    public class DirectionDown : IDirection
    {
        public int ID
        {
            get { return 1; }
        }

        public string Name
        {
            get { return "Down"; }
        }
    }
}