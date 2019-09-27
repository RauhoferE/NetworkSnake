using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake_V_0_3
{
    public class CollisionEventArgs
    {
        public CollisionEventArgs(SnakeSegment segment, StaticObjects powerUp)
        {
            this.Segment = segment;
            this.PowerUp = powerUp;
        }

        public SnakeSegment Segment
        {
            get;
            private set;
        }

        public StaticObjects PowerUp
        {
            get;
            private set;
        }
    }
}