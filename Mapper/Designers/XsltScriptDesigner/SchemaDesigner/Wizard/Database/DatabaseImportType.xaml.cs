using System.Windows;
using System.Windows.Controls;

namespace ScriptModule.Designers.XsltScriptDesigner.SchemaDesigner.Wizard.Database
{
    /// <summary>
    /// Interaktionslogik für ImportType.xaml
    /// </summary>
    public partial class DatabaseImportType : Page
    {
        private readonly IConnectionSettings _connectionSettings;

        public DatabaseImportType(IConnectionSettings connectionSettings)
        {
            this._connectionSettings = connectionSettings;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Query.IsChecked==true)
                 this.NavigationService.Navigate(new Query(_connectionSettings));
            else 
            if (ImportTables.IsChecked==true)
                this.NavigationService.Navigate(new ImportTables(_connectionSettings));
        }
    }
}
