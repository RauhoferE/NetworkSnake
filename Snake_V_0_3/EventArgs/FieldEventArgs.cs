using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake_V_0_3
{
    public class FieldEventArgs
    {
        public FieldEventArgs(PlayingField field)
        {
            this.Field = field;
        }

        public PlayingField Field
        {
            get;
            private set;
        }
    }
}