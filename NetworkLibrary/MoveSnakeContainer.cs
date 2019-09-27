using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkLibrary
{
    [Serializable]
    public class MoveSnakeContainer
    {
        private IInputType snakeMoveCommand;

        public MoveSnakeContainer(IInputType snakeMovement)
        {
            this.SnakeMoveCommand = snakeMovement;
        }

        public IInputType SnakeMoveCommand
        {
            get
            {
                return this.snakeMoveCommand;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Error value cant be null.");
                }

                this.snakeMoveCommand = value;
            }
        }

    }
}