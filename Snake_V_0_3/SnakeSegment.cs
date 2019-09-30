//-----------------------------------------------------------------------
// <copyright file="SnakeSegment.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="SnakeSegment"/> class.
    /// </summary>
    public class SnakeSegment : GameObjects
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SnakeSegment"/> class.
        /// </summary>
        /// <param name="pos"> The <see cref="Position"/>. </param>
        /// <param name="icon"> The <see cref="Icon"/> value. </param>
        /// <param name="color"> The <see cref="Color"/>. </param>
        public SnakeSegment(Position pos, Icon icon, Color color) : base(pos, icon, color)
        {
            this.Icon = icon;
            this.Pos = pos;
            this.Color = color;
        }

        /// <summary>
        /// Gets or sets the <see cref="Color"/>.
        /// </summary>
        /// <value> A normal color. </value>
        public Color Color
        {
            get;
            set;
        }

        /// <summary>
        /// This method clones the snake segment.
        /// </summary>
        /// <returns> It returns a <see cref="SnakeSegment"/>. </returns>
        public SnakeSegment Clone()
        {
            return new SnakeSegment(this.Pos, this.Icon, this.Color);
        }
    }
}