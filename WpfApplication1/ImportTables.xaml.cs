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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for ImportTables.xaml
    /// </summary>
    public partial class ImportTables : Page
    {
        private IConnectionSettings settings;

        public ImportTables()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.NavigationService.LoadCompleted += NavigationService_LoadCompleted;
        }

        void NavigationService_LoadCompleted(object sender, NavigationEventArgs e)
        {
            settings = (IConnectionSettings)e.ExtraData;
        }
    }
}
