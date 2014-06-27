using ScriptModule.DAL;

namespace ScriptModule.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _login;
        public string Login
        {
            get { return _login; }
            set { _login = value; OnPropertyChanged("Login"); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged("Password"); }
        }

        private string _error;
        public string Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged("Error"); }
        }


        private ScriptManager _scriptManager;
        public ScriptManager GetScriptManager()
        {
            return _scriptManager;
        }

        public bool TryInitializeConnection()
        {
            try
            {
                _scriptManager = new ScriptManager(Login, Password);
                return true;
            }
            catch
            {
                Error = "Please check your username and password";
                return false;
            }
        }
    }
}
