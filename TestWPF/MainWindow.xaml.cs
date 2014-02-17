using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public string Data
        {
            get { return (string)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }
        public static DependencyProperty DataProperty = DependencyProperty.Register("Data", typeof(string), typeof(MainWindow), null);

        private static Point EMPTY = new Point();
        public Point StartPoint
        {
            get { return (Point)GetValue(LocationProperty); }
            set { SetValue(LocationProperty, value); }
        }
        public static DependencyProperty LocationProperty = DependencyProperty.Register("StartPoint", typeof(Point), typeof(MainWindow), new PropertyMetadata(EMPTY));
        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void KeyDown(object sender, KeyEventArgs e)
        {
            var rand = new Random();
            StartPoint = new Point(rand.Next(255), rand.Next(255));
            Data += e.Key;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StartPoint = new Point(10, 10);
        }
    }
}
