using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkLibrary
{
    public class MessageTypePrintObject : IMessageType
    {
        public int Id
        {
            get
            {
                return 4;
            }
        }

        public string Descreption
        {
            get
            {
                return "A message type for printing an object.";
            }
        }
    }
}