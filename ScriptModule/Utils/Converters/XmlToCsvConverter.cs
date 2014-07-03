using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace ScriptModule
{
    public class XmlToCsvConverter
    {
        public static string ConvertXmlToCsv(XDocument doc)
        {
            return ConvertXmlToCsv(doc, ";");
        }
        public static string ConvertXmlToCsv(XDocument doc, string separator)
        {
            var sb = new StringBuilder();
            foreach (var e in doc.Root.Elements())
            {
                foreach (var i in e.Elements())
                {
                    sb.Append(i.Value + separator);
                }
                sb.Remove(sb.Length - 1, 1);
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}