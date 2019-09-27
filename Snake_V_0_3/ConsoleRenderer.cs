using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake_V_0_3
{
    public class ConsoleRenderer
    {
        public void PrintField(PlayingField field)
        {
            Console.Clear();

            for (int i = 0; i < field.Length; i++)
            {
                for (int j = 0; j < field.Width; j++)
                {
                    if (i == 0 || i == field.Length-1)
                    {
                        Console.Write(field.Icon.Character);
                    }
                    else if (j == 0 || j == field.Width - 1)
                    {
                        Console.Write(field.Icon.Character);
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }

                Console.WriteLine();
            }
        }

        public void PrintObjects(object sender, GameOBjectListEventArgs e)
        {
            foreach (var seg in e.OldObjects)
            {
                if (seg != null)
                {
                    Console.SetCursorPosition(seg.Pos.X + 1, seg.Pos.Y + 1);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(" ");
                    Console.ResetColor();
                }

            }

            foreach (var seg in e.NewObjects)
            {
                if (seg != null)
                {
                    Console.SetCursorPosition(seg.Pos.X + 1, seg.Pos.Y + 1);
                    Console.ForegroundColor = seg.Color.ForeGroundColor;
                    Console.BackgroundColor = seg.Color.BackGroundColor;
                    Console.Write(seg.Icon.Character);
                    Console.ResetColor();
                }
            }

            Console.SetCursorPosition(0, 29);
            Console.Write(e.Score);
            Console.Write("    ");
            Console.Write(e.SnakeLength);
        }
    }
}