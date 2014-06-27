using System;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Schema;
using ScriptModule.Designers.XsltScriptDesigner.ViewModels;
using ScriptModule.Scripts.Generic;
using ScriptModule.Utils;

namespace ScriptModule.Designers.XsltScriptDesigner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class XsltScriptDesignerControl : DesignerControl, IMapperHost
    {
        public FrameworkElement SourceSchemaControl { get { return SourceSchema; } }

        public FrameworkElement TargetSchemaControl { get { return TargetSchema; } }

        public XsltScriptDesignerControl()
        {
            InitializeComponent();
        }

        public XsltScriptDesignerControl(XsltScript script)
        {
            DataContext = new MapperViewModel(script);
        }

        private MapperViewModel Model
        {
            get { return (MapperViewModel)DataContext; }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Model.Host = this;
            Model.Width = this.ActualWidth;
            Model.Height = this.ActualHeight;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Model.Width = ActualWidth;
            Model.Height = ActualHeight;
        }

        private void DesignPanel_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if ((bool)e.NewValue)
                    Model.UpdateModel();
            }
            catch (Exception ex)
            {
                Model.AddMessage(ex.ToString());
            }
        }

        private void SourceTextBox_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if ((bool)e.NewValue)
                    Model.UpdateSourceTextBox();
            }
            catch (Exception ex)
            {
                Model.AddMessage(ex.ToString());
            }
        }

        private bool _acceptDragAndDrop;

        private void schemaDragEnter(object sender, DragEventArgs e)
        {
            var dst = (FrameworkElement)e.OriginalSource;
            if (dst.DataContext == null)
                return;

            if (dst.DataContext.As<XmlSchemaElement>() == null)
                return;

            _acceptDragAndDrop = e.Data.GetDataPresent(typeof(XmlSchemaElement))
                && getRoot(dst.DataContext.CastAs<XmlSchemaElement>()) != getRoot(e.Data.GetData(typeof(XmlSchemaElement)).CastAs<XmlSchemaElement>());

            if (_acceptDragAndDrop)
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
            if (!_acceptDragAndDrop)
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