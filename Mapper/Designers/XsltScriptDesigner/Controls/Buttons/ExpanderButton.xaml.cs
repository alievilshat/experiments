using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ScriptModule.Designers.XsltScriptDesigner.Controls.Buttons
{
    /// <summary>
    /// Interaction logic for ExpanderButton.xaml
    /// </summary>
    public partial class ExpanderButton : UserControl
    {
        public class ExpanderEventArgs : EventArgs
        {
            public bool IsExpanded { get; set; }
        }

        public ExpanderButton()
        {
            InitializeComponent();
        }

        private void UserControl_MouseClick(object sender, MouseButtonEventArgs e)
        {
            IsExpanded = !IsExpanded;
            e.Handled = true;
        }

        private void OnExpandStateChanged()
        {
            if (ExpandStateChanged != null)
                ExpandStateChanged(this, new ExpanderEventArgs { IsExpanded = IsExpanded });
        }

        public event EventHandler<ExpanderEventArgs> ExpandStateChanged;

        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); OnExpandStateChanged(); }
        }

        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register("IsExpanded", typeof(bool), typeof(ExpanderButton));

        public bool IsHighlighted
        {
            get { return (bool)GetValue(IsHighlightedProperty); }
            set { SetValue(IsHighlightedProperty, value); }
        }

        public static readonly DependencyProperty IsHighlightedProperty =
            DependencyProperty.Register("IsHighlighted", typeof(bool), typeof(ExpanderButton));
    }
}
