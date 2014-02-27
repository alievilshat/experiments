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
using System.Xml.Schema;
using System.Xml;

namespace SchemaEditor
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
