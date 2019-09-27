using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeClientConsole
{
    using NetworkLibrary;

    public interface IRenderer
    {
        int WindowWidth { get; }
        int WindowHeight { get; }

        void PrintMessage(object sender, MessageContainerEventArgs e);
        void PrintErrorMessage(object sender, MessageContainerEventArgs e);
        void PrintGameObjectsAndInfo(object sender,  ObjectPrintEventArgs container);

        void PrintField(object sender, FieldMessageEventArgs container);

        /// <summary>
        /// Prints the user input.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="StringEventArgs"/>. </param>
        void PrintUserInput(object sender, MessageContainerEventArgs e);

        /// <summary>
        /// Deletes the user input.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="StringEventArgs"/>. </param>
        void DeleteUserInput(object sender, EventArgs e);
    }
}