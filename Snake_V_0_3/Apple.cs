//-----------------------------------------------------------------------
// <copyright file="Apple.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="Apple"/> class.
    /// </summary>
    public class Apple : StaticObjects
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Apple"/> class.
        /// </summary>
        /// <param name="position"> The position of the apple. </param>
        public Apple(Position position) : base(position, new Icon('A'))
        {
            this.Pos = position;
            this.Icon = new Icon('A');
            this.Points = 2;
        }
    }
}