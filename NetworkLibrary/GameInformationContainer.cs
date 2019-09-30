//-----------------------------------------------------------------------
// <copyright file="GameInformationContainer.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="GameInformationContainer"/> class.
    /// </summary>
    [Serializable]
    public class GameInformationContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameInformationContainer"/> class.
        /// </summary>
        /// <param name="snakeLength"> The snake length. </param>
        /// <param name="points"> The current points. </param>
        public GameInformationContainer(int snakeLength, int points)
        {
            this.SnakeLength = snakeLength;
            this.Points = points;
        }

        /// <summary>
        /// Gets the snake length.
        /// </summary>
        /// <value> A normal integer. </value>
        public int SnakeLength
        {
            get;
        }

        /// <summary>
        /// Gets the game points.
        /// </summary>
        /// <value> A normal integer. </value>
        public int Points
        {
            get;
        }
    }
}