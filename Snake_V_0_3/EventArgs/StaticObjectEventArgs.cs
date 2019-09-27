using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake_V_0_3
{
    public class StaticObjectEventArgs
    {
        public StaticObjectEventArgs(StaticObjects gameObject)
        {
            this.GameObject = gameObject;
        }

        public StaticObjects GameObject
        {
            get;
            private set;
        }
    }
}