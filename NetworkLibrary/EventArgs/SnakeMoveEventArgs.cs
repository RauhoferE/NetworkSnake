//-----------------------------------------------------------------------
// <copyright file="SnakeMoveEventArgs.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="SnakeMoveEventArgs"/> class.
    /// </summary>
    public class SnakeMoveEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SnakeMoveEventArgs"/> class.
        /// </summary>
        /// <param name="container"> The <see cref="MoveSnakeContainer"/>. </param>
        /// <param name="clientID"> The client id. </param>
        public SnakeMoveEventArgs(MoveSnakeContainer container, int clientID)
        {
            this.MoveSnakeContainer = container;
            this.ClientID = clientID;
        }

        /// <summary>
        /// Gets the <see cref="MoveSnakeContainer"/>.
        /// </summary>
        /// <value> A <see cref="MoveSnakeContainer"/> object. </value>
        public MoveSnakeContainer MoveSnakeContainer
        {
            get;
        }

        /// <summary>
        /// Gets the client id.
        /// </summary>
        /// <value> A normal integer. </value>
        public int ClientID
        {
            get;
        }
    }
}