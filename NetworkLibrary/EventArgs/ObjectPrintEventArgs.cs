using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkLibrary
{
    public class ObjectPrintEventArgs
    {
        public ObjectPrintEventArgs(ObjectListContainer container)
        {
            this.ObjectPrintContainer = container;
        }

        public ObjectListContainer ObjectPrintContainer
        {
            get;
        }
    }
}