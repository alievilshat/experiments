using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using ScintillaNET;
using UserControl = System.Windows.Controls.UserControl;

namespace ScriptModule.Controls
{
    /// <summary>
    /// Interaction logic for Editor.xaml
    /// </summary>
    public partial class Editor : UserControl
    {
        public string Syntax 
        {
            get { return editor.ConfigurationManager.Language; }
            set { editor.ConfigurationManager.Language = value; }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(Editor), new PropertyMetadata(string.Empty));

        public Editor()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                Background = Brushes.AliceBlue;
                return;
            }
            InitializeComponent();
        }
    }

    class ScintillaEditor : Scintilla, INotifyPropertyChanged
    {
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; OnPropertyChanged("Text"); }
        }

        public ScintillaEditor()
        {
            AutoComplete.DropRestOfWord = true;
            Scrolling.HorizontalWidth = 4000;
            Margins.Margin0.Width = 30;
            Indentation.UseTabs = false;
            Indentation.TabWidth = 2;
        }

        protected override void OnLostFocus(EventArgs e)
        {
            OnPropertyChanged("Text");
        }

        private void OnPropertyChanged(string p)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
