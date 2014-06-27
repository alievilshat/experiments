using Npgsql;
using ScriptModule.DAL;
using ScriptModule.Services.DatabaseImport.Registries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace ScriptModule.Services.DatabaseImport
{
    public class DatabaseImporter
    {
        readonly string _login;
        readonly string _password;
        readonly string _userhostaddress;
        readonly string _providernamespace;

        NpgsqlConnection _connection;
        TableColumnRegistry _columnsRegistry;
        TableKeysRegistry _keysRegistry;
        TableRegistry _tableRegistry;
        NpgsqlTransaction _transaction;

        public event ProgressChangedEventHandler ProgressChanged;

        public DatabaseImporter(string login, string password, string userhostaddress, string providernamespace)
        {
            this._login = login;
            this._password = password;
            this._userhostaddress = userhostaddress;
            this._providernamespace = providernamespace;
        }

        public DatabaseImporter(NpgsqlConnection con)
        {
            this._connection = con;
        }

        public void Import(XmlDocument document)
        {
            if (_connection == null)
                _connection = CDBA.GetConnection(_login, _password, _userhostaddress);
            using (_connection)
            {
                DoImport(document);
            }
        }

        private void DoImport(XmlDocument document)
        {
            var doc = XDocument.Load(new XmlNodeReader(document));

            int inserted = 0;
            int updated = 0;

            string currentElement = null;

            try
            {
                OnProgressChanged("Processing", 0);

                _connection.Open();
                _transaction = _connection.BeginTransaction();

                _tableRegistry = new TableRegistry(_connection);
                _columnsRegistry = new TableColumnRegistry(_connection);
                _keysRegistry = new TableKeysRegistry(_connection, _providernamespace);
                new TableForiegnkeysRegistry(_connection);

                var queue = new Queue<XElement>(doc.Root.Element(_connection.Database).Elements());

                int initialQueueLength = queue.Count;
                int currentStep = 0;

                var total = queue.Count;
                var processed = 0;

                while (queue.Count > 0)
                {
                    if (infiniteLoop(queue.Count, ref initialQueueLength, ref currentStep))
                        throw new ApplicationException("Missing foreign keys");

                    var element = queue.Dequeue();

                    try
                    {
                        currentElement = element.ToString();
                    }
                    catch (Exception) { currentElement = element.Name + "(contains invalid characters)"; }

                    var tablename = element.Name.ToString();
                    if (!_tableRegistry.IsTableRegistered(tablename))
                        throw new ApplicationException("There is no table with the name: " + tablename);

                    string error;
                    if (!checkColumnExistance(tablename, element, out error))
                        throw new ApplicationException("Column is missing: " + error);

                    var pk = _tableRegistry.GetPrimaryKey(tablename);
                    String pkval;
                    var found = element.Element(pk);
                    if (found != null)
                    {
                        pkval = element.Element(pk).Value;
                    }
                    else
                    {
                        throw new ApplicationException("Element contains no primary key");
                    }

                    if (!foreignKeysIdentifiersCorrect(element, tablename))
                    {
                        queue.Enqueue(element);
                        continue;
                    }

                    var key = pkval;
                    if (key.StartsWith("pk"))
                    {
                        var res = parseKey("pk", key);
                        if (res.HasValue)
                        {
                            key = res.Value.Value;
                        }
                    }
                    if (_keysRegistry.IsImportedIdRegistered(tablename, key))
                    {
                        updateRow(element, tablename, pk, key);

                        updated++;
                        OnProgressChanged("Completed", ++processed / total);
                    }
                    else
                    {
                        string insertedId = insertRow(element, tablename, pk, pkval);

                        inserted++;
                        OnProgressChanged("Completed", ++processed / total);

                        var parseResult = parseKey("pk", pkval);
                        if (parseResult.HasValue)
                            pkval = parseResult.Value.Value;

                        _keysRegistry.RegisterKey(tablename, pkval, insertedId, _connection);
                    }
                }
                CommitTransaction();

                OnProgressChanged(string.Format("Inserted: {0}, Updated: {1}", inserted, updated), 100);
            }
            catch (Exception e)
            {
                _transaction.Rollback();
                throw new Exception("Error: " + e.Message + "\nElement: " + currentElement, e);
            }
            finally
            {
                CloseConnection();
            }
        }

        protected virtual void CommitTransaction()
        {
            _transaction.Commit();
        }

        protected virtual void CloseConnection()
        {
            _connection.Close();
        }

        private bool infiniteLoop(int currentQueueLength, ref int initialQueueLength, ref int currentStep)
        {
            if (currentStep == initialQueueLength)
            {
                if (currentQueueLength == initialQueueLength)
                    return true;

                initialQueueLength = currentQueueLength;
                currentStep = 0;
            }

            currentStep++;
            return false;
        }

        private bool foreignKeysIdentifiersCorrect(XElement record, string tablename)
        {
            var foreignRefs = record.Elements()
                  .Select(i => i.Value)
                  .Where(i => i.StartsWith("fk"))
                  .ToArray();

            foreach (var r in foreignRefs)
            {
                var parseResult = parseKey("fk", r);
                if (!parseResult.HasValue)
                    continue;

                var key = parseResult.Value;

                var importedIdExist = _keysRegistry.IsImportedIdRegistered(key.Key, key.Value);
                if (!importedIdExist)
                    return false;
            }

            return true;
        }

        private String insertRow(XElement element, string tablename, string pk, string pkval)
        {
            var columns = element.Elements()
                .Select(i => i.Name.ToString());  // column names without primary key

            var generatePrimaryKey = pkval.StartsWith("pk:");

            if (generatePrimaryKey)
                columns = columns.Where(c => c != pk);

            var insertColumns = columns.ToArray();

            var insertValues = new string[insertColumns.Length];

            using (var commandinsert = new NpgsqlCommand("importmanager_insertrow", _connection))
            {
                commandinsert.CommandType = CommandType.StoredProcedure;
                commandinsert.Parameters.AddWithValue("_tablename", tablename);
                commandinsert.Parameters.AddWithValue("_columns", columns.Select(i => "\"" + i + "\""));

                var index = 0;
                foreach (var c in insertColumns)
                {
                    var value = element.Element(c).Value;

                    if (_columnsRegistry.GetColumnType(tablename, c) == "bytea")
                    {
                        value = DownloadFile(value);
                    }

                    //if (foriegnkeysRegistry.IsForeignKey(tablename, c))
                    //{
                    //    var refTable = foriegnkeysRegistry.GetReferencedTable(tablename, c);
                    //    value = keysRegistry.GetSystemId(refTable, value.ToString());
                    //}

                    insertValues[index] = formatValue(tablename, c, value);
                    index++;
                }

                commandinsert.Parameters.AddWithValue("_values", insertValues);
                commandinsert.Parameters.AddWithValue("_returnGeneratedId", generatePrimaryKey);

                var result = (string)commandinsert.ExecuteScalar();

                if (result == "-1")
                    throw new Exception(string.Format("Cannot insert record {0}({1} = {2})", tablename, pk, pkval));

                if (generatePrimaryKey)
                    return result.ToString();
                else
                    return pkval;
            }
        }

        private static string DownloadFile(string value)
        {
            var result = "decode('', 'hex')";
            foreach (var url in value.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries))
            {
                try
                {
                    result = DownloadData(url);
                    break;
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return result;
        }

        private static string DownloadData(string url)
        {
            using (var client = new WebClient())
            {
                var imageData = client.DownloadData(new Uri(url));

                var result = new StringBuilder(imageData.Length * 2 + 20);
                result.Append("decode('");

                foreach (byte b in imageData)
                {
                    result.AppendFormat("{0:x2}", b);
                }
                result.Append("', 'hex')");

                return result.ToString();
            }
        }

        private string getFkFromString(string value)
        {
            var parseResult = parseKey("fk", value);
            if (parseResult.HasValue)
            {
                value = _keysRegistry.GetSystemId(parseResult.Value.Key, parseResult.Value.Value);
            }
            return value;
        }

        public KeyValuePair<string, string>? parseKey(string prefix, string value)
        {
            var match = Regex.Match(value, @"^\s*" + prefix + @":(?<tablename>[\w\d_]+)\((?<value>.+)\)\s*$");

            if (!match.Success)
                return null;

            return new KeyValuePair<string, string>(match.Groups["tablename"].Value, match.Groups["value"].Value);
        }

        private void updateRow(XElement element, string tablename, string pk, string pkval)
        {
            var systempk = _keysRegistry.GetSystemId(tablename, pkval);

            var columns = element.Elements()
                .Select(i => i.Name.ToString())
                .Where(i => i != pk)
                //.Where(i => columnsRegistry.GetColumnType(tablename, i) != "bytea")
                .ToArray();     // column names without primary key and images (images are temporarily excluded from update for the performance reasons)

            string[] updvalues = new string[columns.Length];
            using (var commandupdate = new NpgsqlCommand("importmanager_updaterow", _connection))
            {
                commandupdate.CommandType = CommandType.StoredProcedure;
                commandupdate.Parameters.AddWithValue("_tablename", tablename);
                commandupdate.Parameters.AddWithValue("_columns", columns.Select(i => "\"" + i + "\""));

                int index = 0;
                foreach (var c in columns)
                {
                    var value = element.Element(c).Value;

                    // Commented out due to performance issues
                    if (_columnsRegistry.GetColumnType(tablename, c) == "bytea")
                    {
                        value = DownloadFile(value);
                    }

                    //if (foriegnkeysRegistry.IsForeignKey(tablename, c))
                    //{
                    //    var refTable = foriegnkeysRegistry.GetReferencedTable(tablename, c);
                    //    value = keysRegistry.GetSystemId(refTable, value.ToString());
                    //}

                    updvalues[index] = formatValue(tablename, c, value);
                    index++;
                }

                commandupdate.Parameters.AddWithValue("_values", updvalues);
                commandupdate.Parameters.AddWithValue("_updpk", systempk);
                commandupdate.Parameters.AddWithValue("_updpkcol", pk);

                commandupdate.ExecuteNonQuery();
            }
        }

        private string formatValue(string table, string column, string value)
        {
            if (value.Contains("fk:"))
                return getFkFromString(value);

            if (value == string.Empty)
            {
                var type = _columnsRegistry.GetColumnType(table, column);
                if (type != "character varying" && type != "text")
                    return "NULL";
            }

            return value;
        }

        private bool checkColumnExistance(string tablename, XElement element, out string error)
        {
            error = null;
            foreach (var c in element.Elements().Select(i => i.Name.LocalName))
            {
                if (!_columnsRegistry.IsColumnExists(tablename, c))
                {
                    error = "Column " + c + " does not exist";
                    return false;
                }
            }
            return true;
        }


        private void OnProgressChanged(string userState, int progressPercentage)
        {
            if (ProgressChanged != null)
                ProgressChanged(this, new ProgressChangedEventArgs(progressPercentage, userState));
        }
    }
}
