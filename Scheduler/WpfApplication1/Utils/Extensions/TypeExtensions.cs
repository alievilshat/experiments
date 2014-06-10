using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace System
{
    public static class TypeExtensions
    {
        public static T As<T>(this object obj)
            where T : class
        {
            if (obj is DynamicObject)
            {
                try
                {
                    var dobj = (dynamic)obj;
                    return (T)dobj;
                }
                catch (InvalidCastException)
                {
                    return null;
                }
            }

            return obj as T;
        }

        public static T CastAs<T>(this object obj)
        {
            if (obj is DynamicObject)
            {
                var dobj = (dynamic)obj;
                return (T)dobj;
            }

            return (T)obj;
        }
    }
}
