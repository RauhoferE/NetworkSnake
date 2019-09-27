using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake_V_0_3
{
    public static class PowerUpHelper
    {

        public static System.ConsoleColor GetRandomColor(Random rnd)
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