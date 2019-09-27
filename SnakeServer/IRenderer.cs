using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeServer
{
    using NetworkLibrary;
    using Snake_V_0_3;

    public interface IRenderer
    {
        int Width { get; }
        int Height { get; }

        void PrintClientConnectInfo(object sender, ClientIDEventArgs e);

        void PrintClientDisConnectInfo(object sender, ClientIDEventArgs e);

        void PrintSnakeMovementReceived(object sender, SnakeMoveEventArgs e);

        void PrintGameOver(object sender, EventArgs e);
        void PrintMessage(string s);

        void PrintErrorMessage(object sender, StringEventArgs e);
    }
}