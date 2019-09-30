//-----------------------------------------------------------------------
// <copyright file="MoveSnakeContainer.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This is a network library.
// </summary>
//-----------------------------------------------------------------------
namespace NetworkLibrary
{
    using System;

    /// <summary>
    /// The <see cref="MoveSnakeContainer"/> class.
    /// </summary>
    [Serializable]
    public class MoveSnakeContainer
    {
        /// <summary>
        /// The snake move type.
        /// </summary>
        private IInputType snakeMoveCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="MoveSnakeContainer"/> class.
        /// </summary>
        /// <param name="snakeMovement"> The snake movement type. </param>
        public MoveSnakeContainer(IInputType snakeMovement)
        {
            this.SnakeMoveCommand = snakeMovement;
        }

        /// <summary>
        /// Gets the snake move type.
        /// </summary>
        /// <value> A <see cref="IInputType"/> object. </value>
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