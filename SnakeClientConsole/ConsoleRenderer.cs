//-----------------------------------------------------------------------
// <copyright file="ConsoleRenderer.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="ConsoleRenderer"/> class.
    /// </summary>
    public class ConsoleRenderer : IRenderer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleRenderer"/> class.
        /// </summary>
        /// <param name="windowWidth"> The window width. </param>
        /// <param name="windowHeight"> The window height. </param>
        public ConsoleRenderer(int windowWidth, int windowHeight)
        {
            this.WindowHeight = windowHeight;
            this.WindowWidth = windowWidth;
        }

        /// <summary>
        /// Gets the window width.
        /// </summary>
        /// <value> A normal integer. </value>
        public int WindowWidth
        {
            get;
        }

        /// <summary>
        /// Gets the window height.
        /// </summary>
        /// <value> A normal integer. </value>
        public int WindowHeight
        {
            get;
        }

        /// <summary>
        /// This method print a message.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="MessageContainerEventArgs"/>. </param>
        public void PrintMessage(object sender, MessageContainerEventArgs e)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(e.MessageContainer.Message);
            Console.ResetColor();
        }

        /// <summary>
        /// This method print a error message.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="MessageContainerEventArgs"/>. </param>
        public void PrintErrorMessage(object sender, MessageContainerEventArgs e)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e.MessageContainer.Message);
            Console.ResetColor();
        }

        /// <summary>
        /// This method print a game objects and information.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="container"> The <see cref="ObjectPrintEventArgs"/>. </param>
        public void PrintGameObjectsAndInfo(object sender, ObjectPrintEventArgs container)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write("                              ");
            Console.SetCursorPosition(0, 0);
            Console.Write("Points: " + container.ObjectPrintContainer.Information.Points);
            Console.SetCursorPosition(0, 1);
            Console.Write("                              ");
            Console.SetCursorPosition(0, 1);
            Console.Write("Snake Length: " + container.ObjectPrintContainer.Information.SnakeLength);

            foreach (var element in container.ObjectPrintContainer.OldItems)
            {
                Console.SetCursorPosition(element.PosInField.X + 1, element.PosInField.Y + 3);
                Console.Write(" ");
            }

            foreach (var element in container.ObjectPrintContainer.NewItems)
            {
                Console.SetCursorPosition(element.PosInField.X + 1, element.PosInField.Y + 3);
                Console.ForegroundColor = element.Color;
                Console.Write(element.ObjectChar);
                Console.ResetColor();
            }
        }

        /// <summary>
        /// This method prints a field.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="container"> The <see cref="FieldMessageEventArgs"/>. </param>
        public void PrintField(object sender, FieldMessageEventArgs container)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();

            for (int i = 0; i < container.FieldPrintContainer.Height; i++)
            {
                for (int j = 0; j < container.FieldPrintContainer.Width; j++)
                {
                    if (i == 0 || i == container.FieldPrintContainer.Height - 1)
                    {
                        Console.Write(container.FieldPrintContainer.Symbol);
                    }
                    else if (j == 0 || j == container.FieldPrintContainer.Width - 1)
                    {
                        Console.Write(container.FieldPrintContainer.Symbol);
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// This method print the user input.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="MessageContainerEventArgs"/>. </param>
        public void PrintUserInput(object sender, MessageContainerEventArgs e)
        {
            Console.SetCursorPosition(0, 1);
            Console.Write(e.MessageContainer.Message);
        }

        /// <summary>
        /// This method deletes the user input.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        public void DeleteUserInput(object sender, EventArgs e)
        {
            Console.SetCursorPosition(Console.CursorLeft - 1, 1);
            Console.Write(" ");
            Console.SetCursorPosition(Console.CursorLeft - 1, 1);
        }
    }
}