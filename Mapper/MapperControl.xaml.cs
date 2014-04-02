using System;
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

        private void schemaDragEnter(object sender, DragEventArgs e)
        {
            var dst = (FrameworkElement)e.OriginalSource;
            if (dst.DataContext == null)
                return;

            if (dst.DataContext.As<XmlSchemaElement>() == null)
                return;

            acceptDragAndDrop = e.Data.GetDataPresent(typeof(XmlSchemaElement))
                && getRoot(dst.DataContext.CastAs<XmlSchemaElement>()) != getRoot(e.Data.GetData(typeof(XmlSchemaElement)).CastAs<XmlSchemaElement>());

            if (acceptDragAndDrop)
            {
                dst.FindAncestor<TreeViewItem>().IsSelected = true;
            }

            schemaDragOver(sender, e);
        }

        private XmlSchemaObject getRoot(XmlSchemaObject element)
        {
            if (element.Parent == null)
                return element;
            return getRoot(element.Parent);
        }

        private void schemaDragOver(object sender, DragEventArgs e)
        {
            if (!acceptDragAndDrop)
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
            }
        }

        private void sourceSchemaDrop(object sender, DragEventArgs e)
        {
            var source = e.Data.GetData(typeof(XmlSchemaElement)).CastAs<XmlSchemaElement>();
            var target = e.OriginalSource.CastAs<FrameworkElement>().DataContext.CastAs<XmlSchemaElement>();

            Model.AddTransformation(target, source);
        }

        private void targetSchemaDrop(object sender, DragEventArgs e)
        {
            var source = e.Data.GetData(typeof(XmlSchemaElement)).CastAs<XmlSchemaElement>();
            var target = e.OriginalSource.CastAs<FrameworkElement>().DataContext.CastAs<XmlSchemaElement>();

            Model.AddTransformation(source, target);
        }
    }
}