using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Schema;
using Microsoft.Win32;
using System.Xml.Linq;

namespace WpfApplication1
{
    public partial class MainWindow : Window
    {
        private const string FILE = "XMLSchema1.xsd";

        public XmlSchema Schema { get; set; }

        public string Code
        {
            get
            {
                var w = new StringWriter();
                Schema.Write(w);
                return w.ToString();
            }
            set
            {
                var r = new StringReader(value);
                Schema = XmlSchema.Read(r, validationCallback);
            }
        }

        public MainWindow()
        {
            loadSchema(FILE);

            InitializeComponent();

            DataContext = this;
        }

        private void loadSchema(string filepath)
        {
            using (var stream = File.OpenRead(filepath))
            {
                Schema = XmlSchema.Read(stream, validationCallback);
            }
        }

        private void validationCallback(object sender, ValidationEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateTargets();
        }

        private void updateTargets()
        {
            if (sourceTab.IsSelected)
            {
                sourceTab.Focus();
                schemaCode.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            }
            else
                schemaTree.GetBindingExpression(TreeView.ItemsSourceProperty).UpdateTarget();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            updateTargets();
            using (var stream = File.OpenWrite(FILE))
            {
                Schema.Write(stream);
            }
        }

        private void addattr_Click(object sender, RoutedEventArgs e)
        {
            var parent = (XmlSchemaElement)schemaTree.SelectedItem;
            if (parent != null)
            {
                var xmlSchemaAttribute = new XmlSchemaAttribute { Name = "New Attribute" };
                addAttribute(parent, xmlSchemaAttribute);

                updateTargets();
            }
        }

        private void addAttribute(XmlSchemaElement parent, XmlSchemaAttribute xmlSchemaAttribute)
        {
            var schema = ensureType<XmlSchemaComplexType>(parent.SchemaType);
            parent.SchemaType = schema;
            schema.Attributes.Add(xmlSchemaAttribute);
        }

        private void addelem_Click(object sender, RoutedEventArgs e)
        {
            var parent = (XmlSchemaElement)schemaTree.SelectedItem;
            if (parent != null)
            {
                var xmlElement = new XmlSchemaElement { Name = "New Element" };
                addChildElement(parent, xmlElement);

                updateTargets();
            }
        }

        private void addChildElement(XmlSchemaElement parent, XmlSchemaElement element)
        {
            var schema = ensureType<XmlSchemaComplexType>(parent.SchemaType);
            parent.SchemaType = schema;

            var particle = ensureType<XmlSchemaSequence>(schema.Particle);
            schema.Particle = particle;

            particle.Items.Add(element);
        }

        private T ensureType<T>(object element)
            where T : class, new()
        {
            T res = element as T;
            if (res == null)
                res = new T();

            return res;
        }

        private ContextMenuEventArgs handledContextMenuEventArgs;

        private void schemaTree_PreviewMouseRightButtonDown(object sender, ContextMenuEventArgs e)
        {
            if (handledContextMenuEventArgs != e)
            {
                handledContextMenuEventArgs = e;
                TreeViewItem item = sender as TreeViewItem;
                if (item != null)
                {
                    item.Focus();
                }
            }
        }

        private void ImportCSV_Click(object sender, RoutedEventArgs e)
        {
            var parent = (XmlSchemaElement)schemaTree.SelectedItem;
            try
            {
                var dialog = new OpenFileDialog()
                {
                    Title = "Choose Csv File",
                    Filter = "Csv files (*.csv)|*.csv|Text files (*.txt)|*.txt|All files|*.*"
                };
                if (dialog.ShowDialog().GetValueOrDefault())
                {
                    var source = dialog.FileName;
                    var csvtext = File.ReadAllText(source);
                    var xsd = XsdInferrer.InferXsdFromCsv(csvtext);

                    importItems(parent, xsd);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ImporXML_Click(object sender, RoutedEventArgs e)
        {
            var parent = (XmlSchemaElement)schemaTree.SelectedItem;
            try
            {
                var dialog = new OpenFileDialog()
                {
                    Title = "Choose Csv File",
                    Filter = "Xml files (*.xml, *.xls)|*.xml;*.xls|All files|*.*"
                };
                if (dialog.ShowDialog().GetValueOrDefault())
                {
                    var source = XDocument.Load(dialog.FileName);
                    var xsd = XsdInferrer.InferXsdFromXml(source);

                    importItems(parent, xsd);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void importItems(XmlSchemaElement parent, XmlSchema xsd)
        {
            foreach (var i in xsd.Items)
            {
                var e = i as XmlSchemaElement;
                if (e != null)
                    addChildElement(parent, e);
            }
        }

        private void deletelem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleattr_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
