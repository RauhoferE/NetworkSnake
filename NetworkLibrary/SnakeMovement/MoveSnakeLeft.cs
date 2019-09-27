using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkLibrary
{
    [Serializable]
    public class MoveSnakeLeft : IInputType
    {
        public int Id
        {
            get
            {
                return 3;
            }
        }
        public string Description
        {
            get
            {
                return "Move left";
            }
        }
    }
}