using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkLibrary
{
    public class MessageContainerEventArgs
    {
        public MessageContainerEventArgs(MessageContainer messageContainer)
        {
            this.MessageContainer = messageContainer;
        }
        
        public MessageContainer MessageContainer
        {
            get;
        }
    }
}