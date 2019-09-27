using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeServer
{
    using NetworkLibrary;
    using Snake_V_0_3;

    public class ConsoleRenderer : IRenderer
    {
        public ConsoleRenderer(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public int Width
        {
            get;
        }

        public int Height
        {
            get;
        }

        public void PrintClientConnectInfo(object sender, ClientIDEventArgs e)
        {
            Console.WriteLine("Client with the number: " + e.ClientID + " just connected. " + DateTime.Now.Hour + ":" + DateTime.Now.Minute);
        }

        public void PrintClientDisConnectInfo(object sender, ClientIDEventArgs e)
        {
            Console.WriteLine("Client with the number: " + e.ClientID + " disconnected. " + DateTime.Now.Hour + ":" + DateTime.Now.Minute);
        }

        public void PrintSnakeMovementReceived(object sender, SnakeMoveEventArgs e)
        {
            if (e.MoveSnakeContainer.SnakeMoveCommand.Id == new OtherKeyPressed().Id)
            {
                return;
            }

            Console.WriteLine(e.ClientID + " Client with the number: " + e.ClientID + " used the command " + e.MoveSnakeContainer.SnakeMoveCommand.Description + " to move the snake. " + DateTime.Now.Hour + ":" + DateTime.Now.Minute);
        }

        public void PrintGameOver(object sender, EventArgs e)
        {
            Console.WriteLine("Game Over " + DateTime.Now.Hour + ":" + DateTime.Now.Minute);
        }

        public void PrintMessage(string s)
        {
            Console.WriteLine(s);
        }

        public void PrintErrorMessage(object sender, StringEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e.Text);
            Console.ResetColor();
        }
    }
}