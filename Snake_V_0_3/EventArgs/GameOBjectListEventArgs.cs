//-----------------------------------------------------------------------
// <copyright file="GameOBjectListEventArgs.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This is a network library.
// </summary>
//-----------------------------------------------------------------------
namespace Snake_V_0_3
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="GameOBjectListEventArgs"/> class.
    /// </summary>
    public class GameOBjectListEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameOBjectListEventArgs"/> class.
        /// </summary>
        /// <param name="newObj"> The new objects. </param>
        /// <param name="oldObj"> The old objects. </param>
        /// <param name="score"> The player score. </param>
        /// <param name="snakeLength"> The snake length. </param>
        public GameOBjectListEventArgs(List<GameObjects> newObj, List<GameObjects> oldObj, int score, int snakeLength)
        {
            this.NewObjects = newObj;
            this.OldObjects = oldObj;
            this.Score = score;
            this.SnakeLength = snakeLength;
        }

        /// <summary>
        /// Gets the list of new objects.
        /// </summary>
        /// <value> A normal list of <see cref="GameObjects"/>. </value>
        public List<GameObjects> NewObjects
        {
            get;
        }

        /// <summary>
        /// Gets the list of old objects.
        /// </summary>
        /// <value> A normal list of <see cref="GameObjects"/>. </value>
        public List<GameObjects> OldObjects
        {
            get;
        }

        /// <summary>
        /// Gets the score of the player.
        /// </summary>
        /// <value> A normal integer. </value>
        public int Score
        {
            get;
        }

        /// <summary>
        /// Gets the length of the snake.
        /// </summary>
        /// <value> A normal integer. </value>
        public int SnakeLength
        {
            get;
        }
    }
}