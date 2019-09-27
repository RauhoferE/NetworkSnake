using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeClientConsole
{
    public class CharEventArgs
    {
        public CharEventArgs(char character)
        {
            this.Character = character;
        }

        public char Character
        {
            get;
            private set;
        }
    }
}