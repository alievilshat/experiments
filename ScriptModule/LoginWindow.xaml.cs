using System.Windows;
using System.Windows.Controls;
using ScriptModule.ViewModels;

namespace ScriptModule
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        protected LoginViewModel Model { get { return (LoginViewModel) DataContext; } }

        public LoginWindow(LoginViewModel model)
        {
            DataContext = model;
            InitializeComponent();
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            if (Model.TryInitializeConnection())
            {
                DialogResult = true;
                Close();
            }
        }

        private void Password_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            Model.Password = ((PasswordBox) sender).Password;
        }
    }
}
