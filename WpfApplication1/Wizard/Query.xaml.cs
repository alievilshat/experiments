using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Schema;
using System.Xml;

namespace WpfApplication1
{
    /// <summary>
    /// Interaktionslogik für Query.xaml
    /// </summary>
    public partial class Query : Page
    {
        private IConnectionSettings _connectionSettings;
        public Query(IConnectionSettings connectionSettings)
        {
            DataContext = this;
            this._connectionSettings = connectionSettings;
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private XmlSchema retrieveQueryResult()
        {
            using (var con = new NpgsqlConnection(_connectionSettings.GetConnectionString()))
            {
                con.Open();
                var cmd = con.CreateCommand();
                cmd.CommandText = QueryText.Text;
                var reader = cmd.ExecuteReader();
                var set = new DataSet(_connectionSettings.Database);
                var datatable = new DataTable("query");
                set.Tables.Add(datatable);
                datatable.Load(reader);

                using (MemoryStream stream = new MemoryStream())
                {
                    set.WriteXmlSchema(stream);
                    stream.Seek(0, SeekOrigin.Begin);
                    var schema = XmlSchema.Read(stream, (o, e) => Console.WriteLine(e.Message));
                    addAnnotations(schema);
                    return schema;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var wizard = (ImportWizard)Window.GetWindow(this);
                wizard.Schema = retrieveQueryResult();
              
                
                wizard.DialogResult = true;
                wizard.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
        }
        private void addAnnotations(XmlSchema schema)
        {
            var root = schema.Items.OfType<XmlSchemaElement>().FirstOrDefault();
            if (root == null)
                return;
            var annotation = new XmlSchemaAnnotation();
            root.Annotation = annotation;
            var info = new XmlSchemaAppInfo();
            annotation.Items.Add(info);

            XmlDocument xmldocument = new XmlDocument();
            var server = xmldocument.CreateElement("Server");
            server.InnerText = _connectionSettings.Server;
            var port = xmldocument.CreateElement("port");
            port.InnerText = _connectionSettings.Port.ToString();
            var username = xmldocument.CreateElement("username");
            username.InnerText = _connectionSettings.Login;
            var password = xmldocument.CreateElement("password");
            password.InnerText = _connectionSettings.LoginPassword;
            var database = xmldocument.CreateElement("database");
            database.InnerText = _connectionSettings.Database;
            var query = xmldocument.CreateElement("query");
            query.InnerText = QueryText.Text;
            info.Markup = new[] {
               server, port,username,password,database,query
            };
        }
    }
}
