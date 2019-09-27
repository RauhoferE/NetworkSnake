using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkLibrary
{
    public interface IInputType
    {
        int Id { get;}
        string Description { get; }
    }
}