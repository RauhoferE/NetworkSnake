using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkLibrary
{
    public interface IMessageType
    {
        int Id
        {
            get;
        }

        string Descreption
        {
            get;
        }
    }
}