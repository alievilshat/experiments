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
