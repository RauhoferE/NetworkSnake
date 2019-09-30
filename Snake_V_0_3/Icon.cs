//-----------------------------------------------------------------------
// <copyright file="Icon.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="Icon"/> class.
    /// </summary>
    public class Icon
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Icon"/> class.
        /// </summary>
        /// <param name="icon"> The character. </param>
        public Icon(char icon)
        {
            this.Character = icon;
        }

        /// <summary>
        /// Gets or sets the character.
        /// </summary>
        /// <value> A normal character. </value>
        public char Character
        {
            get;
            set;
        }
    }
}