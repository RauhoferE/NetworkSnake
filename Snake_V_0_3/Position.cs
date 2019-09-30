//-----------------------------------------------------------------------
// <copyright file="Position.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This is a network library.
// </summary>
//-----------------------------------------------------------------------
namespace Snake_V_0_3
{
    /// <summary>
    /// The <see cref="Position"/> class.
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Position"/> class.
        /// </summary>
        public Position()
        {
            this.X = 0;
            this.Y = 0;
        }

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
        /// Gets or sets the x value.
        /// </summary>
        /// <value> A normal integer. </value>
        public int X
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the y value.
        /// </summary>
        /// <value> A normal integer. </value>
        public int Y
        {
            get;
            set;
        }
    }
}