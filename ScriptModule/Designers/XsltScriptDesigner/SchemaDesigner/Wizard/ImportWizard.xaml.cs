using System.Windows.Navigation;
using System.Xml.Schema;

namespace ScriptModule.Designers.XsltScriptDesigner.SchemaDesigner.Wizard
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
