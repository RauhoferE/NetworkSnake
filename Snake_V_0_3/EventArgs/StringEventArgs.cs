using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake_V_0_3
{
    public class StringEventArgs
    {
        public StringEventArgs(string s)
        {
            this.Text = s;
        }

        public string Text
        {
            get;
            private set;
        }
    }
}