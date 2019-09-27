using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake_V_0_3
{
    public abstract class GameObjects
    {
        private Icon icon;
        private Position pos;

        public GameObjects(Position pos, Icon icon, Color color)
        {
            this.Pos = pos;
            this.icon = icon;
            this.Color = color;
        }

        public Position Pos
        {
            get
            {
                return this.pos;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentException();
                }

                this.pos = value;
            }
        }

        public Icon Icon
        {
            get
            {
                return this.icon;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentException();
                }

                this.icon = value;
            }
        }

        public Color Color
        {
            get;
            set;
        }
    }
}