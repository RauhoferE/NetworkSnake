using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake_V_0_3
{
    public class Color
    {
        public Color(System.ConsoleColor foreGround, System.ConsoleColor backGround)
        {
            this.ForeGroundColor = foreGround;
            this.BackGroundColor = backGround;
        }

        public System.ConsoleColor ForeGroundColor
        {
            get;
            set;
        }

        public System.ConsoleColor BackGroundColor
        {
            get;
            set;
        }
    }
}