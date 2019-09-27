using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeClientConsole
{
    using NetworkLibrary;

    public class ConsoleRenderer : IRenderer
    {
        public ConsoleRenderer(int windowWidth, int windowHeight)
        {
            this.WindowHeight = windowHeight;
            this.WindowWidth = windowWidth;
        }

        public int WindowWidth
        {
            get;
        }

        public int WindowHeight
        {
            get;
        }

        public void PrintMessage(object sender, MessageContainerEventArgs e)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(e.MessageContainer.Message);
            Console.ResetColor();
        }

        public void PrintErrorMessage(object sender, MessageContainerEventArgs e)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e.MessageContainer.Message);
            Console.ResetColor();
        }

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

        public void PrintUserInput(object sender, MessageContainerEventArgs e)
        {
            Console.SetCursorPosition(0, 1);
            Console.Write(e.MessageContainer.Message);
        }

        public void DeleteUserInput(object sender, EventArgs e)
        {
            Console.SetCursorPosition(Console.CursorLeft - 1, 1);
            Console.Write(" ");
            Console.SetCursorPosition(Console.CursorLeft - 1, 1);
        }
    }
}