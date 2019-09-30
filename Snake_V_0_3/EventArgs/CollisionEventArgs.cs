//-----------------------------------------------------------------------
// <copyright file="CollisionEventArgs.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="CollisionEventArgs"/> class.
    /// </summary>
    public class CollisionEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CollisionEventArgs"/> class.
        /// </summary>
        /// <param name="segment"> The current <see cref="SnakeSegment"/>. </param>
        /// <param name="powerUp"> The current <see cref="StaticObjects"/>. </param>
        public CollisionEventArgs(SnakeSegment segment, StaticObjects powerUp)
        {
            this.Segment = segment;
            this.PowerUp = powerUp;
        }

        /// <summary>
        /// Gets the <see cref="SnakeSegment"/>.
        /// </summary>
        /// <value> A normal <see cref="SnakeSegment"/>. </value>
        public SnakeSegment Segment
        {
            get;
        }

        /// <summary>
        /// Gets the <see cref="StaticObjects"/>.
        /// </summary>
        /// <value> A normal <see cref="StaticObjects"/>. </value>
        public StaticObjects PowerUp
        {
            get;
        }
    }
}