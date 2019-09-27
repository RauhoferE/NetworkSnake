using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeServer
{
    public class MessageEventArgs
    {
        public MessageEventArgs(string s)
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