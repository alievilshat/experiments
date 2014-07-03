using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace ScriptModule.Utils.Converters
{
    public class CsvToXmlConverter
    {
        public static XmlDocument ConvertCsvToXml(string text)
        {
            return ConvertCsvToXml(text, "root", "record");
        }

        public static XmlDocument ConvertCsvToXml(string text, string rootName, string rowName)
        {
            var res = new XmlDocument();
            res.Load(new XDocument(new XElement(rootName, getRows(text, rowName))).CreateReader());
            return res;
        }

        private static IEnumerable<XElement> getRows(string text, string rowName)
        {
            var converter = new CsvReader(text);
            var columns = converter.ReadRow().Select(normalize).ToArray();

            string[] row;
            while ((row = converter.ReadRow()) != null)
            {
                if (row.Length == columns.Length)
                    yield return new XElement(rowName, getColumns(row, columns));
            }
        }

        private static string normalize(string val)
        {
            var res = Regex.Replace(val.Trim(), "[^A-Za-z0-9]+", "_");
            if (res.Length == 0 || !char.IsLetter(res[0]))
                res = "_" + res;
            return res;
        }

        private static IEnumerable<XElement> getColumns(string[] row, IEnumerable<string> columns)
        {
            return columns.Select((t, i) => new XElement(t, row[i]));
        }
    }
}
