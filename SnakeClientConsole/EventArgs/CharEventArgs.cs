//-----------------------------------------------------------------------
// <copyright file="CharEventArgs.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This is a network library.
// </summary>
//-----------------------------------------------------------------------
namespace SnakeClientConsole
{
    using System;

    /// <summary>
    /// The <see cref="CharEventArgs"/> class.
    /// </summary>
    public class CharEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CharEventArgs"/> class.
        /// </summary>
        /// <param name="character"> The character. </param>
        public CharEventArgs(char character)
        {
            this.Character = character;
        }

        /// <summary>
        /// Gets the character.
        /// </summary>
        /// <value> A normal character. </value>
        public char Character
        {
            get;
        }
    }
}