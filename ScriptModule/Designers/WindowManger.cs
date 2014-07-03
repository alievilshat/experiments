using System;
using System.Windows;

namespace ScriptModule.Designers
{
    public interface IWindowManager
    {
        void ShowWindow(object content, string title = null);

        void SetStatus(string p);

        void ShowException(Exception ex);
    }

    public class WindowManger
    {
        private WindowManger()
        { }

        private static readonly PopupWindowManager _defaultWindowManager = new PopupWindowManager();
        private static IWindowManager _currnetIWindowManager;
        public static IWindowManager Current
        {
            get { return _currnetIWindowManager ?? _defaultWindowManager; }
        }

        public static void SetCurrentWindowManager(IWindowManager manager)
        {
            _currnetIWindowManager = manager;
        }
    }

    public class PopupWindowManager : IWindowManager
    {

        public void ShowWindow(object content, string title = null)
        {
            var window = new Window
            {
                Title = title ?? content.GetType().Name, 
                Content = content
            };
            window.Show();
        }

        public void SetStatus(string p)
        {
            MessageBox.Show(p);
        }

        public void ShowException(Exception ex)
        {
            MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
