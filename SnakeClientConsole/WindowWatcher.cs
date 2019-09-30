//-----------------------------------------------------------------------
// <copyright file="WindowWatcher.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="WindowWatcher"/> class.
    /// </summary>
    public class WindowWatcher
    {
        /// <summary>
        /// The window width.
        /// </summary>
        private int windowWidth;

        /// <summary>
        /// The watcher thread.
        /// </summary>
        private Thread thread;

        /// <summary>
        /// The window height.
        /// </summary>
        private int windowHeight;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowWatcher"/> class.
        /// </summary>
        /// <param name="windowWidth"> The window width. </param>
        /// <param name="windowHeight"> The window height. </param>
        public WindowWatcher(int windowWidth, int windowHeight)
        {
            this.windowHeight = windowHeight;
            this.windowWidth = windowWidth;

            Console.SetWindowSize(this.windowWidth, this.windowHeight);
        }

        /// <summary>
        /// Gets a value indicating whether the thread is running or not.
        /// </summary>
        /// <value> Is true if the thread is running. </value>
        public bool IsRunning
        {
            get;
            private set;
        }

        /// <summary>
        /// This method starts the watcher.
        /// </summary>
        public void Start()
        {
            if (this.thread != null && this.thread.IsAlive)
            {
                throw new ArgumentException("Error thread is already running.");
            }

            this.IsRunning = true;
            this.thread = new Thread(this.Worker);
            this.thread.IsBackground = true;
            this.thread.Start();
        }

        /// <summary>
        /// This method stops the watcher.
        /// </summary>
        public void Stop()
        {
            if (this.thread == null || !this.thread.IsAlive)
            {
                throw new ArgumentException("Error thread is already dead.");
            }

            this.IsRunning = false;
            this.thread.Join();
        }

        /// <summary>
        /// The worker.
        /// </summary>
        private void Worker()
        {
            while (this.IsRunning)
            {
                lock (new object())
                {
                    if (Console.WindowWidth < this.windowWidth || Console.WindowHeight < this.windowHeight || Console.WindowWidth > this.windowWidth || Console.WindowHeight > this.windowHeight)
                    {
                        try
                        {
                            Console.SetWindowSize(120, 30);
                        }
                        catch
                        {
                        }
                    }
                }

                Thread.Sleep(1000);
            }
        }
    }
}