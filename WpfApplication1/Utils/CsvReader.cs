using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WpfApplication1
{
    class CsvReader
    {
        private BinaryReader reader;
        string currentLine = "";
        string lineseparator = null;
        char? separator = null;

        public CsvReader(string csvData)
        {
            reader = new BinaryReader(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(csvData)));
        }

        public string[] ReadRow()
        {

            // ReadLine() will return null if there's no next line
            if (reader.BaseStream.Position >= reader.BaseStream.Length)
                return null;

            StringBuilder builder = new StringBuilder();

            // Read the next line
            while ((reader.BaseStream.Position < reader.BaseStream.Length) && (lineseparator == null || (!builder.ToString().EndsWith(lineseparator))))
            {
                char c = reader.ReadChar();
                builder.Append(c);

                if (lineseparator == null)
                    lineseparator = determineLineseparator(c, reader.PeekChar());
            }

            currentLine = builder.ToString();
            if (lineseparator == null)
                lineseparator = Environment.NewLine;

            if (currentLine.EndsWith(lineseparator))
                currentLine = currentLine.Remove(currentLine.IndexOf(lineseparator), lineseparator.Length);

            // Build the list of objects in the line
            var objects = new List<string>();
            while (currentLine != "")
                objects.Add(ReadNextObject());
            return objects.ToArray();
        }

        private string determineLineseparator(char c, int p)
        {
            if (c == '\n')
            {
                if (p == '\r')
                    return "\n\r";
                return "\n";
            }
            if (c == '\r')
            {
                if (p == '\n')
                    return "\r\n";
                return "\r";
            }
            return null;
        }

        private string ReadNextObject()
        {
            if (currentLine == null)
                return null;

            if (!separator.HasValue)
                separator = determineSeparator(currentLine);

            // Check to see if the next value is quoted
            bool quoted = false;
            if (currentLine.StartsWith("\""))
                quoted = true;

            // Find the end of the next value
            string nextObjectString = "";
            int i = 0;
            int len = currentLine.Length;
            bool foundEnd = false;
            while (!foundEnd && i <= len)
            {
                // Check if we've hit the end of the string
                if ((!quoted && i == len) // non-quoted strings end with a comma or end of line
                    || (!quoted && currentLine.Substring(i, 1) == separator.ToString())
                    // quoted strings end with a quote followed by a comma or end of line
                    || (quoted && i == len - 1 && currentLine.EndsWith("\""))
                    || (quoted && currentLine.Substring(i, 2) == "\"" + separator))
                    foundEnd = true;
                else
                    i++;
            }
            if (quoted)
            {
                if (i > len || !currentLine.Substring(i, 1).StartsWith("\""))
                    throw new FormatException("Invalid CSV format: " + currentLine.Substring(0, i));
                i++;
            }
            nextObjectString = currentLine.Substring(0, i).Replace("\"\"", "\"");

            if (i < len)
                currentLine = currentLine.Substring(i + 1);
            else
                currentLine = "";

            if (quoted)
            {
                if (nextObjectString.StartsWith("\""))
                    nextObjectString = nextObjectString.Substring(1);
                if (nextObjectString.EndsWith("\""))
                    nextObjectString = nextObjectString.Substring(0, nextObjectString.Length - 1);
                return nextObjectString;
            }
            else
            {
                return nextObjectString;
            }
        }

        private char determineSeparator(string currentLine)
        {
            if (currentLine.Contains(";"))
                return ';';
            if (currentLine.Contains("\t"))
                return '\t';
            return ',';
        }
    }
}
