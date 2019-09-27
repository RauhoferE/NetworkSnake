using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkLibrary
{
    [Serializable]
    public class MoveSnakeRight : IInputType
    {
        public int Id
        {
            get
            {
                return 2;
            }
        }
        public string Description
        {
            get
            {
                return "Move right";
            }
        }
    }
}