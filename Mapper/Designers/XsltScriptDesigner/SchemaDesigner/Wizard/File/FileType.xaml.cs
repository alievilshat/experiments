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

namespace ScriptModule
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
