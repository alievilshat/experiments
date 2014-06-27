using System;
using System.Windows;
using System.Windows.Controls;
using Npgsql;

namespace ScriptModule.Designers.XsltScriptDesigner.SchemaDesigner.Wizard.Database
{
    /// <summary>
    /// Interaction logic for ConnectionSettings.xaml
    /// </summary>
    public partial class ConnectionSettings : Page, IConnectionSettings
    {
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
            
            
            Server = ConnectionSetting.Default.Server;
            Login = ConnectionSetting.Default.Username;
            Database = ConnectionSetting.Default.Database;
            
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            ConnectionSetting.Default.Server=Server;
            ConnectionSetting.Default.Username = Login;
            ConnectionSetting.Default.Database=   Database ;
            ConnectionSetting.Default.Port=Port;
            ConnectionSetting.Default.Timeout=Timeout;
            ConnectionSetting.Default.Encription = Encription;
            ConnectionSetting.Default.Save();
            this.NavigationService.Navigate(new DatabaseImportType(this));
            
        }

        private void Test_connection_Click(object sender, RoutedEventArgs e)
        {
            if (!(Server == string.Empty) && !(Login == string.Empty) && !(LoginPassword == string.Empty) && !(Database == string.Empty))
            {
                CheckConnection();
            }
            else 
                MessageBox.Show("Field can not be empty");
       
        }

        public string GetConnectionString()
        {
            return String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};",
                          Server, Port, Login, LoginPassword, Database);
        }

        public void CheckConnection()
        {
            try
            {
                using (var conn = new NpgsqlConnection(GetConnectionString()))
                {
                    conn.Open();
                }
                
               MessageBox.Show("Connection Succeded");
            }
            catch (Exception e)
            {
                MessageBox.Show("Connection Error: " + e.Message);
            }
        }
    }
}
