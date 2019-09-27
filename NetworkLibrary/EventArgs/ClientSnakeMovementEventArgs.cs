using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkLibrary
{
    public class ClientSnakeMovementEventArgs
    {
        public ClientSnakeMovementEventArgs(MoveSnakeContainer container)
        {
            this.Container = container;
        }

        public MoveSnakeContainer Container
        {
            get;
            private set;
        }
    }
}