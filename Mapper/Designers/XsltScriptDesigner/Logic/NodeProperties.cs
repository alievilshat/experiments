using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ScriptModule
{
    class NodeProperties
    {
        public string Name { get; set; }
        public string Namespace { get; set; }
        public Dictionary<string, string> Attributes { get; set; }

        public bool Match(XmlNode e)
        {
            if (e.LocalName != Name || e.NamespaceURI != (Namespace ?? string.Empty))
                return false;

            if (Attributes != null)
            {
                foreach (var a in Attributes)
                {
                    var attr = e.Attributes[a.Key];
                    if (attr == null || attr.Value != a.Value)
                        return false;
                }
            }

            return true;
        }
    }
}
