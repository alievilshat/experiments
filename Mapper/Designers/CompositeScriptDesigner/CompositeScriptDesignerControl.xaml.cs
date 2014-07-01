using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ScriptModule.Designers.CompositeScriptDesigner.ViewModels;
using ScriptModule.Scripts;
using Xceed.Wpf.AvalonDock.Layout;

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
                ShowDesigner(designer);
            }
        }

        private void Scripts_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBoxItem)
            {
                var draggedItem = sender as ListBoxItem;
                DragDrop.DoDragDrop(draggedItem, draggedItem.DataContext, DragDropEffects.Move);
                draggedItem.IsSelected = true;
            }
        }

        private void Scripts_OnDrop(object sender, DragEventArgs e)
        {
            var droppedData = e.Data.GetData(typeof(CompositeScriptItemViewModel)) as CompositeScriptItemViewModel;
            var target = ((ListBoxItem)sender).DataContext as CompositeScriptItemViewModel;

            int removedIdx = ScriptsListView.Items.IndexOf(droppedData);
            int targetIdx = ScriptsListView.Items.IndexOf(target);

            if (removedIdx < targetIdx)
            {
                Model.ScriptItems.Insert(targetIdx + 1, droppedData);
                Model.ScriptItems.RemoveAt(removedIdx);
            }
            else
            {
                int remIdx = removedIdx + 1;
                if (Model.ScriptItems.Count + 1 > remIdx)
                {
                    Model.ScriptItems.Insert(targetIdx, droppedData);
                    Model.ScriptItems.RemoveAt(remIdx);
                }
            }

        }
    }
}
