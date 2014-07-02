using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ScriptModule.Designers.CompositeScriptDesigner.ViewModels;
using ScriptModule.Scripts;
using ScriptModule.Utils;

namespace ScriptModule.Designers.CompositeScriptDesigner
{
    /// <summary>
    /// Interaction logic for CompositeScriptDesignerControl.xaml
    /// </summary>
    public partial class CompositeScriptDesignerControl : DesignerControl
    {
        private CompositeScript _script;

        private CompositeScriptViewModel Model
        {
            get { return (CompositeScriptViewModel) DataContext; }
        }
        public CompositeScriptDesignerControl(CompositeScript script)
        {
            this._script = script;
            DataContext = new CompositeScriptViewModel(script);
            InitializeComponent();
        }

        private void OpenDesigner_Click(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount != 2)
                return;

            var viewModel = ((FrameworkElement) sender).DataContext as CompositeScriptItemViewModel;
            if (viewModel != null)
            {
                var designer = viewModel.ScriptDesigner;
                ShowDesigner(designer, viewModel.ScriptName);
            }
        }

        private void MoveUp(object sender, RoutedEventArgs e)
        {
            var item = sender.As<FrameworkElement>().DataContext.As<CompositeScriptItemViewModel>();
            Model.MoveUp(item);
        }

        private void MoveDown(object sender, RoutedEventArgs e)
        {
            var item = sender.As<FrameworkElement>().DataContext.As<CompositeScriptItemViewModel>();
            Model.MoveDown(item);
        }

        private void Remove(object sender, RoutedEventArgs e)
        {
            var item = sender.As<FrameworkElement>().DataContext.As<CompositeScriptItemViewModel>();
            Model.Remove(item);
        }
    }
}
