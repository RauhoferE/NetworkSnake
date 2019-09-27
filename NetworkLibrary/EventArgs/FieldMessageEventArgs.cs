using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkLibrary
{
    public class FieldMessageEventArgs
    {
        public FieldMessageEventArgs(FieldPrintContainer fieldPrintContainer)
        {
            this.FieldPrintContainer = fieldPrintContainer;
        }

        public FieldPrintContainer FieldPrintContainer
        {
            get;
        }
    }
}