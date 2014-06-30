﻿using System.Linq;
using System.Windows.Input;
using ScriptModule.ViewModels;
using System.Windows;
using AvalonDock;
using ScriptModule.Designers;

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

        private void Close_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void ScriptItem_Click(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount != 2)
                return;

            var context = ((FrameworkElement)sender).DataContext as ScriptRowViewModel;
            if (context == null || context.ScriptText == null)
                return;

            var doc = dockManager.Documents.FirstOrDefault(i => i.Content == context.ScriptDesigner);
            if (doc != null)
            {
                doc.Activate();
                return;
            }
            var documentContent = new DocumentContent();
            documentContent.Title = context.ScriptName;
            documentContent.Content = context.ScriptDesigner;
            documentContent.Show(dockManager);
        }

        private static ScriptRowViewModel getModel(object sender)
        {
            var model = ((FrameworkElement)sender).DataContext as ScriptRowViewModel;
            return model;
        }

        private void ScriptName_TextChanged(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var model = getModel(sender);
                model.RenameMode = false;
            }
        }

        private void ScriptRename_Click(object sender, RoutedEventArgs e)
        {
            var model = getModel(sender);
            model.RenameMode = true;
        }

        private void ScriptExecute_Click(object sender, RoutedEventArgs e)
        {
            var model = getModel(sender);
            Model.Execute(model);
        }

        private void ScriptDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CanRun_Handler(object sender, CanExecuteRoutedEventArgs e)
        {
            Model.CanExecute();
        }

        private void Run_Click(object sender, ExecutedRoutedEventArgs e)
        {
            Model.Execute();
        }

        private void ShowWindow(object sender, RoutedEventArgs e)
        {
            var pane = ((FrameworkElement)sender).Tag as DockableContent;
            switch (pane.State)
            {
                case DockableContentState.Hidden:
                case DockableContentState.AutoHide:
                    pane.Show();
                    break;
            }
        }
    }
}
