//-----------------------------------------------------------------------
// <copyright file="ConsoleKeyEventArgs.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This is a network library.
// </summary>
//-----------------------------------------------------------------------
namespace SnakeClientConsole
{
    using System;

    /// <summary>
    /// The <see cref="ConsoleKeyEventArgs"/> class.
    /// </summary>
    public class ConsoleKeyEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleKeyEventArgs"/> class.
        /// </summary>
        /// <param name="key"> The pressed key. </param>
        /// <param name="mod"> The pressed modifier. </param>
        /// <param name="consoleChar"> The key as a character. </param>
        public ConsoleKeyEventArgs(ConsoleKey key, ConsoleModifiers mod, char consoleChar)
        {
            this.Key = key;
            this.Modifier = mod;
            this.ConsoleChar = consoleChar;
        }

        /// <summary>
        /// Gets the pressed key.
        /// </summary>
        /// <value> A <see cref="ConsoleKey"/>. </value>
        public ConsoleKey Key
        {
            get;
        }

        /// <summary>
        /// Gets the pressed modifier.
        /// </summary>
        /// <value> A <see cref="ConsoleModifiers"/>. </value>
        public ConsoleModifiers Modifier
        {
            get;
        }

        /// <summary>
        /// Gets the pressed character.
        /// </summary>
        /// <value> A normal <see cref="char"/>. </value>
        public char ConsoleChar
        {
            get;
        }
    }
}