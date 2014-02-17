using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Schema;

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
            var parent = (XmlSchemaElement) schemaTree.SelectedItem;
            if (parent != null)
            {
                var schema = parent.SchemaType as XmlSchemaComplexType;
                if (schema == null)
                    parent.SchemaType = schema = new XmlSchemaComplexType();

                XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute { Name = "New Attribute" };
                schema.Attributes.Add(xmlSchemaAttribute);
                updateTargets();
            }
        }

        private void addelem_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
