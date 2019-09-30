//-----------------------------------------------------------------------
// <copyright file="IInputWatcher.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="IInputWatcher"/> interface.
    /// </summary>
    public interface IInputWatcher
    {
        /// <summary>
        /// This event fires when a key has been read.
        /// </summary>
        event EventHandler<ConsoleKeyEventArgs> OnKeyInputReceived;

        /// <summary>
        /// This method starts the watcher.
        /// </summary>
        void Start();

        /// <summary>
        /// This method stops the watcher.
        /// </summary>
        void Stop();

        /// <summary>
        /// This method starts the watcher.
        /// </summary>
        void Watcher();
    }
}