using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Schema;
using Npgsql;

namespace Mapper
{
    /// <summary>
    /// Interaction logic for ImportTables.xaml
    /// </summary>
    public partial class ImportTables : Page
    {
        public class TableItem : ViewModelBase
        {
            private string _name;
            public string Name 
            {
                get { return _name; }
                set { _name = value; OnPropertyChanged("Name"); }
            }

            private bool _selected;
            public bool Selected 
            {
                get { return _selected; }
                set { _selected = value; OnPropertyChanged("Selected"); }
            }

            private bool _visible;
            public bool Visible 
            {
                get { return _visible; }
                set { _visible = value; OnPropertyChanged("Visible"); }
            }
        }

        private IConnectionSettings _connectionSettings;

        public ImportTables(IConnectionSettings connectionSettings)
        {
            DataContext = this;
            this._connectionSettings = connectionSettings;

            InitializeComponent();
        }

        public string FilterText
        {
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }
        public static readonly DependencyProperty FilterTextProperty =
            DependencyProperty.Register("FilterText", typeof(string), typeof(ImportTables), new PropertyMetadata("", FilterChanged));

        public List<TableItem> Tables 
        {
            get { return (List<TableItem>)GetValue(TablesProperty); }
            set { SetValue(TablesProperty, value); }
        }
        public static readonly DependencyProperty TablesProperty =
            DependencyProperty.Register("Tables", typeof(List<TableItem>), typeof(ImportTables), new UIPropertyMetadata(null, FilterChanged));

        private static void FilterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sender = (ImportTables)d;
            var tables = sender.Tables;
            var filter = sender.FilterText;

            if (tables == null)
                return;

            foreach (var t in tables)
            {
                t.Visible = string.IsNullOrEmpty(filter) || t.Name.Contains(filter);
            }
        }
  
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Tables = retrieveTables().ToList();
        }

        private IEnumerable<TableItem> retrieveTables()
        {
            using (var con = new NpgsqlConnection(_connectionSettings.GetConnectionString()))
            {
                con.Open();
                var cmd = con.CreateCommand();
                cmd.CommandText = "SELECT table_name FROM information_schema.tables WHERE table_schema='public' AND table_type='BASE TABLE' order by  table_name"; // todo: make query
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    yield return new TableItem { Name = (string)reader["table_name"] };
                }
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var tablenames = Tables.Where(i => i.Selected).Select(I => I.Name);
            
            try
            {
                var wizard = (ImportWizard)Window.GetWindow(this);
                wizard.Schema = getResultSchema(tablenames);
                wizard.DialogResult = true;
                wizard.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private XmlSchema getResultSchema(IEnumerable<string> tablenames)
        {
            using (var con = new NpgsqlConnection(_connectionSettings.GetConnectionString()))
            {
                con.Open();
                var set = new DataSet(_connectionSettings.Database);
                set.Tables.AddRange(tablenames.Select(t =>
                {
                    var cmd = con.CreateCommand();
                    cmd.CommandText = "select * from " + t + " limit 0";
                    var reader = cmd.ExecuteReader();
                    var dt = new DataTable(t);
                    dt.Load(reader);
                    return dt;
                }).ToArray());

                using (var stream = new MemoryStream())
                {
                    set.WriteXmlSchema(stream);
                    stream.Seek(0, SeekOrigin.Begin);
                    var schema = XmlSchema.Read(stream, (o, e) => Console.WriteLine(e.Message));
                    addAnnotations(schema);
                    return schema;
                }
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

            XmlDocument xmldocument= new XmlDocument();
            var createNode = (Func<string, string, XmlElement>)((name, value) =>
            {
                var res = xmldocument.CreateElement(name);
                res.InnerText = value;
                return res;
            });

            info.Markup = new[] {
                createNode("Type", "Database"),
                createNode("Server", _connectionSettings.Server),
                createNode("port", _connectionSettings.Port.ToString()),
                createNode("username", _connectionSettings.Login),
                createNode("password", _connectionSettings.LoginPassword),
                createNode("database", _connectionSettings.Database)
            };

            root.MaxOccursString = "unbounded";
            var items = root.SchemaType.As<XmlSchemaComplexType>().Particle.As<XmlSchemaGroupBase>().Items;
            if (items == null)
                return;

            foreach (var i in items.Cast<XmlSchemaElement>())
            {
                i.MaxOccursString = "unbounded";
            }
        }
    }
}