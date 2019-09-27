using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake_V_0_3
{
    public class SnakeSegment : GameObjects
    {
        public SnakeSegment(Position pos, Icon icon, Color color) : base(pos, icon, color)
        {
            this.Icon = icon;
            this.Pos = pos;
            this.Color = color;
        }

        public Color Color
        {
            get;
            set;
        }

        public SnakeSegment Clone()
        {
            return new SnakeSegment(this.Pos, this.Icon, this.Color);
        }
    }
}