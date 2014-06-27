using System.Windows;
using System.Windows.Controls;

namespace ScriptModule.Designers.XsltScriptDesigner.SchemaDesigner.Wizard.File
{
    /// <summary>
    /// Interaktionslogik für FileType.xaml
    /// </summary>
    public partial class FileType : Page
    {
        public FileType()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (xsd.IsChecked.GetValueOrDefault())
                this.NavigationService.Navigate(new Schemalcation());
            else
                this.NavigationService.Navigate(new FileLocation(xml.IsChecked.GetValueOrDefault() ? "xml" : "csv"));
        }
    }
}
