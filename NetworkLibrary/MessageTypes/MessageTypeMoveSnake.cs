using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkLibrary
{
    public class MessageTypeMoveSnake : IMessageType
    {
        public int Id
        {
            get
            {
                return 1;
            }
        }

        public string Descreption
        {
            get
            {
                return "A message type for movement of the snake.";
            }
        }
    }
}