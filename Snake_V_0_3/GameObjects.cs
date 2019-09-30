//-----------------------------------------------------------------------
// <copyright file="GameObjects.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="GameObjects"/> class.
    /// </summary>
    public abstract class GameObjects
    {
        /// <summary>
        /// The icon of the object.
        /// </summary>
        private Icon icon;

        /// <summary>
        /// The position of the object.
        /// </summary>
        private Position pos;

        /// <summary>
        /// The color of the object.
        /// </summary>
        private Color color;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameObjects"/> class.
        /// </summary>
        /// <param name="pos"> The position of the object. </param>
        /// <param name="icon"> The icon of the object. </param>
        /// <param name="color"> The color of the object. </param>
        public GameObjects(Position pos, Icon icon, Color color)
        {
            this.Pos = pos;
            this.icon = icon;
            this.Color = color;
        }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value> A normal <see cref="Position"/>. </value>
        public Position Pos
        {
            get
            {
                return this.pos;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Error Pos cant be null.");
                }

                this.pos = value;
            }
        }

        /// <summary>
        /// Gets or sets the icon.
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
                    throw new ArgumentException("Error Pos cant be null.");
                }

                this.icon = value;
            }
        }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value> A normal <see cref="Color"/>. </value>
        public Color Color
        {
            get
            {
                return this.color;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Error color cant be null.");
                }

                this.color = value;
            }
        }
    }
}