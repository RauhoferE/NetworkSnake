using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkLibrary
{
    [Serializable]
    public class MoveSnakeUp : IInputType
    {
        public int Id
        {
            get
            {
                return 0;
            }
        }
        public string Description
        {
            get
            {
                return "Move Up";
            }
        }
    }
}