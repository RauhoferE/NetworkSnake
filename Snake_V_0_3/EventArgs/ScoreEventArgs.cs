//-----------------------------------------------------------------------
// <copyright file="ScoreEventArgs.cs" company="FH Wiener Neustadt">
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

    /// <summary>
    /// The <see cref="ScoreEventArgs"/> class.
    /// </summary>
    public class ScoreEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreEventArgs"/> class.
        /// </summary>
        /// <param name="score"> The player score. </param>
        public ScoreEventArgs(int score)
        {
            this.Score = score;
        }

        /// <summary>
        /// Gets the player score.
        /// </summary>
        /// <value> A normal integer. </value>
        public int Score
        {
            get;
        }
    }
}