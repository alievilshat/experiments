
namespace ScriptModule.Scripts.Converters
{
    public class CsvToXml : ScriptBase
    {
        protected override object ExecuteScript(object param)
        {
            if (param == null)
                return null;

            return CsvToXmlConverter.ConvertCsvToXml(param.ToString()).ToString();
        }
    }
}
