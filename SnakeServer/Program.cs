using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Application app = new Application(new ConsoleRenderer(120, 30));
            app.Start();
        }
    }
}
