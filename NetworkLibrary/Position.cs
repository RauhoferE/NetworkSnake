//-----------------------------------------------------------------------
// <copyright file="Position.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="Position"/> class.
    /// </summary>
    [Serializable]
    public class Position
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Position"/> class.
        /// </summary>
        /// <param name="x"> The x value. </param>
        /// <param name="y"> The y value. </param>
        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Gets the x value.
        /// </summary>
        /// <value> A normal integer. </value>
        public int X
        {
            get;
        }

        /// <summary>
        /// Gets the y value.
        /// </summary>
        /// <value> A normal integer. </value>
        public int Y
        {
            get;
        }
    }
}