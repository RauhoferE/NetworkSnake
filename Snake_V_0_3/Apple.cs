using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake_V_0_3
{
    public class Apple : StaticObjects
    {
        public Apple(Position position) : base(position, new Icon('A'))
        {
            this.Pos = position;
            this.Icon = new Icon('A');
            this.Points = 2;
        }
    }
}