using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkLibrary
{
    [Serializable]
    public class OtherKeyPressed : IInputType
    {
        public string Description
        {
            get { return "Any Key"; }
        }

        public int Id
        {
            get { return 999; }
        }
    }
}