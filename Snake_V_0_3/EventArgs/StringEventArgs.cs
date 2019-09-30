//-----------------------------------------------------------------------
// <copyright file="StringEventArgs.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="StringEventArgs"/> class.
    /// </summary>
    public class StringEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringEventArgs"/> class.
        /// </summary>
        /// <param name="s"> The text to be send. </param>
        public StringEventArgs(string s)
        {
            this.Text = s;
        }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <value> A normal string. </value>
        public string Text
        {
            get;
        }
    }
}