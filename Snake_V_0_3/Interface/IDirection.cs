using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake_V_0_3
{
    public interface IDirection
    {
        int ID { get; }
        string Name { get; }
    }
}