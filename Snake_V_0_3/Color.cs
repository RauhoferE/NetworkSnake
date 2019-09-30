//-----------------------------------------------------------------------
// <copyright file="Color.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="Color"/> class.
    /// </summary>
    public class Color
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> class.
        /// </summary>
        /// <param name="foreGround"> The foreground color. </param>
        /// <param name="backGround"> The background color. </param>
        public Color(ConsoleColor foreGround, ConsoleColor backGround)
        {
            this.ForeGroundColor = foreGround;
            this.BackGroundColor = backGround;
        }

        /// <summary>
        /// Gets or sets the foreground color.
        /// </summary>
        /// <value> A <see cref="ConsoleColor"/> value. </value>
        public ConsoleColor ForeGroundColor
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the background color.
        /// </summary>
        /// <value> A <see cref="ConsoleColor"/> value. </value>
        public ConsoleColor BackGroundColor
        {
            get;
            set;
        }
    }
}