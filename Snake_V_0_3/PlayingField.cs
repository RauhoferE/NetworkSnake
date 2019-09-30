//-----------------------------------------------------------------------
// <copyright file="PlayingField.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="PlayingField"/> class.
    /// </summary>
    public class PlayingField
    {
        /// <summary>
        /// The icon of the field.
        /// </summary>
        private Icon icon;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayingField"/> class.
        /// </summary>
        /// <param name="length"> The length of the field. </param>
        /// <param name="width"> The width of the field. </param>
        /// <param name="icon"> The icon of the character. </param>
        public PlayingField(int length, int width, Icon icon)
        {
            this.Length = length;
            this.Width = width;
            this.Icon = icon;
        }

        /// <summary>
        /// Gets or sets the length of the field.
        /// </summary>
        /// <value> A normal integer. </value>
        public int Length
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the width of the field.
        /// </summary>
        /// <value> A normal integer. </value>
        public int Width
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the icon of the field.
        /// </summary>
        /// <value> A normal <see cref="Icon"/>. </value>
        public Icon Icon
        {
            get
            {
                return this.icon;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentException();
                }

                this.icon = value;
            }
        }
    }
}