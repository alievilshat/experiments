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
        public class TableItem
        {
            public string Name { get; set; }
            public bool Selected { get; set; }
        }

        private IConnectionSettings _connectionSettings;

        public ImportTables(IConnectionSettings connectionSettings)
        {
            InitializeComponent();
            DataContext = this;
            this._connectionSettings = connectionSettings;
        }


        public List<TableItem> Tables 
        {
            get { return (List<TableItem>)GetValue(TablesProperty); }
            set { SetValue(TablesProperty, value); }
        }
        public static readonly DependencyProperty TablesProperty =
            DependencyProperty.Register("Tables", typeof(List<TableItem>), typeof(ImportTables), new UIPropertyMetadata(null));


  
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Tables = new List<TableItem> { new TableItem { Name = "a" }, new TableItem { Name = "b" }, new TableItem { Name = "c" } };
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var tables = Tables.Where(i => i.Selected);
        }
    }
}
