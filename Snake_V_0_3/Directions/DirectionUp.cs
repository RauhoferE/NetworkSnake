using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake_V_0_3
{
    public class DirectionUp : IDirection
    {
        public int ID
        {
            get { return 3; }
        }

        public string Name
        {
            get { return "Up"; }
        }
    }
}