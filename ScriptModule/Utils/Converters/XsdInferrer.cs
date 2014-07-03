using System.Linq;
using System.Xml;
using System.Xml.Schema;

namespace ScriptModule.Utils.Converters
{
    public class XsdInferrer
    {
        public static XmlSchema InferXsdFromXml(XmlDocument doc)
        {
            var reader = new XmlNodeReader(doc);
            var inference = new XmlSchemaInference(); 
            var schemaSet = inference.InferSchema(reader);

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