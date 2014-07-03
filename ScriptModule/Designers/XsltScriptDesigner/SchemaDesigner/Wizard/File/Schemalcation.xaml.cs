using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Schema;
using Microsoft.Win32;

namespace ScriptModule.Designers.XsltScriptDesigner.SchemaDesigner.Wizard.File
{
    /// <summary>
    /// Interaktionslogik für Schemalcation.xaml
    /// </summary>
    public partial class Schemalcation : Page
    {
        public Schemalcation()
        {
            InitializeComponent();
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new OpenFileDialog()
                {
                    Title = "Choose XSD File",
                    Filter = "Xsd files (*.xsd)|*.xsd|All files|*.*"
                };
                if (dialog.ShowDialog().GetValueOrDefault())
                {
                    location.Text = dialog.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var wizard = (ImportWizard)Window.GetWindow(this);

                using (var stream = System.IO.File.OpenRead(location.Text))
                {
                    var xsd = XmlSchema.Read(stream, (o, ea) => Console.WriteLine(ea.Message));
                    addAnnotations(xsd, "xml");
                    wizard.Schema = xsd;
                }

                wizard.DialogResult = true;
                wizard.Close();
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
                createNode("ContentType",filetype),
                createNode("location",""),
                createNode("Context","local")
            };
        }
    }
}
