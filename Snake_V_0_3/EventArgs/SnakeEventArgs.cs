//-----------------------------------------------------------------------
// <copyright file="SnakeEventArgs.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="SnakeEventArgs"/> class.
    /// </summary>
    public class SnakeEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SnakeEventArgs"/> class.
        /// </summary>
        /// <param name="list"> The list of segments. </param>
        public SnakeEventArgs(List<SnakeSegment> list)
        {
            this.Snake = list;
        }

        /// <summary>
        /// Gets the list of segments.
        /// </summary>
        /// <value> A normal list of <see cref="SnakeSegment"/>. </value>
        public List<SnakeSegment> Snake
        {
            get;
        }
    }
}