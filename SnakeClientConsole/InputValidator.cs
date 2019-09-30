//-----------------------------------------------------------------------
// <copyright file="InputValidator.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This is a network library.
// </summary>
//-----------------------------------------------------------------------
namespace SnakeClientConsole
{
    using System;
    using NetworkLibrary;

    /// <summary>
    /// The <see cref="InputValidator"/> class.
    /// </summary>
    public class InputValidator
    {
        /// <summary>
        /// This event should fire when the snake has been moved.
        /// </summary>
        public event EventHandler<ClientSnakeMovementEventArgs> OnSnakeMoved;

        /// <summary>
        /// This method gets the input.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="ConsoleKeyEventArgs"/>. </param>
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

        /// <summary>
        /// This method fires the <see cref="OnSnakeMoved"/> event.
        /// </summary>
        /// <param name="e"> The <see cref="ClientSnakeMovementEventArgs"/>. </param>
        protected virtual void FireOnSnakeMoved(ClientSnakeMovementEventArgs e)
        {
            this.OnSnakeMoved?.Invoke(this, e);
        }
    }
}