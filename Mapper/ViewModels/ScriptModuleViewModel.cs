using System.Collections.Generic;
using ScriptModule.DAL;
using ScriptModule.Scripts;
using ScriptModule.Utils.Collections;
using ScriptModuleModel;

namespace ScriptModule.ViewModels
{
    public class ScriptModuleViewModel : ViewModelBase
    {
        private ScriptManager _scriptManager;
        private ScriptModuleEntities _entities;

        private ViewModelObjectSet<ScriptRowViewModel, ScriptRow> _scripts;
        public ViewModelObjectSet<ScriptRowViewModel, ScriptRow> Scripts
        {
            get { return _scripts; }
            set { _scripts = value; OnScriptsPropertyChanged(); }
        }

        private void OnScriptsPropertyChanged()
        {
            _rootScipts = new ObservableCollectionView<ScriptRowViewModel, ScriptRowViewModel>(_scripts, i => i.ParentId == null);
            OnPropertyChanged("Scripts");
        }

        private ObservableCollectionView<ScriptRowViewModel, ScriptRowViewModel> _rootScipts;
        public ObservableCollectionView<ScriptRowViewModel, ScriptRowViewModel> RootScripts
        {
            get { return _rootScipts; }
            set { _rootScipts = value; OnPropertyChanged("RootScripts");}
        }

        private ScriptRowViewModel _current;
        public ScriptRowViewModel Current
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
            Scripts = new ViewModelObjectSet<ScriptRowViewModel, ScriptRow>(_entities.ScriptRows);
        }

        public void NewScript()
        {
            var script = new CompositeScript();
            var row = new ScriptRow
            {
                Scriptname = "New Script " + (Scripts.Count + 1),
                Scripttext = ScriptBase.GetScriptText(script)
            };
            Scripts.Add(new ScriptRowViewModel(Scripts, row) { RenameMode = true });
        }

        public bool CanExecute()
        {
            return Current != null;
        }

        internal void Execute()
        {
            throw new System.NotImplementedException();
        }

        internal void Save()
        {
            throw new System.NotImplementedException();
        }
    }
}
