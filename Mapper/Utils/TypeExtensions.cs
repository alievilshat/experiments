using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    public static class TypeExtensions
    {
        public static T As<T>(this object obj)
            where T: class
        {
            return obj as T;
        }
    }
}
