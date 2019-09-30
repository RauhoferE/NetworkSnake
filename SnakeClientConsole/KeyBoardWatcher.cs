//-----------------------------------------------------------------------
// <copyright file="KeyBoardWatcher.cs" company="FH Wiener Neustadt">
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
    using System.Threading;

    /// <summary>
    /// The <see cref="KeyBoardWatcher"/> class.
    /// </summary>
    public class KeyBoardWatcher : IInputWatcher
    {
        /// <summary>
        /// The watcher thread.
        /// </summary>
        private Thread thread;

        /// <summary>
        /// Is true when the thread is running.
        /// </summary>
        private bool isRunning;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyBoardWatcher"/> class.
        /// </summary>
        public KeyBoardWatcher()
        {
            this.isRunning = false;
        }

        /// <summary>
        /// This event fires when a key input has been received.
        /// </summary>
        public event EventHandler<ConsoleKeyEventArgs> OnKeyInputReceived;

        /// <summary>
        /// This method starts the watcher.
        /// </summary>
        public void Start()
        {
            if (this.thread != null && this.thread.IsAlive)
            {
                throw new ArgumentException("Error tread is already running.");
            }

            this.isRunning = true;
            this.thread = new Thread(this.Watcher);
            this.thread.IsBackground = true;
            this.thread.Start();
        }

        /// <summary>
        /// This method stops the watcher.
        /// </summary>
        public void Stop()
        {
            if (this.thread == null && !this.thread.IsAlive)
            {
                throw new ArgumentException("Error tread is already dead.");
            }

            this.isRunning = false;
            this.thread.Join();
        }

        /// <summary>
        /// This method starts the watcher.
        /// </summary>
        public void Watcher()
        {
            while (this.isRunning)
            {
                var readKey = Console.ReadKey(true);
                this.FireOnKeyInputReceived(new ConsoleKeyEventArgs(readKey.Key, readKey.Modifiers, readKey.KeyChar));
            }
        }

        /// <summary>
        /// This method fires the <see cref="OnKeyInputReceived"/> event.
        /// </summary>
        /// <param name="e"> The <see cref="ConsoleKeyEventArgs"/>. </param>
        protected virtual void FireOnKeyInputReceived(ConsoleKeyEventArgs e)
        {
            this.OnKeyInputReceived?.Invoke(this, e);
        }
    }
}