using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkLibrary
{
    public class MessageTypePrintInformation : IMessageType
    {
        public int Id
        {
            get
            {
                return 6;
            }
        }

        public string Descreption
        {
            get
            {
                return "A message type for points and the length of the snake.";
            }
        }
    }
}