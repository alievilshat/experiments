using System.Linq;
using System.Windows.Input;
using ScriptModule.ViewModels;
using System.Windows;
using AvalonDock;

namespace ScriptModule
{
    /// <summary>
    /// Interaction logic for ScriptModule.xaml
    /// </summary>
    public partial class ScriptModuleWindow : Window
    {
        public ScriptModuleViewModel Model { get { return (ScriptModuleViewModel) DataContext; } }

        public ScriptModuleWindow()
            : this(new ScriptModuleViewModel())
        { }

        public ScriptModuleWindow(string login, string password)
            : this(new ScriptModuleViewModel(login, password))
        { }

        public ScriptModuleWindow(ScriptModuleViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        private void ScriptModule_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!Model.Iniailzise())
            {
                Close();
                return;
            }
            Model.LoadScripts();
        }

        private void NewScript_Click(object sender, ExecutedRoutedEventArgs e)
        {
            Model.NewScript();
        }

        private void Save_Click(object sender, ExecutedRoutedEventArgs e)
        {
            Model.Save();
        }

        private void Run_Click(object sender, ExecutedRoutedEventArgs e)
        {
            Model.Execute();
        }

        private void CanRun_Handler(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model.CanExecute();
        }

        private void Close_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void ScriptItem_Click(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount != 2)
                return;

            var context = ((FrameworkElement)sender).DataContext as ScriptRowViewModel;
            if (context == null)
                return;

            var doc = dockManager.Documents.FirstOrDefault(i => i.Content == context);
            if (doc != null)
            {
                doc.Activate();
                return;
            }
            var documentContent = new DocumentContent();
            documentContent.Title = context.ScriptName;
            documentContent.Show(dockManager);
        }
    }
}
