//-----------------------------------------------------------------------
// <copyright file="PowerUpHelper.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="PowerUpHelper"/> class.
    /// </summary>
    public static class PowerUpHelper
    {
        /// <summary>
        /// This method returns a random color.
        /// </summary>
        /// <param name="rnd"> The random. </param>
        /// <returns> It returns a <see cref="ConsoleColor"/>. </returns>
        public static ConsoleColor GetRandomColor(Random rnd)
        {
            switch (rnd.Next(1, 12))
            {
                case 0:
                    return ConsoleColor.White;
                case 1:
                    return ConsoleColor.Cyan;
                case 2:
                    return ConsoleColor.Cyan;
                case 3:
                    return ConsoleColor.Cyan;
                case 4:
                    return ConsoleColor.Green;
                case 5:
                    return ConsoleColor.Magenta;
                case 6:
                    return ConsoleColor.Red;
                case 7:
                    return ConsoleColor.Yellow;
                case 8:
                    return ConsoleColor.Green;
                case 9:
                    return ConsoleColor.Magenta;
                case 10:
                    return ConsoleColor.Red;
                case 11:
                    return ConsoleColor.Yellow;
                default:
                    return ConsoleColor.White;
            }
        }
    }
}