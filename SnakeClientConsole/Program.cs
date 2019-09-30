//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This is a network library.
// </summary>
//-----------------------------------------------------------------------
namespace SnakeClientConsole
{
    /// <summary>
    /// The <see cref="Program"/> class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Starts the app.
        /// </summary>
        /// <param name="args"> Unspecified console args. </param>
        public static void Main(string[] args)
        {
            IInputWatcher watcher = new KeyBoardWatcher();
            IRenderer renderer = new ConsoleRenderer(120, 32);
            App app = new App(watcher, renderer);
            app.Start();
        }
    }
}
