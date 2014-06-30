using System.Collections.Generic;
using ScriptModule.DAL;
using ScriptModule.Scripts;
using ScriptModule.Utils.Collections;
using ScriptModuleModel;
using System.Linq;
using System.Collections.ObjectModel;
using AvalonDock;
using System;
using System.Threading;
using System.Windows.Threading;

namespace ScriptModule.ViewModels
{
    public class ScriptModuleViewModel : ViewModelBase
    {
        private ScriptManager _scriptManager;
        private ScriptModuleEntities _entities;

        private ObservableCollection<object> _messages = new ObservableCollection<object>();
        public ObservableCollection<object> Messages
        {
            get { return _messages; }
            set { _messages = value; OnPropertyChanged("Messages"); }
        }

        private ObservableCollection<ScriptRowViewModel> _rootScipts;
        public ObservableCollection<ScriptRowViewModel> RootScripts
        {
            get { return _rootScipts; }
            set { _rootScipts = value; OnPropertyChanged("RootScripts"); }
        }

        private ManagedContent _current;
        public ManagedContent Current
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
            return Current != null;
        }

        internal void Execute()
        {
            if (Current != null)
                Execute((ScriptRowViewModel)Current.DataContext);
            return;
        }

        public void Execute(ScriptRowViewModel scriptrowviewmodel)
        {
            var script = ScriptBase.GetScript(scriptrowviewmodel.ScriptText);
            script.ProgressChanged += (o, e) =>
                {
                    Dispatcher.CurrentDispatcher.Invoke(() =>
                        {
                            Messages.Add(string.Format("{0}: [{1}] {2}",
                                DateTime.Now.ToString("YYYY.MM.DD hh:mm:ss"),
                                e.ProgressPercentage,
                                e.UserState));
                        });
                };
            var thread = new Thread(() => script.Execute());
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
