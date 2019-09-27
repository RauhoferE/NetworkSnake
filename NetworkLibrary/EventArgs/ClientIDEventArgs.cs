using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkLibrary
{
    public class ClientIDEventArgs
    {
        public ClientIDEventArgs(int clientId)
        {
            this.ClientID = clientId;
        }

        public int ClientID
        {
            get;
        }
    }
}