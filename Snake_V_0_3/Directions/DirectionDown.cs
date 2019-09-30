//-----------------------------------------------------------------------
// <copyright file="DirectionDown.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="DirectionDown"/> class.
    /// </summary>
    public class DirectionDown : IDirection
    {
        /// <summary>
        /// Gets the id of the direction.
        /// </summary>
        /// <value> A normal integer. </value>
        public int ID
        {
            get { return 1; }
        }

        /// <summary>
        /// Gets the description of the direction.
        /// </summary>
        /// <value> A normal string. </value>
        public string Name
        {
            get { return "Down"; }
        }
    }
}