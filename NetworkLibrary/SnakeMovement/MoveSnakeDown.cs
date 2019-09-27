using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkLibrary
{
    [Serializable]
    public class MoveSnakeDown : IInputType
    {
        public int Id
        {
            get
            {
                return 1;
            }
        }
        public string Description
        {
            get
            {
                return "Move down";
            }
        }
    }
}