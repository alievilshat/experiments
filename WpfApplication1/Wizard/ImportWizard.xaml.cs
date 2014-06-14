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

namespace SchemaEditor
{
    /// <summary>
    /// Interaction logic for ImportWizard.xaml
    /// </summary>
    public partial class ImportWizard : NavigationWindow
    {
        public XmlSchema Schema { get; set; }

        public ImportWizard()
        {
            InitializeComponent();
        }

    }
}
