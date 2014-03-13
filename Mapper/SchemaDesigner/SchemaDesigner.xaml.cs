using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using Microsoft.Win32;
using WPG.Data;

namespace Mapper
{
    public partial class SchemaDesigner : Window
    {
        private const string FILE = "XMLSchema1.xsd";

        public XmlSchema Schema { get; set; }

        public string Code
        {
            get
            {
                if (Schema == null) return string.Empty;

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

        public SchemaDesigner()
            : this(emptySchema())
        {
        }

        public SchemaDesigner(string script)
            : this(loadSchema(script))
        {
        }

        public SchemaDesigner(XmlSchema schema)
        {
            Schema = schema;

            InitializeComponent();

            DataContext = this;
        }

        private static XmlSchema emptySchema()
        {
            var r = new XmlSchema();
            r.Items.Add(new XmlSchemaElement { Name = "root" });
            return r;
        }

        private static XmlSchema loadSchema(string script)
        {
            var reader = new StringReader(script);
            return XmlSchema.Read(reader, (o, e) => Console.WriteLine(e.Message));
        }

        private static void validationCallback(object sender, ValidationEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private bool PropertyFilter(Property p)
        {
            return p.PropertyType == typeof(string)
                || p.PropertyType == typeof(int)
                || p.PropertyType == typeof(long)
                || p.PropertyType == typeof(float)
                || p.PropertyType == typeof(double)
                || p.PropertyType == typeof(decimal);
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
            {
                designerTab.Focus();
                schemaTree.GetBindingExpression(TreeView.ItemsSourceProperty).UpdateTarget();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            updateTargets();
        }

        private void addAttr_Click(object sender, RoutedEventArgs e)
        {
            var parent = (XmlSchemaElement)schemaTree.SelectedItem;
            if (parent != null)
            {
                var xmlSchemaAttribute = new XmlSchemaAttribute { Name = "new_attribute" };
                addAttribute(parent, xmlSchemaAttribute);
            }
        }

        private void addAttribute(XmlSchemaElement parent, XmlSchemaAttribute attribute)
        {
            var schema = ensureType<XmlSchemaComplexType>(parent.SchemaType);
            parent.SchemaType = schema;
            schema.Attributes.Add(attribute);
            attribute.Parent = schema;

            var items = (IEnumerable)parent.Parent.As<dynamic>().Items;
            items.AsObservable().Update();
        }

        private void addElem_Click(object sender, RoutedEventArgs e)
        {
            var parent = (XmlSchemaElement)schemaTree.SelectedItem;
            if (parent != null)
            {
                var xmlElement = new XmlSchemaElement { Name = "new_element" };
                addChildElement(parent, xmlElement);
            }
        }

        private void addChildElement(XmlSchemaElement parent, XmlSchemaElement element)
        {
            var schema = ensureType<XmlSchemaComplexType>(parent.SchemaType);
            parent.SchemaType = schema;

            var particle = ensureType<XmlSchemaSequence>(schema.Particle);
            schema.Particle = particle;

            particle.Items.Add(element);
            element.Parent = particle;

            var items = (IEnumerable)parent.Parent.As<dynamic>().Items;
            items.AsObservable().Update();
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

        private void importItems(XmlSchemaElement parent, XmlSchema xsd)
        {
            foreach (var i in xsd.Items)
            {
                var e = i as XmlSchemaElement;
                if (e != null)
                    addChildElement(parent, e);
            }
        }

        private void deleteElem_Click(object sender, RoutedEventArgs e)
        {
            var item = (XmlSchemaElement)schemaTree.SelectedItem;
            var parent = item.Parent as XmlSchemaSequence;
            if (parent == null)
                return;
            dynamic sequence = parent.Items.AsObservable();
            sequence.Remove(item);
        }

        private void deleteAttr_Click(object sender, RoutedEventArgs e)
        {
            var attribute = (XmlSchemaAttribute)schemaTree.SelectedItem;
            var parent = (XmlSchemaComplexType)attribute.Parent;
            if (parent == null)
                throw new NotImplementedException("Cannot remove the attribute from " + parent);
            dynamic attributesCollection = parent.Attributes.AsObservable();
            attributesCollection.Remove(attribute);
        }


        private void importWizard_Click(object sender, RoutedEventArgs e)
        {
            var parent = (XmlSchemaElement)schemaTree.SelectedItem;

            try
            {
                var wizard = new ImportWizard();
                wizard.Navigate(new ImportType());

                if (wizard.ShowDialog().GetValueOrDefault())
                {
                    importItems(parent, wizard.Schema);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
