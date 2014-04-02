using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Xml;
using System.Xml.Schema;

namespace Mapper
{
    class XsltMixedContentViewModel : XsltViewModelBase
    {
        private RichTextBox _contentTextBox;
        public RichTextBox ContentTextBox
        {
            get { return _contentTextBox; }
            set { _contentTextBox = value; OnPropertyChanged("ContentTextBox"); }
        }

        private ObservableCollection<string> _sourcePaths;
        public ObservableCollection<string> SourcePaths
        {
            get { return _sourcePaths; }
            set { _sourcePaths = value; OnPropertyChanged("SourcePaths"); }
        }

        public ObservableCollection<Thumb> _sourcePorts;
        public ObservableCollection<Thumb> SourcePorts
        {
            get { return _sourcePorts; }
            set { _sourcePorts = value; OnPropertyChanged("SourcePorts"); }
        }

        private string _targetPath;
        public string TargetPath
        {
            get { return _targetPath; }
            set { _targetPath = value; OnPropertyChanged("TargetPath"); }
        }

        private Thumb _targetPort;

        public Thumb TargetPort
        {
            get { return _targetPort; }
            set { _targetPort = value; OnPropertyChanged("TargetPort"); }
        }


        public XsltMixedContentViewModel()
        {
            ContentTextBox = createContentTextBox();
            SourcePaths = new ObservableCollection<string>();
        }

        protected override void OnNodeChanged()
        {
            updateContentTextBox();
            updateSourcePaths();
            updateTargetPath();

            base.OnNodeChanged();
        }

        private void updateTargetPath()
        {
            var path = new Stack<string>(20);
            var n = Node;
            while (!(n is XmlDocument))
            {
                if (n.NamespaceURI != XSL_NAMESPACE)
                    path.Push(n.LocalName);
                n = n.ParentNode;
            }
            TargetPath = "/" + string.Join("/", path);

            MapperViewModel.LayoutUpdated += (o, e) => updateTargetPort();
        }

        private void updateTargetPort()
        {
            var port = getNodePort(MapperViewModel.Host.TargetSchemaControl, TargetPath);
            if (port != TargetPort)
                TargetPort = port;
        }

        private void updateSourcePaths()
        {
            //throw new System.NotImplementedException();
        }

        private Thumb getNodePort(FrameworkElement schema, string path)
        {
            if (schema == null || path == null)
                return null;

            var parts = path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            var node = schema.GetChildrenBFS().OfType<FrameworkElement>().FirstOrDefault(i => i.DataContext is XmlSchemaElement);
            if (node == null)
                return null;

            foreach (var p in parts)
            {
                node = node.GetChildrenBFS().OfType<FrameworkElement>().Where(i => i.DataContext is XmlSchemaElement)
                    .FirstOrDefault(i => string.Compare(((XmlSchemaElement)i.DataContext).Name, p, true) == 0);

                if (node == null)
                    return null;
            }

            if (!node.IsVisible)
                return null;

            return node.GetChildren().OfType<Thumb>().FirstOrDefault();
        }

        private void updateContentTextBox()
        {
            var paragraph = new Paragraph();
            paragraph.Inlines.AddRange(Node.As<IEnumerable>().Cast<XmlNode>().Select(i =>
            {
                if (i is XmlText)
                    return (Inline)new Run(i.Value.Trim());
                if (i.LocalName == "value-of" && i.Attributes["select"] != null)
                    return (Inline)new Bold(new Run("[" + i.Attributes["select"].Value.Trim() + "]"));
                return (Inline)new Bold(new Run("[Unknown]"));
            }));

            ContentTextBox.Document.Blocks.Clear();
            ContentTextBox.Document.Blocks.Add(paragraph);
        }

        protected RichTextBox createContentTextBox()
        {
            var rtb = CreateRichTextBox();

            rtb.PreviewKeyDown += rtb_PreviewKeyDown;
            rtb.TextChanged += rtb_TextChanged;
            return rtb;
        }

        private void rtb_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var rtb = (RichTextBox)sender;
                var newPointer = rtb.Selection.Start.InsertLineBreak();
                rtb.Selection.Select(newPointer, newPointer);
                e.Handled = true;
            }
        }

        void rtb_TextChanged(object sender, TextChangedEventArgs e)
        {
            // TODO: Update node
            // TODO: Syntax highlighting
        }
    }
}
