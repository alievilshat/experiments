using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Schema;
using Microsoft.Win32;
using ScriptModule.Utils.Converters;

namespace ScriptModule.Designers.XsltScriptDesigner.SchemaDesigner.Wizard.File
{
    /// <summary>
    /// Interaktionslogik für FileLocation.xaml
    /// </summary>
    public partial class FileLocation : Page
    {
        private string _contenttype;

        public FileLocation()
        {
            InitializeComponent();
        }

        public FileLocation(string contenttype)
            :this()
        {
            this._contenttype = contenttype;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_contenttype == "xml")
                {

                    var dialog = new OpenFileDialog()
                    {

                        Title = "Choose xml File",
                        Filter = "Xml files (*.xml, *.xls)|*.xml;*.xls|All files|*.*"
                    };

                    if (dialog.ShowDialog().GetValueOrDefault())
                    {
                        location.Text = dialog.FileName;
                    }
                }
                else
                {
                    var dialog = new OpenFileDialog()
                    {

                        Title = "Choose Csv File",
                        Filter = "Csv files (*.csv)|*.csv|Text files (*.txt)|*.txt|All files|*.*"
                    };

                    if (dialog.ShowDialog().GetValueOrDefault())
                    {
                        location.Text = dialog.FileName;
                    }
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
                Debug.Assert(wizard != null, "wizard != null");

                if (local.IsChecked.GetValueOrDefault())
                {
                    if (_contenttype == "xml")
                    {

                        var source = new XmlDocument();
                        source.Load(new StringReader(location.Text));
                        var xsd = XsdInferrer.InferXsdFromXml(source);
                        addAnnotations(xsd, _contenttype, "");
                        wizard.Schema = xsd;
                       
                    }
                    else 
                    {
                        var source = location.Text;
                        var csvtext = System.IO.File.ReadAllText(source);
                        var xsd = XsdInferrer.InferXsdFromCsv(csvtext);
                        addAnnotations(xsd, _contenttype, "");
                        wizard.Schema = xsd;
                    
                    }
                       
                        
                    
                }
                else 
                { 
                    // call the remote file from URLpath text box.
                }

                wizard.DialogResult = true;
                wizard.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void addAnnotations(XmlSchema schema, string filetype ,string location)
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
                createNode("location",location),
                createNode("Context","local")
            };
        }

    }
}
