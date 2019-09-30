//-----------------------------------------------------------------------
// <copyright file="FieldEventArgs.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="FieldEventArgs"/> class.
    /// </summary>
    public class FieldEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FieldEventArgs"/> class.
        /// </summary>
        /// <param name="field"> The current <see cref="PlayingField"/>. </param>
        public FieldEventArgs(PlayingField field)
        {
            this.Field = field;
        }

        /// <summary>
        /// Gets the <see cref="PlayingField"/>.
        /// </summary>
        /// <value> A normal <see cref="PlayingField"/>. </value>
        public PlayingField Field
        {
            get;
        }
    }
}