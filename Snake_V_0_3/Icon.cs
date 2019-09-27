using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake_V_0_3
{
    public class Icon
    {
        public Icon()
        {
            this.Character = 'D';
        }

        public Icon(char icon)
        {
            this.Character = icon;
        }

        public char Character
        {
            get;
            set;
        }
    }
}