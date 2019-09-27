using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake_V_0_3
{
    public class DirectionLeft : IDirection
    {
        public int ID
        {
            get { return 2; }
        }

        public string Name
        {
            get { return "Left"; }
        }
    }
}