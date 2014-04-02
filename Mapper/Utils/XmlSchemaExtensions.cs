using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Mapper
{
    public static class XmlSchemaExtensions
    {
        public static IEnumerable<XmlSchemaObject> Path(this XmlSchemaObject e)
        {
            var elements = new Stack<XmlSchemaObject>();
            elements.Push(e);
            while (e.Parent != null)
            {
                e = e.Parent;
                elements.Push(e);
            }

            return elements;
        }
    }
}
