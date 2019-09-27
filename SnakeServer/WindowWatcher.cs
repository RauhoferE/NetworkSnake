﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeServer
{
    using System.Threading;

    public class WindowWatcher
    {
        /// <summary>
        /// The window width.
        /// </summary>
        private int windowWidth;
        private Thread thread;

        /// <summary>
        /// The window height.
        /// </summary>
        private int windowHeight;

        public WindowWatcher(int windowWidth, int windowHeight)
        {
            this.windowHeight = windowHeight;
            this.windowWidth = windowWidth;

            this.thread = new Thread(this.Worker);
            this.thread.IsBackground = true;
        }

        public bool IsRunning
        {
            get;
            private set;
        }

        public void Start()
        {
            if (this.thread != null && this.thread.IsAlive)
            {
                throw new ArgumentException("Error thread is already running.");
            }

            this.thread.Start();
            this.IsRunning = true;
        }

        public void Stop()
        {
            if (this.thread == null || !this.thread.IsAlive)
            {
                throw new ArgumentException("Error thread is already dead.");
            }

            this.IsRunning = false;
            this.thread.Join();
        }

        public void Worker()
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