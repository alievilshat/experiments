using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Mapper
{
    /// <summary>
    /// Interaction logic for ExpanderButton.xaml
    /// </summary>
    public partial class CloseButton : UserControl
    {
        public bool IsHighlighted
        {
            get { return (bool)GetValue(IsHighlightedProperty); }
            set { SetValue(IsHighlightedProperty, value); }
        }

        public static readonly DependencyProperty IsHighlightedProperty =
            DependencyProperty.Register("IsHighlighted", typeof(bool), typeof(CloseButton));

        public event EventHandler<EventArgs> Closing;

        private void OnClose()
        {
            if (Closing != null)
                Closing(this, EventArgs.Empty);
        }

        public CloseButton()
        {
            InitializeComponent();
        }

        private void UserControl_MouseClick(object sender, MouseButtonEventArgs e)
        {
            OnClose();
            e.Handled = true;
        }
    }
}
