using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake_V_0_3
{
    public class SegmentDestroyer : StaticObjects
    {
        public SegmentDestroyer(Position pos) : base(pos, new Icon('Ω'))
        {
            this.Pos = pos;
            this.Icon = new Icon('Ω');
            this.Points = -10;
        }
    }
}