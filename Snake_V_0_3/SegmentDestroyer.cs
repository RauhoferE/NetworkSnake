//-----------------------------------------------------------------------
// <copyright file="SegmentDestroyer.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="SegmentDestroyer"/> class.
    /// </summary>
    public class SegmentDestroyer : StaticObjects
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SegmentDestroyer"/> class.
        /// </summary>
        /// <param name="pos"> The <see cref="Position"/>. </param>
        public SegmentDestroyer(Position pos) : base(pos, new Icon('Ω'))
        {
            this.Pos = pos;
            this.Icon = new Icon('Ω');
            this.Points = -10;
        }
    }
}