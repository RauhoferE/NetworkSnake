//-----------------------------------------------------------------------
// <copyright file="InputValidatorVM.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This is a network library.
// </summary>
//-----------------------------------------------------------------------
namespace WPFClientSnake
{
    using System;
    using System.Windows.Input;
    using NetworkLibrary;

    /// <summary>
    /// The <see cref="InputValidatorVM"/> class.
    /// </summary>
    public class InputValidatorVM
    {
        /// <summary>
        /// This event fires when a key has been pressed.
        /// </summary>
        public event EventHandler<ClientSnakeMovementEventArgs> OnKeyPressed;

        /// <summary>
        /// This method gets the input.
        /// </summary>
        /// <param name="e"> The <see cref="KeyEventArgs"/>. </param>
        public void GetInput(KeyEventArgs e)
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

        /// <summary>
        /// This method fires the <see cref="OnKeyPressed"/> event.
        /// </summary>
        /// <param name="e"> The <see cref="ClientSnakeMovementEventArgs"/>. </param>
        protected virtual void FireOnKeyPressed(ClientSnakeMovementEventArgs e)
        {
            this.OnKeyPressed?.Invoke(this, e);
        }
    }
}
