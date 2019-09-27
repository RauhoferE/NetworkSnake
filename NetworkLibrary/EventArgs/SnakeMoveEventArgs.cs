using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkLibrary
{
    public class SnakeMoveEventArgs
    {
        public SnakeMoveEventArgs(MoveSnakeContainer container, int clientID)
        {
            this.MoveSnakeContainer = container;
            this.ClientID = clientID;
        }

        public MoveSnakeContainer MoveSnakeContainer
        {
            get;
        }

        public int ClientID
        {
            get;
        }
    }
}