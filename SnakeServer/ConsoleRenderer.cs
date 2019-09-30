//-----------------------------------------------------------------------
// <copyright file="ConsoleRenderer.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This is a network library.
// </summary>
//-----------------------------------------------------------------------
namespace SnakeServer
{
    using System;
    using NetworkLibrary;
    using Snake_V_0_3;

    /// <summary>
    /// The <see cref="ConsoleRenderer"/> class.
    /// </summary>
    public class ConsoleRenderer : IRenderer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleRenderer"/> class.
        /// </summary>
        /// <param name="width"> The width of the console. </param>
        /// <param name="height"> The height of the console. </param>
        public ConsoleRenderer(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// Gets the width of the renderer.
        /// </summary>
        /// <value> A normal integer. </value>
        public int Width
        {
            get;
        }

        /// <summary>
        /// Gets the height of the renderer.
        /// </summary>
        /// <value> A normal integer. </value>
        public int Height
        {
            get;
        }

        /// <summary>
        /// This method prints the client connect info.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="ClientIDEventArgs"/>. </param>
        public void PrintClientConnectInfo(object sender, ClientIDEventArgs e)
        {
            Console.WriteLine("Client with the number: " + e.ClientID + " just connected. " + DateTimeReturner.ReturnCurrentTime());
        }

        /// <summary>
        /// This method prints the client disconnect info.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="ClientIDEventArgs"/>. </param>
        public void PrintClientDisConnectInfo(object sender, ClientIDEventArgs e)
        {
            Console.WriteLine("Client with the number: " + e.ClientID + " disconnected. " + DateTimeReturner.ReturnCurrentTime());
        }

        /// <summary>
        /// This method prints the received movement from the client.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="SnakeMoveEventArgs"/>. </param>
        public void PrintSnakeMovementReceived(object sender, SnakeMoveEventArgs e)
        {
            if (e.MoveSnakeContainer.SnakeMoveCommand.Id == new OtherKeyPressed().Id)
            {
                return;
            }

            Console.WriteLine(e.ClientID + " Client with the number: " + e.ClientID + " used the command " + e.MoveSnakeContainer.SnakeMoveCommand.Description + " to move the snake. " + DateTimeReturner.ReturnCurrentTime());
        }

        /// <summary>
        /// This method prints the game over.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        public void PrintGameOver(object sender, EventArgs e)
        {
            Console.WriteLine("Game Over " + DateTimeReturner.ReturnCurrentTime());
        }

        /// <summary>
        /// This method prints a message.
        /// </summary>
        /// <param name="s"> The string. </param>
        public void PrintMessage(string s)
        {
            Console.WriteLine(s);
        }

        /// <summary>
        /// This method prints an error message.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="StringEventArgs"/>. </param>
        public void PrintErrorMessage(object sender, StringEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e.Text);
            Console.ResetColor();
        }
    }
}