//-----------------------------------------------------------------------
// <copyright file="StaticObjectEventArgs.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="StaticObjectEventArgs"/> class.
    /// </summary>
    public class StaticObjectEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StaticObjectEventArgs"/> class.
        /// </summary>
        /// <param name="gameObject"> The <see cref="StaticObjects"/>. </param>
        public StaticObjectEventArgs(StaticObjects gameObject)
        {
            this.GameObject = gameObject;
        }

        /// <summary>
        /// Gets the <see cref="StaticObjects"/>.
        /// </summary>
        /// <value> A normal <see cref="StaticObjects"/>. </value>
        public StaticObjects GameObject
        {
            get;
        }
    }
}