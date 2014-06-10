using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Text.RegularExpressions;

namespace ScriptModule

{
    public class CsvToXmlConverter
    {
        public static XDocument ConvertCsvToXml(string text)
        {
            return ConvertCsvToXml(text, "root", "record");
        }

        public static XDocument ConvertCsvToXml(string text, string rootName, string rowName)
        {
            return new XDocument(new XElement(rootName, getRows(text, rowName)));
        }

        private static IEnumerable<XElement> getRows(string text, string rowName)
        {
            var converter = new CsvReader(text);
            var columns = converter.ReadRow().Select(i => normalize(i)).ToArray();

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

        private static IEnumerable<XElement> getColumns(string[] row, string[] columns)
        {
            for (int i = 0; i < columns.Length; i++)
            {
                yield return new XElement(columns[i], row[i]);
            }
        }
    }
}
