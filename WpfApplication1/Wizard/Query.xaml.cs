using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Schema;
using Npgsql;

namespace SchemaEditor
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
            var createNode = (Func<string, string, XmlElement>)((name, value) =>
            {
                var res = xmldocument.CreateElement(name);
                res.InnerText = value;
                return res;
            });

            info.Markup = new[] {
                createNode("Type","DatabaseQuery"),
                createNode("Server", _connectionSettings.Server),
                createNode("port", _connectionSettings.Port.ToString()),
                createNode("username", _connectionSettings.Login),
                createNode("password", _connectionSettings.LoginPassword),
                createNode("database", _connectionSettings.Database),
                createNode("query", QueryText.Text)
            };
        }
    }
}
