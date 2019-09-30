//-----------------------------------------------------------------------
// <copyright file="IRenderer.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="IRenderer"/> class.
    /// </summary>
    public interface IRenderer
    {
        /// <summary>
        /// Gets the width of the renderer.
        /// </summary>
        /// <value> A normal integer. </value>
        int Width { get; }

        /// <summary>
        /// Gets the height of the renderer.
        /// </summary>
        /// <value> A normal integer. </value>
        int Height { get; }

        /// <summary>
        /// This method prints the client connect info.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="ClientIDEventArgs"/>. </param>
        void PrintClientConnectInfo(object sender, ClientIDEventArgs e);

        /// <summary>
        /// This method prints the client disconnect info.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="ClientIDEventArgs"/>. </param>
        void PrintClientDisConnectInfo(object sender, ClientIDEventArgs e);

        /// <summary>
        /// This method prints the received movement from the client.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="SnakeMoveEventArgs"/>. </param>
        void PrintSnakeMovementReceived(object sender, SnakeMoveEventArgs e);

        /// <summary>
        /// This method prints the game over.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        void PrintGameOver(object sender, EventArgs e);

        /// <summary>
        /// This method prints a message.
        /// </summary>
        /// <param name="s"> The string. </param>
        void PrintMessage(string s);

        /// <summary>
        /// This method prints an error message.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="StringEventArgs"/>. </param>
        void PrintErrorMessage(object sender, StringEventArgs e);
    }
}