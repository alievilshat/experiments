using System;
using Npgsql;
using ScriptModule.DAL;
using ScriptModule.Services.DatabaseImport;
using System.Xml;
using ScriptModule.Utils;

namespace ScriptModule.Scripts.Output
{
    class OutputDatabase : ScriptBase
    {
        private string _query;
        public string Query
        {
            get { return _query; }
            set { _query = value; OnPropertyChanged("Query"); }
        }

        private string _login;
        public string Login
        {
            get { return _login; }
            set { _login = value; OnPropertyChanged("Login"); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged("Password"); }
        }

        private string _sourceName;
        public string SourceName
        {
            get { return _sourceName; }
            set { _sourceName = value; OnPropertyChanged("SourceName"); }
        }

        private bool _useAppConnection;
        public bool UseAppConnection
        {
            get { return _useAppConnection; }
            set { _useAppConnection = value; OnPropertyChanged("UseAppConnection"); }
        }

        protected override object ExecuteScript(object param)
        {
            var doc = param.As<XmlDocument>();
            if (doc == null)
                throw new Exception("Input parameter of InputDatabase script is not XmlDocument");
            var importer = createDatabaseImporter();
            importer.Import(doc);
            return doc;
        }

        private DatabaseImporter createDatabaseImporter()
        {
            if (UseAppConnection)
                return new DatabaseImporter(new NpgsqlConnection(AppConnectionString.Default));
            return new DatabaseImporter(Login, Password, string.Empty, SourceName);
        }
    }
}
