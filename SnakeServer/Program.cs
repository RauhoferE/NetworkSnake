//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This is a network library.
// </summary>
//-----------------------------------------------------------------------
namespace SnakeServer
{
    /// <summary>
    /// The <see cref="Program"/> class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Starts the app.
        /// </summary>
        /// <param name="args"> Unspecified arguments. </param>
        public static void Main(string[] args)
        {
            Application app = new Application(new ConsoleRenderer(120, 30));
            app.Start();
        }
    }
}
