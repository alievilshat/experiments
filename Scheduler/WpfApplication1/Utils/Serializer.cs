using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;
using System.IO;


namespace WpfApplication1
{
    class Serializer
    {
        public static string Serialize(object obj) {
            
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            StringWriter stringWriter = new StringWriter();
            serializer.Serialize(stringWriter, obj);
            return stringWriter.ToString();
        }

        public static T Deserialize<T>(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(new StringReader(xml));
        }
    }
}
