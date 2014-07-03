using System.Xml.Linq;

namespace ScriptModule.Scripts.Converters
{
    public class XmlToCsv : ScriptBase
    {
        public string Separator { get; set; }

        protected override object ExecuteScript(object param)
        {
            if (param == null)
                return null;

            XDocument doc = XDocument.Parse(param.ToString());
            return XmlToCsvConverter.ConvertXmlToCsv(doc, Separator);
        }
    }
}
