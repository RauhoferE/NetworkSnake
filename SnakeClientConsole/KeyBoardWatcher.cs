using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeClientConsole
{
    using System.Threading;

    public class KeyBoardWatcher : IInputWatcher
    {
        private Thread thread;
        private bool isRunning;

        public KeyBoardWatcher()
        {
            
            this.isRunning = false;
        }

        public event EventHandler<ConsoleKeyEventArgs> OnKeyInputReceived;

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

        public void Stop()
        {
            if (this.thread == null && !this.thread.IsAlive)
            {
                throw new ArgumentException("Error tread is already dead.");
            }

            this.isRunning = false;
            this.thread.Join();
        }

        public void Watcher()
        {
            while (this.isRunning)
            {
                var readKey = Console.ReadKey(true);
                this.FireOnKeyInputReceived(new ConsoleKeyEventArgs(readKey.Key, readKey.Modifiers, readKey.KeyChar));
            }
        }

        protected virtual void FireOnKeyInputReceived(ConsoleKeyEventArgs e)
        {
            OnKeyInputReceived?.Invoke(this, e);
        }
    }
}