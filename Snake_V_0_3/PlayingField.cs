using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake_V_0_3
{
    public class PlayingField
    {
        private Icon icon;

        public PlayingField(int length, int width, Icon icon)
        {
            this.Length = length;
            this.Width = width;
            this.Icon = icon;
        }

        public int Length
        {
            get;
            set;
        }

        public int Width
        {
            get;
            set;
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
    }
}