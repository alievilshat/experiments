using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Xml.Schema;
using ScriptModule.Utils.Extensions;

namespace ScriptModule
{
    /// <summary>
    /// Interaction logic for Schema.xaml
    /// </summary>
    public partial class SchemaControl : UserControl
    {
        public SchemaControl()
        {
            DependencyPropertyDescriptor
                .FromProperty(FlowDirectionProperty, typeof(FrameworkElement))
                .AddValueChanged(this, (s, e) => 
                {
                    switch (FlowDirection)
                    {
                        case FlowDirection.LeftToRight:
                            ScrollViewFlowDirection = FlowDirection.RightToLeft;
                            TreeViewFlowDirection = FlowDirection.LeftToRight;
                            break;
                        case FlowDirection.RightToLeft:
                            ScrollViewFlowDirection = FlowDirection.LeftToRight;
                            TreeViewFlowDirection = FlowDirection.RightToLeft;;
                            break;
                    }
                });
            InitializeComponent();
            Port = port;
        }

        public bool DragDropEnabled { get; set; }

        private void OpenDesigner_Click(object sender, RoutedEventArgs e)
        {
            var w = new StringWriter();
            Schema.Write(w);

            var designer = new SchemaDesigner(w.ToString());
            designer.Show();
            designer.Closing += designer_Closing;
        }

        void designer_Closing(object sender, CancelEventArgs e)
        {
            var res = MessageBox.Show("Do you want to save the changes?", "Save", MessageBoxButton.YesNoCancel);
            switch (res)
            {
                case MessageBoxResult.No:
                    return;

                case MessageBoxResult.Yes:
                    Schema = sender.As<SchemaDesigner>().Schema;
                    break;

                default:
                case MessageBoxResult.None:
                case MessageBoxResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }

        public XmlSchema Schema
        {
            get { return (XmlSchema)GetValue(SchemaProperty); }
            set { SetValue(SchemaProperty, value); }
        }
        public static readonly DependencyProperty SchemaProperty =
            DependencyProperty.Register("Schema", typeof(XmlSchema), typeof(SchemaControl), new PropertyMetadata(null));


        public FlowDirection ScrollViewFlowDirection
        {
            get { return (FlowDirection)GetValue(ScrollViewFlowDirectionProperty); }
            set { SetValue(ScrollViewFlowDirectionProperty, value); }
        }
        public static readonly DependencyProperty ScrollViewFlowDirectionProperty =
            DependencyProperty.Register("ScrollViewFlowDirection", typeof(FlowDirection), typeof(SchemaControl), new PropertyMetadata(FlowDirection.RightToLeft));


        public FlowDirection TreeViewFlowDirection
        {
            get { return (FlowDirection)GetValue(TreeViewFlowDirectionProperty); }
            set { SetValue(TreeViewFlowDirectionProperty, value); }
        }
        public static readonly DependencyProperty TreeViewFlowDirectionProperty =
            DependencyProperty.Register("TreeViewFlowDirection", typeof(FlowDirection), typeof(SchemaControl), new PropertyMetadata(FlowDirection.LeftToRight));


        public Thumb Port
        {
            get { return (Thumb)GetValue(PortProperty); }
            set { SetValue(PortProperty, value); }
        }
        public static readonly DependencyProperty PortProperty =
            DependencyProperty.Register("Port", typeof(Thumb), typeof(SchemaControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        private void XmlElement_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateLayout();
        }

        private void Node_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (DragDropEnabled && e.LeftButton == MouseButtonState.Pressed)
            {
                var item = e.OriginalSource as TextBlock;

                if (item == null || item.DataContext == null)
                    return;

                var data = getDataFromSchemaNodeDataContext(item);

                var dragData = new DataObject(data);
                DragDrop.DoDragDrop(item, dragData, DragDropEffects.Copy);
            }
        }

        private static XmlSchemaElement getDataFromSchemaNodeDataContext(FrameworkElement dst)
        {
            if (dst.DataContext is ObservableDecorator)
                return ((ObservableDecorator)dst.DataContext).Target as XmlSchemaElement;
            return dst.DataContext as XmlSchemaElement;
        }
    }
}
