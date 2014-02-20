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
using Npgsql;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for ConnectionSettings.xaml
    /// </summary>
    public partial class ConnectionSettings : Page, IConnectionSettings
    {
        NpgsqlConnection conn;
        public string Server
        {
            get { return (string)GetValue(ServerProperty); }
            set { SetValue(ServerProperty, value); }
        }
        public static readonly DependencyProperty ServerProperty =
            DependencyProperty.Register("Server", typeof(string), typeof(ConnectionSettings), new UIPropertyMetadata(string.Empty));



        public int Port
        {
            get { return (int)GetValue(PortProperty); }
            set { SetValue(PortProperty, value); }
        }
        public static readonly DependencyProperty PortProperty =
            DependencyProperty.Register("Port", typeof(int), typeof(ConnectionSettings), new UIPropertyMetadata(5432));


        public string Login
        {
            get { return (string)GetValue(LoginProperty); }
            set { SetValue(LoginProperty, value); }
        }
        public static readonly DependencyProperty LoginProperty =
            DependencyProperty.Register("Login", typeof(string), typeof(ConnectionSettings), new UIPropertyMetadata(string.Empty));


        public string LoginPassword
        {
            get { return (string)GetValue(LoginPasswordProperty); }
            set { SetValue(LoginPasswordProperty, value); }
        }
        public static readonly DependencyProperty LoginPasswordProperty =
            DependencyProperty.Register("LoginPassword", typeof(string), typeof(ConnectionSettings), new UIPropertyMetadata(string.Empty));


        public string  Database
        {
            get { return (string )GetValue(DatabaseProperty); }
            set { SetValue(DatabaseProperty, value); }
        }
        public static readonly DependencyProperty DatabaseProperty =
            DependencyProperty.Register("Database", typeof(string ), typeof(ConnectionSettings), new UIPropertyMetadata(string.Empty));


        public int Timeout
        {
            get { return (int)GetValue(TimeoutProperty); }
            set { SetValue(TimeoutProperty, value); }
        }
        public static readonly DependencyProperty TimeoutProperty =
            DependencyProperty.Register("Timeout", typeof(int), typeof(ConnectionSettings), new UIPropertyMetadata(60));



        public bool Encription
        {
            get { return (bool)GetValue(EncriptionProperty); }
            set { SetValue(EncriptionProperty, value); }
        }
        public static readonly DependencyProperty EncriptionProperty =
            DependencyProperty.Register("Encription", typeof(bool), typeof(ConnectionSettings), new UIPropertyMetadata(false));

    
        public ConnectionSettings()
        {
            InitializeComponent();

            DataContext = this;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ImportType(this));
        }

        private void Test_connection_Click(object sender, RoutedEventArgs e)
        {
            if (!(Server == string.Empty) && !(Login == string.Empty) && !(LoginPassword == string.Empty) && !(Database == string.Empty))
            {
                string connstring = String.Format("Server={0};Port={1};" +
                        "User Id={2};Password={3};Database={4};",
                        Server, Port, Login,
                        LoginPassword, Database);

                conn = new NpgsqlConnection(connstring);
                this.OpenConn();
            }
            else 
                MessageBox.Show("Field can not be empty");
       
        }

        public void OpenConn()
        {
            try
            {
                conn.Open();
                
               MessageBox.Show("Connection Succeded");
            }
            catch (Exception e)
            {
                MessageBox.Show("Connection Error");
            }
        }

        public void CloseConn()
        {
            try
            {
                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Connection Error");
            }
        }
    }
}
