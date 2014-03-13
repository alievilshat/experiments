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

namespace Mapper
{
    /// <summary>
    /// Interaktionslogik für ImportType.xaml
    /// </summary>
    public partial class DatabaseImportType : Page
    {
        private IConnectionSettings _connectionSettings;

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
