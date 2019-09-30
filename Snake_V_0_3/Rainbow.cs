//-----------------------------------------------------------------------
// <copyright file="Rainbow.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="Rainbow"/> class.
    /// </summary>
    public class Rainbow : StaticObjects
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rainbow"/> class.
        /// </summary>
        /// <param name="pos"> The <see cref="Position"/>. </param>
        public Rainbow(Position pos) : base(pos, new Icon('R'))
        {
            this.Pos = pos;
            this.Icon = new Icon('R');
            this.Points = 10;
        }
    }
}