using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeClientConsole
{
    using NetworkLibrary;

    public class InputValidator
    {
        public event EventHandler<ClientSnakeMovementEventArgs> OnSnakeMoved;

        public void GetInput(object sender, ConsoleKeyEventArgs e)
        {
            switch (e.Key)
            {
                case ConsoleKey.UpArrow:
                    this.FireOnSnakeMoved(new ClientSnakeMovementEventArgs(new MoveSnakeContainer(new MoveSnakeUp())));
                    break;
                case ConsoleKey.DownArrow:
                    this.FireOnSnakeMoved(new ClientSnakeMovementEventArgs(new MoveSnakeContainer(new MoveSnakeDown())));
                    break;
                case ConsoleKey.LeftArrow:
                    this.FireOnSnakeMoved(new ClientSnakeMovementEventArgs(new MoveSnakeContainer(new MoveSnakeLeft())));
                    break;
                case ConsoleKey.RightArrow:
                    this.FireOnSnakeMoved(new ClientSnakeMovementEventArgs(new MoveSnakeContainer(new MoveSnakeRight())));
                    break;
                default:
                    this.FireOnSnakeMoved(new ClientSnakeMovementEventArgs(new MoveSnakeContainer(new OtherKeyPressed())));
                    break;
            }
        }

        protected virtual void FireOnSnakeMoved(ClientSnakeMovementEventArgs e)
        {
            OnSnakeMoved?.Invoke(this, e);
        }
    }
}