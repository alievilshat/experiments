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
                DataTable datatable = new DataTable();
                datatable.Load(reader);

                using (MemoryStream stream = new MemoryStream())
                {
                    datatable.WriteXmlSchema(stream);
                    stream.Seek(0, SeekOrigin.Begin);
                    var schema = XmlSchema.Read(stream, (o, e) => Console.WriteLine(e.Message));
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

    }
}
