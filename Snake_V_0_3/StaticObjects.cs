//-----------------------------------------------------------------------
// <copyright file="StaticObjects.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="StaticObjects"/> class.
    /// </summary>
    public abstract class StaticObjects : GameObjects
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StaticObjects"/> class.
        /// </summary>
        /// <param name="pos"> The <see cref="Position"/>. </param>
        /// <param name="icon"> The <see cref="Icon"/> value. </param>
        public StaticObjects(Position pos, Icon icon) : base(pos, icon, new Color(ConsoleColor.White, ConsoleColor.Black))
        {
            this.Pos = pos;
            this.Icon = icon;
        }

        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        /// <value> A normal integer. </value>
        public int Points
        {
            get;
            set;
        }
    }
}