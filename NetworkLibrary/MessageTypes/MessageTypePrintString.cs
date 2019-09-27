using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkLibrary
{
    public class MessageTypePrintString : IMessageType
    {
        public int Id
        {
            get
            {
                return 2;
            }
        }

        public string Descreption
        {
            get
            {
                return "A message type for printing a text.";
            }
        }
    }
}