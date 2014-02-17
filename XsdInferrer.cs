using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace NaitonStore.App_Code.Utilites
{
    public class XsdInferrer
    {
        public static XDocument InferXsdFromXml(XDocument doc)
        {
            XmlReader reader = doc.CreateReader();
            XmlSchemaSet schemaSet = new XmlSchemaSet(); 
            XmlSchemaInference inference = new XmlSchemaInference(); 
            schemaSet = inference.InferSchema(reader);

            var schema = schemaSet.Schemas().Cast<XmlSchema>().First();

            using (var mem = new MemoryStream())
            {
                schema.Write(mem);
                mem.Position = 0;

                return XDocument.Load(mem);
            }
        }

        public static XDocument InferXsdFromCsv(string text)
        {
            var xml = CsvToXmlConverter.ConvertCsvToXml(text);
            return InferXsdFromXml(xml);
        }
    }
}