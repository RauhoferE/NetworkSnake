using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake_V_0_3
{
    public class Rainbow : StaticObjects
    {
        public Rainbow(Position pos) : base(pos, new Icon('R'))
        {
            this.Pos = pos;
            this.Icon = new Icon('R');
            this.Points = 10;
        }
    }
}