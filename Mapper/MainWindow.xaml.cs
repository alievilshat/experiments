using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

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
            loadStylesheet();

            DataContext = this;
            InitializeComponent();
        }

        private void loadStylesheet()
        {
            Stylesheet = new XmlDataProvider();
            Stylesheet.Document = new XmlDocument();
            Stylesheet.Document.LoadXml(File.ReadAllText("transformation.xsl"));
            Stylesheet.XmlNamespaceManager = new XmlNamespaceManager(Stylesheet.Document.NameTable);
            Stylesheet.XmlNamespaceManager.AddNamespace("xsl", "http://www.w3.org/1999/XSL/Transform");
        }
    }
}
