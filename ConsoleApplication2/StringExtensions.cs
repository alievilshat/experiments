using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    public static class StringExtensions
    {
        public static string Quote(this string value)
        {
            return string.Format("\"{0}\"", value);
        }
    }
}
