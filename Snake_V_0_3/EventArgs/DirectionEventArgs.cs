//-----------------------------------------------------------------------
// <copyright file="DirectionEventArgs.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="DirectionEventArgs"/> class.
    /// </summary>
    public class DirectionEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectionEventArgs"/> class.
        /// </summary>
        /// <param name="e"> The current <see cref="IDirection"/>. </param>
        public DirectionEventArgs(IDirection e)
        {
            this.Direction = e;
        }

        /// <summary>
        /// Gets the new direction.
        /// </summary>
        /// <value> A normal <see cref="IDirection"/> object. </value>
        public IDirection Direction
        {
            get;
        }
    }
}