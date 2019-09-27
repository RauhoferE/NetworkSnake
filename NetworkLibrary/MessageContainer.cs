using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkLibrary
{
    [Serializable]
    public class MessageContainer
    {
        private string message;

        public MessageContainer(string message)
        {
            this.Message = message;
        }

        public string Message
        {
            get
            {
                return this.message;
            }

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Error value cant be null.");
                }

                this.message = value;
            }
        }
    }
}