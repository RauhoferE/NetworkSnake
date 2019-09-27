using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeClientConsole
{
    public interface IInputWatcher
    {
        event System.EventHandler<ConsoleKeyEventArgs> OnKeyInputReceived;

        void Start();
        void Stop();
        void Watcher();
    }
}