using ScriptModule.Services.DatabaseImport;
using System.Xml;

namespace ScriptModule.Scripts.Output
{
    class OutputDatabase : ScriptBase
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string UserHostAddress { get; set; }
        public string ProviderNamespace { get; set; }

        protected override object ExecuteScript(object param)
        {
            var importer = new DatabaseImporter(Login, Password, UserHostAddress, ProviderNamespace);
            importer.ProgressChanged += (sender, args) => OnProgressChanged(args.UserState, args.ProgressPercentage);
            importer.Import((XmlDocument)param);
            return param;
        }
    }
}
