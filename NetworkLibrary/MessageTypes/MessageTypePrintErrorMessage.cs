using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkLibrary
{
    public class MessageTypePrintErrorMessage : IMessageType
    {
        public int Id
        {
            get
            {
                return 5;
            }
        }

        public string Descreption
        {
            get
            {
                return "A message type for printing an error message.";
            }
        }
    }
}