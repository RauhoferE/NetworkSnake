using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkLibrary
{
    public class MessageTypePrintField : IMessageType
    {
        public int Id
        {
            get
            {
                return 3;
            }
        }

        public string Descreption
        {
            get
            {
                return "A message type for printing a field.";
            }
        }
    }
}