using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = (int)(DateTime.Now.Ticks & 0x7FFFFFFF);
            var t = new TimeSpan(0x7FFFFFFF);
            var j = new TimeSpan(0, 1, 0);
            Console.WriteLine(j.Ticks);

        }
    }
}
