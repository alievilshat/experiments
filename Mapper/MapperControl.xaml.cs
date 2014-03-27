using System.Windows;
using System.Windows.Controls;
using System.Xml.Schema;

namespace Mapper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MapperControl : UserControl, IMapperHost
    {
        public FrameworkElement SourceSchemaControl { get { return sourceSchema; } }

        public FrameworkElement TargetSchemaControl { get { return targetSchema; } }

        public MapperControl()
        {
            InitializeComponent();
        }

        private MapperViewModel Model
        {
            get { return (MapperViewModel)DataContext; }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Model.Host = this;
            LayoutUpdated += (o, a) => Model.OnLayoutUpdated();
        }

        private void DesignPanel_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
                Model.UpdateModel();
        }

        private void SourceTextBox_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
                Model.UpdateSourceTextBox();
        }

        private bool acceptDragAndDrop = false;

        private void schemaNodeDragEnter(object sender, DragEventArgs e)
        {
            var dst = (FrameworkElement)sender;

            acceptDragAndDrop = e.Data.GetDataPresent(typeof(XmlSchemaElement))
                && getRoot(((dynamic)dst.DataContext).Target) != getRoot((XmlSchemaElement)e.Data.GetData(typeof(XmlSchemaElement)));

            if (acceptDragAndDrop)
            {
                dst.FindAncestor<TreeViewItem>().IsSelected = true;
            }

            schemaNodeDragDrop(sender, e);
        }

        private XmlSchemaObject getRoot(XmlSchemaObject element)
        {
            if (element.Parent == null)
                return element;
            return getRoot(element.Parent);
        }

        private void schemaNodeDragOver(object sender, DragEventArgs e)
        {
            if (!acceptDragAndDrop)
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
            }
        }

        private void schemaNodeDragDrop(object sender, DragEventArgs e)
        {
            // TODO: UDPATE MODEL
        }
    }
}