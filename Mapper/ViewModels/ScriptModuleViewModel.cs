using System.Windows;
using ScriptModule.DAL;
using ScriptModule.Scripts;
using ScriptModule.Utils;
using ScriptModuleModel;
using System.Linq;
using System.Collections.ObjectModel;
using System;
using System.Threading;
using System.Windows.Threading;
using Xceed.Wpf.AvalonDock.Layout;

namespace ScriptModule.ViewModels
{
    public class ScriptModuleViewModel : ViewModelBase
    {
        private ScriptManager _scriptManager;
        private ScriptModuleEntities _entities;

        private ObservableCollection<object> _output = new ObservableCollection<object>();
        public ObservableCollection<object> Output
        {
            get { return _output; }
            set { _output = value; OnPropertyChanged("Output"); }
        }

        private ObservableCollection<string> _errors = new ObservableCollection<string>();
        public ObservableCollection<string> Errors
        {
            get { return _errors; }
            set { _errors = value; OnPropertyChanged("Errors"); }
        }

        private ObservableCollection<ScriptRowViewModel> _rootScipts;
        public ObservableCollection<ScriptRowViewModel> RootScripts
        {
            get { return _rootScipts; }
            set { _rootScipts = value; OnPropertyChanged("RootScripts"); }
        }

        private LayoutContent _current;
        public LayoutContent Current
        {
            get { return _current; }
            set { _current = value; OnPropertyChanged("Current"); }
        }

        private readonly string _login;
        private readonly string _password;

        public ScriptModuleViewModel()
        { }

        public ScriptModuleViewModel(string login, string password)
        {
            this._login = login;
            this._password = password;
        }

        public bool Iniailzise()
        {
            var login = new LoginViewModel { Login = _login, Password = _password };

            // TODO: UNCOMMENT
            //if (!new LoginWindow(login).ShowDialog().GetValueOrDefault())
            //    return false;

            //_scriptManager = login.GetScriptManager();
            _scriptManager = new ScriptManager();
            _entities = _scriptManager.CreateEntitiesContainer();
            return true;
        }

        public void LoadScripts()
        {
            RootScripts = new ObservableCollection<ScriptRowViewModel>(
                _entities.ScriptRows.Where(i => i.Parent == null)
                .ToArray().Select(i => new ScriptRowViewModel(_entities.ScriptRows, i)));
        }

        public void NewScript()
        {
            var script = new CompositeScript();
            var row = new ScriptRow
            {
                Scriptname = "New Script " + (RootScripts.Count + 1),
                Scripttext = ScriptBase.GetScriptText(script)
            };
            _entities.ScriptRows.AddObject(row);
            RootScripts.Add(new ScriptRowViewModel(_entities.ScriptRows, row) { RenameMode = true });
        }

        public bool CanExecute()
        {
            return Current is LayoutDocument 
                && Current.Content is FrameworkElement 
                && ((FrameworkElement)Current.Content).DataContext is ScriptRowViewModel;
        }

        internal void Execute()
        {
            if (!CanExecute())
                return;

            Execute(Current.Content.As<FrameworkElement>().DataContext.As<ScriptRowViewModel>());
        }

        public void Execute(ScriptRowViewModel scriptrowviewmodel)
        {
            var script = ScriptBase.GetScript(scriptrowviewmodel.ScriptText);
            script.ProgressChanged += (o, e) =>
                {
                    App.Current.Dispatcher.Invoke(() => Output.Add(string.Format("{0} [{1}%] {2}",
                                                                            DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                                                                            e.ProgressPercentage,
                                                                            e.UserState)));
                };
            var thread = new Thread(() =>
            {
                var res = script.Execute();
                App.Current.Dispatcher.Invoke(() => Output.Add(string.Format("{0} [100%] Result: {1}",
                                                                            DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                                                                            res)));
            });
            thread.Start();
        }

        public void Save()
        {
            foreach (var s in RootScripts)
            {
                s.SaveChanges();
            }
            _entities.SaveChanges();
        }
    }
}
