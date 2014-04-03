using System;
using System.Collections;
using System.Collections.Generic;
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

        private IEnumerable<string> _sourcePaths;
        public IEnumerable<Reference<Thumb>> _sourcePorts;
        public IEnumerable<Reference<Thumb>> SourcePorts
        {
            get { return _sourcePorts; }
            set { _sourcePorts = value; OnPropertyChanged("SourcePorts"); }
        }

        private string _targetPath;
        private Reference<Thumb> _targetPort;
        public Reference<Thumb> TargetPort
        {
            get { return _targetPort; }
            set { _targetPort = value; OnPropertyChanged("TargetPort"); }
        }

        public XsltMixedContentViewModel()
        {
            ContentTextBox = createContentTextBox();
        }

        protected override void OnLayoutUpdated(object sender, EventArgs e)
        {
            base.OnLayoutUpdated(sender, e);

            updateTargetPort();
            updateSourcePorts();
        }

        protected override void OnNodeChanged()
        {
            updateContentTextBox();

            // Target path
            _targetPath = GetTargetContext(Node); 

            // Source paths
            var context = getSourceContext(Node);
            _sourcePaths = Node.ChildNodes.Cast<XmlNode>().Select(i => getXslPathAttribute(i)).Where(a => a != null).Select(a => context + "/" + a.Value).ToArray();

            base.OnNodeChanged();
        }

        private void updateTargetPath()
        {
        }

        private void updateTargetPort()
        {
            var port = GetNodePort(MapperViewModel.Host.TargetSchemaControl, _targetPath);
            if (port != TargetPort.As<Thumb>())
                TargetPort = new Reference<Thumb>(port);
        }

        private void updateSourcePorts()
        {
            var sourcePorts = _sourcePaths.Select(i => GetNodePort(MapperViewModel.Host.SourceSchemaControl, i));

            if (SourcePorts == null || !sourcePorts.SequenceEqual(SourcePorts.Select(i => i.Target)))
            {
                SourcePorts = sourcePorts.Select(i => new Reference<Thumb>(i));
            }
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
