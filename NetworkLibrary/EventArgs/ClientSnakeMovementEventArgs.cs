//-----------------------------------------------------------------------
// <copyright file="ClientSnakeMovementEventArgs.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="ClientSnakeMovementEventArgs"/> class.
    /// </summary>
    public class ClientSnakeMovementEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientSnakeMovementEventArgs"/> class.
        /// </summary>
        /// <param name="container"> The <see cref="MoveSnakeContainer"/>. </param>
        public ClientSnakeMovementEventArgs(MoveSnakeContainer container)
        {
            this.Container = container;
        }

        /// <summary>
        /// Gets the <see cref="MoveSnakeContainer"/>.
        /// </summary>
        /// <value> A <see cref="MoveSnakeContainer"/> object. </value>
        public MoveSnakeContainer Container
        {
            get;
        }
    }
}