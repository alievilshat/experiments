using System;
using System.Dynamic;

namespace ScriptModule.Utils
{
    public static class TypeExtensions
    {
        public static T As<T>(this object obj)
            where T: class
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
            if (obj is Reference<T>)
            {
                return ((Reference<T>)obj).Target;
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
