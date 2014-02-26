﻿using System;
using System.Collections;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Schema;
using Microsoft.Win32;
using WPG.Data;
using System.Xml;

namespace SchemaEditor
{
    public partial class MainWindow : Window
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

        public MainWindow()
            : this(loadSchema(FILE))
        {
        }

        public MainWindow(XmlSchema schema)
        {
            Schema = schema;

            InitializeComponent();

            DataContext = this;
        }

        private static XmlSchema loadSchema(string filepath)
        {
            using (var stream = File.OpenRead(filepath))
            {
                return XmlSchema.Read(stream, validationCallback);
            }
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
                var xmlSchemaAttribute = new XmlSchemaAttribute { Name = "New Attribute" };
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
                var xmlElement = new XmlSchemaElement { Name = "New Element" };
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

        private void importCSV_Click(object sender, RoutedEventArgs e)
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
                    addAnnotations(Schema, "Csv");
                    importItems(parent, xsd);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void importXML_Click(object sender, RoutedEventArgs e)
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
                    addAnnotations(Schema, "Xml");
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

        private void importXSD_Click(object sender, RoutedEventArgs e)
        {
            var parent = (XmlSchemaElement)schemaTree.SelectedItem;
            try
            {
                var dialog = new OpenFileDialog()
                {
                    Title = "Choose XSD File",
                    Filter = "Xsd files (*.xsd)|*.xsd|All files|*.*"
                };
                if (dialog.ShowDialog().GetValueOrDefault())
                {
                    var source = dialog.FileName;
                    using (var stream = File.Open(source, FileMode.Open))
                    {
                        var xsd = XmlSchema.Read(stream, validationCallback);
                        addAnnotations(Schema, "Xsd");
                        importItems(parent, xsd);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void importTables_Click(object sender, RoutedEventArgs e)
        {
            var parent = (XmlSchemaElement)schemaTree.SelectedItem;

            try
            {
                var wizard = new ImportWizard();
                var settings = new ConnectionSettings();

                wizard.Navigate(settings);

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

        private void addAnnotations(XmlSchema schema, string filetype)
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
                createNode("Type","File"),
                createNode("ContextType",filetype),
                createNode("location",""),
                createNode("Context","local")
            };
        }
    }
}
