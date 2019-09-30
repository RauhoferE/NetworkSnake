//-----------------------------------------------------------------------
// <copyright file="IRenderer.cs" company="FH Wiener Neustadt">
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
    using NetworkLibrary;

    /// <summary>
    /// The <see cref="IRenderer"/> interface.
    /// </summary>
    public interface IRenderer
    {
        /// <summary>
        /// Gets the window width.
        /// </summary>
        /// <value> A normal integer. </value>
        int WindowWidth { get; }

        /// <summary>
        /// Gets the window height.
        /// </summary>
        /// <value> A normal integer. </value>
        int WindowHeight { get; }

        /// <summary>
        /// This method print a message.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="MessageContainerEventArgs"/>. </param>
        void PrintMessage(object sender, MessageContainerEventArgs e);

        /// <summary>
        /// This method print a error message.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="MessageContainerEventArgs"/>. </param>
        void PrintErrorMessage(object sender, MessageContainerEventArgs e);

        /// <summary>
        /// This method print a game objects and information.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="container"> The <see cref="ObjectPrintEventArgs"/>. </param>
        void PrintGameObjectsAndInfo(object sender,  ObjectPrintEventArgs container);

        /// <summary>
        /// This method prints a field.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="container"> The <see cref="FieldMessageEventArgs"/>. </param>
        void PrintField(object sender, FieldMessageEventArgs container);

        /// <summary>
        /// Prints the user input.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="MessageContainerEventArgs"/>. </param>
        void PrintUserInput(object sender, MessageContainerEventArgs e);

        /// <summary>
        /// Deletes the user input.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="MessageContainerEventArgs"/>. </param>
        void DeleteUserInput(object sender, EventArgs e);
    }
}