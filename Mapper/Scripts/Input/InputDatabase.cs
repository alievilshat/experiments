using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using Npgsql;
using ScriptModule.DAL;

namespace ScriptModule.Scripts.Input
{
    public class InputDatabase : ScriptBase
    {
        private bool _useAppConnection;
        public bool UseAppConnection
        {
            get { return _useAppConnection; }
            set { _useAppConnection = value; OnPropertyChanged("UseAppConnection"); }
        }

        private string _connectionString;
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; OnPropertyChanged("ConnectionString"); }
        }

        private string _query;
        public string Query
        {
            get { return _query; }
            set { _query = value; OnPropertyChanged("Query"); }
        }

        protected override object ExecuteScript(object param)
        {
            if (Query == null)
                return null;

            using (var con = new NpgsqlConnection(getConnectionString()))
            {
                con.Open();
                var table = performQuery(Query, con);

                return convertToXmlDocument(table);
            }
        }

        private static XmlDocument convertToXmlDocument(DataTable table)
        {
            var sb = new StringBuilder();
            using (var writer = new StringWriter(sb))
            {
                table.WriteXml(writer);
                var doc = new XmlDocument();
                doc.LoadXml(sb.ToString());
                return doc;
            }
        }

        private string getConnectionString()
        {
            return UseAppConnection ? AppConnectionString.Default : ConnectionString;
        }


        private static DataTable performQuery(string query, NpgsqlConnection con)
        {
            using (var cmd = con.CreateCommand())
            {
                cmd.CommandText = query;

                var table = new DataTable("result");

                using (var reader = cmd.ExecuteReader())
                {
                    table.Load(reader);
                }
                return table;
            }
        }
    }
}
