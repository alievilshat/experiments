﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace ScriptModule.Designers.XsltScriptDesigner.Controls
{
    /// <summary>
    /// Interaction logic for BlockControl.xaml
    /// </summary>
    public partial class BlockControl : UserControl
    {
        private static int s_ZIndex = 0;

        public BlockControl()
        {
            s_ZIndex++;
            InitializeComponent();
        }

        private void ExpanderHeaderDoubleClick(object sender, MouseButtonEventArgs e)
        {
            expander.IsExpanded = !expander.IsExpanded;
            e.Handled = true;
        }

        public object HeaderContent
        {
            get { return (object)GetValue(HeaderContentProperty); }
            set { SetValue(HeaderContentProperty, value); }
        }
        public static readonly DependencyProperty HeaderContentProperty =
            DependencyProperty.Register("HeaderContent", typeof(object), typeof(BlockControl), new PropertyMetadata(null));


        public object BlockContent
        {
            get { return (object)GetValue(BlockContentProperty); }
            set { SetValue(BlockContentProperty, value); }
        }
        public static readonly DependencyProperty BlockContentProperty =
            DependencyProperty.Register("BlockContent", typeof(object), typeof(BlockControl), new PropertyMetadata(null));

        public Thumb LeftPort
        {
            get { return leftPort; }
        }

        public Thumb RightPort
        {
            get { return rightPort; }
        }

        private void uc_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var parent = VisualTreeHelper.GetParent(this);
            Panel.SetZIndex((UIElement)parent, s_ZIndex++);
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Canvas.SetLeft(container, Canvas.GetLeft(container) + e.HorizontalChange);
            Canvas.SetTop(container, Canvas.GetTop(container) + e.VerticalChange);
        }

        private void RightThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (!expander.IsExpanded) return;
            var val = content.Width + e.HorizontalChange;
            if (val > 120)
                content.Width = val;
        }

        private void BottomThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (!expander.IsExpanded) return;
            var val = content.Height + e.VerticalChange;
            if (val > 20)
                content.Height = val;
        }
    }
}
