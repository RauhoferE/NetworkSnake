using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkLibrary
{
    public class MessageTypePing : IMessageType
    {
        public int Id
        {
            get
            {
                return 0;
            }
        }

        public string Descreption
        {
            get
            {
                return "A normal ping.";
            }
        }
    }
}