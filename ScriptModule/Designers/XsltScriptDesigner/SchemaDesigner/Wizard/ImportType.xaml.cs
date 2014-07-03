using System.Windows;
using System.Windows.Controls;
using ScriptModule.Designers.XsltScriptDesigner.SchemaDesigner.Wizard.Database;
using ScriptModule.Designers.XsltScriptDesigner.SchemaDesigner.Wizard.File;
using ScriptModule.Designers.XsltScriptDesigner.SchemaDesigner.Wizard.Webservice;

namespace ScriptModule.Designers.XsltScriptDesigner.SchemaDesigner.Wizard
{
    /// <summary>
    /// Interaktionslogik für ImportType.xaml
    /// </summary>
    public partial class ImportType : Page
    {
        public ImportType()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)

        {
            if (Database.IsChecked.GetValueOrDefault())
                this.NavigationService.Navigate(new ConnectionSettings());
            else if (Webservice.IsChecked.GetValueOrDefault())
                this.NavigationService.Navigate(new WebServiceCall());
            else
                this.NavigationService.Navigate(new FileType());

        }
    }
}
