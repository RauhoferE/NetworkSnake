using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake_V_0_3
{
    public abstract class StaticObjects : GameObjects
    {
        public StaticObjects(Position pos, Icon icon) : base(pos, icon, new Color(ConsoleColor.White, ConsoleColor.Black))
        {
            this.Pos = pos;
            this.Icon = icon;
        }

        public int Points
        {
            get;
            set;
        }
    }
}