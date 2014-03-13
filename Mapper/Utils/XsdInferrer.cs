using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Mapper
{
    public class XsdInferrer
    {
        public static XmlSchema InferXsdFromXml(XDocument doc)
        {
            XmlReader reader = doc.CreateReader();
            XmlSchemaSet schemaSet = new XmlSchemaSet(); 
            XmlSchemaInference inference = new XmlSchemaInference(); 
            schemaSet = inference.InferSchema(reader);

            var schema = schemaSet.Schemas().Cast<XmlSchema>().First();

            return schema;
        }

        public static XmlSchema InferXsdFromCsv(string text)
        {
            var xml = CsvToXmlConverter.ConvertCsvToXml(text);
            return InferXsdFromXml(xml);
        }
    }
}