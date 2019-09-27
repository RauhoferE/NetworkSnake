using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClientSnake.ViewModel
{
    using System.Windows.Input;
    using NetworkLibrary;

    public class InputValidatorVM
    {
        public event EventHandler<ClientSnakeMovementEventArgs> OnKeyPressed;
        public void GetInput(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    this.FireOnKeyPressed(new ClientSnakeMovementEventArgs(new MoveSnakeContainer(new MoveSnakeUp())));
                    break;
                case Key.Down:
                    this.FireOnKeyPressed(new ClientSnakeMovementEventArgs(new MoveSnakeContainer(new MoveSnakeDown())));
                    break;
                case Key.Left:
                    this.FireOnKeyPressed(new ClientSnakeMovementEventArgs(new MoveSnakeContainer(new MoveSnakeLeft())));
                    break;
                case Key.Right:
                    this.FireOnKeyPressed(new ClientSnakeMovementEventArgs(new MoveSnakeContainer(new MoveSnakeRight())));
                    break;
                default:
                    this.FireOnKeyPressed(new ClientSnakeMovementEventArgs(new MoveSnakeContainer(new OtherKeyPressed())));
                    break;
            }
        }

        protected virtual void FireOnKeyPressed(ClientSnakeMovementEventArgs e)
        {
            OnKeyPressed?.Invoke(this, e);
        }
    }
}
