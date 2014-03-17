using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Xml;
using System.Xml.Schema;

namespace Mapper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public XmlDataProvider Stylesheet { get; set; }

        public MainWindow()
        {
            SourceSchema = loadSchema("source.xsd");
            TargetSchema = loadSchema("target.xsd");

            loadStylesheet();

            DataContext = this;
            InitializeComponent();

            FindResource("sourceNodePortFinder").As<NodePortFinderConverter>().Schema = sourceSchema;
            FindResource("targetNodePortFinder").As<NodePortFinderConverter>().Schema = targetSchema;
        }

        private XmlSchema loadSchema(string filename)
        {
            using (var stream = File.OpenText(filename))
            {
                return XmlSchema.Read(stream, (o, e) => Console.WriteLine(e.Message));
            }
        }

        private void loadStylesheet()
        {
            Stylesheet = new XmlDataProvider();
            Stylesheet.Document = new XmlDocument();
            Stylesheet.Document.LoadXml(File.ReadAllText("transformation.xsl"));
            Stylesheet.XmlNamespaceManager = new XmlNamespaceManager(Stylesheet.Document.NameTable);
            Stylesheet.XmlNamespaceManager.AddNamespace("xsl", "http://www.w3.org/1999/XSL/Transform");
        }

        public XmlSchema SourceSchema
        {
            get { return (XmlSchema)GetValue(SourceSchemaProperty); }
            set { SetValue(SourceSchemaProperty, value); }
        }
        public static readonly DependencyProperty SourceSchemaProperty =
            DependencyProperty.Register("SourceSchema", typeof(XmlSchema), typeof(MainWindow), new PropertyMetadata(null));

        public XmlSchema TargetSchema
        {
            get { return (XmlSchema)GetValue(TargetSchemaProperty); }
            set { SetValue(TargetSchemaProperty, value); }
        }
        public static readonly DependencyProperty TargetSchemaProperty =
            DependencyProperty.Register("TargetSchema", typeof(XmlSchema), typeof(MainWindow), new PropertyMetadata(null));

        private void saveSchema(XmlSchema schema, string filename)
        {
            using (var w = File.OpenWrite(filename))
            {
                schema.Write(w);
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            var res = MessageBox.Show("Do you want to save the changes?", "Save", MessageBoxButton.YesNoCancel);
            switch (res)
            {
                case MessageBoxResult.No:
                    return;

                case MessageBoxResult.Yes:
                    saveSchema(SourceSchema, "source.xsd");
                    saveSchema(TargetSchema, "target.xsd");
                    Stylesheet.Document.Save("transformation.xsl");
                    break;

                default:
                case MessageBoxResult.None:
                case MessageBoxResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }
    }
}