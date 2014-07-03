using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Schema;

namespace ScriptModule.Designers.XsltScriptDesigner.SchemaDesigner.Wizard.Webservice
{
    /// <summary>
    /// Interaktionslogik für WebServiceCall.xaml
    /// </summary>
    public partial class WebServiceCall : Page
    {
        public WebServiceCall()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                XmlSchema schema = new XmlSchema();

                XmlSchemaElement firstname = new XmlSchemaElement();
                firstname.Name = "WebServiceCall";
                schema.Items.Add(firstname);
                addAnnotations(schema);
                var wizard = (ImportWizard)Window.GetWindow(this);
                wizard.Schema = schema;
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
            var contenttype="";
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
            if (xml.IsChecked==true)
               contenttype  ="xml";
            else
                contenttype="csv";

            info.Markup = new[] {
                createNode("Type", "WebService"),
                createNode("ContentType", contenttype),
               
            };
        }
    }
}
