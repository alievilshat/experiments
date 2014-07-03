using System.Configuration;
using System.Windows;
using ScriptModule.DAL;
using ScriptModule.Designers;
using ScriptModule.Properties;
using ScriptModule.Scripts;
using ScriptModule.Utils;
using ScriptModuleModel;
using System.Linq;
using System.Collections.ObjectModel;
using System;
using System.Threading;
using Xceed.Wpf.AvalonDock.Layout;

namespace ScriptModule.ViewModels
{
    public class ScriptModuleViewModel : ViewModelBase, IWindowManager
    {
        private ScriptManager _scriptManager;
        private ScriptModuleEntities _entities;

        private ObservableCollection<LayoutContent> _documents = new ObservableCollection<LayoutContent>();
        public ObservableCollection<LayoutContent> Documents
        {
            get { return _documents; }
            set { _documents = value; OnPropertyChanged("Documents"); }
        }

        private LayoutContent _activeDocument;
        public LayoutContent ActiveDocument
        {
            get { return _activeDocument; }
            set { _activeDocument = value; OnPropertyChanged("ActiveDocument"); }
        }

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

        private string _statu;
        public string Status
        {
            get { return _statu; }
            set { _statu = value; OnPropertyChanged("Status"); }
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

        public bool Iniailise()
        {
            WindowManger.SetCurrentWindowManager(this);

            var login = new LoginViewModel { Login = _login, Password = _password };

            // TODO: UNCOMMENT
            //if (!new LoginWindow(login).ShowDialog().GetValueOrDefault())
            //    return false;

            //_scriptManager = login.GetScriptManager();
            _scriptManager = new ScriptManager();
            _entities = _scriptManager.CreateEntitiesContainer();

            // TODO: FIX
            AppConnectionString.Default = "User Id=postgres;Password=Banek12;Host=85.92.146.196;Database=dubaiprint";

            return true;
        }

        public void ShowScriptWindow(ScriptRowViewModel scriptRowViewModel)
        {
            ShowWindow(scriptRowViewModel.ScriptDesigner, scriptRowViewModel.ScriptName);
        }

        public void ShowWindow(object content, string title = null)
        {
            var doc = Documents.FirstOrDefault(i => i.Content == content);
            if (doc == null)
            {
                doc = new LayoutDocument
                {
                    Title = title ?? content.GetType().Name,
                    Content = content
                };
                Documents.Add(doc);
            }
            ActiveDocument = doc;
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

        public void Execute()
        {
            if (!CanExecute())
                return;

            Execute(Current.Content.As<FrameworkElement>().DataContext.As<ScriptRowViewModel>());
        }

        private static void Invoke(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }

        public void Execute(ScriptRowViewModel scriptrowviewmodel)
        {
            var script = ScriptBase.GetScript(scriptrowviewmodel.ScriptText);

            script.ProgressChanged += (o, e) =>
            {
                Invoke(() => Output.Add(string.Format("{0} [{1}%] {2}", getTimestamp(), e.ProgressPercentage, e.UserState)));
            };

            var thread = new Thread(() =>
            {
                try
                {
                    var res = script.Execute();
                    Invoke(() => Output.Add(string.Format("{0} [100%] Result: {1}", getTimestamp(), res)));
                }
                catch (Exception ex)
                {
                    Invoke(() => Errors.Add(string.Format("{0} {1}", getTimestamp(), ex.Message)));
                    WindowManger.Current.SetStatus("ERROR");
                }
            });
                
            thread.Start();
        }

        private static string getTimestamp()
        {
            return DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        }

        public void Save()
        {
            foreach (var s in RootScripts)
            {
                s.SaveChanges();
            }
            _entities.SaveChanges();
        }

        public void AddScriptToDesigner(IScript script)
        {
            if (script == null || ActiveDocument == null)
                return;

            var control = ActiveDocument.Content.As<DesignerControl>();
            if (control == null)
                return;
            
            control.AddScript(script);
        }

        internal void AddScriptToDesigner(Type type)
        {
            var script = Activator.CreateInstance(type).As<IScript>();
            AddScriptToDesigner(script);
        }

        public void SetStatus(string text)
        {
            Status = text;
        }

        public void ShowException(Exception ex)
        {
            Errors.Add(ex.ToString());
        }
    }
}