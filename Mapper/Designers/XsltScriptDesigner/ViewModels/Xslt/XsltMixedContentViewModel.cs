using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Xml;
using ScriptModule.Utils;

namespace ScriptModule.Designers.XsltScriptDesigner.ViewModels.Xslt
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
        private IEnumerable<Reference<Thumb>> _sourcePorts;
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

        protected override void OnPositionChanged()
        {
            base.OnPositionChanged();
            refreshPorts();
        }

        protected override void OnSizeChanged(object sender, EventArgs e)
        {
            base.OnSizeChanged(sender, e);
            refreshPorts();
        }

        private void refreshPorts()
        {
            var t = SourcePorts;
            SourcePorts = null;
            SourcePorts = t;
            //OnPropertyChanged("SourcePorts");

            OnPropertyChanged("TargetPort");
        }

        protected override void OnInitialized(object sender, EventArgs e)
        {
            base.OnInitialized(sender, e);

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
                    return (Inline)new Bold(new Run("{" + i.Attributes["select"].Value.Trim() + "}"));
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

        bool _editing;
        void rtb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_editing)
                return;
            _editing = true;

            var offset = ContentTextBox.CaretPosition.DocumentStart.GetOffsetToPosition(ContentTextBox.CaretPosition);

            var paragraph = ContentTextBox.Document.Blocks.OfType<Paragraph>().FirstOrDefault();
            if (paragraph == null)
            {
                paragraph = new Paragraph();
                ContentTextBox.Document.Blocks.Add(paragraph);
            }

            while (analizeText(paragraph)) ;
            updateText(paragraph);

            ContentTextBox.CaretPosition = ContentTextBox.CaretPosition.DocumentStart.GetPositionAtOffset(offset);
            _editing = false;
        }

        private bool analizeText(Paragraph paragraph)
        {
            bool requiresExtraAnalysis = false;

            var t = paragraph.Inlines.FirstInline;
            while (t != null)
            {
                if (t is Bold)
                {
                    var bold = (Bold)t;
                    if (bold.Inlines.Any(i => !(i is Run)))
                    {
                        t = extract(paragraph.Inlines, bold);
                        requiresExtraAnalysis = true;
                        continue;
                    }

                    var run = (Run)bold.Inlines.FirstInline;
                    if (run != null)
                    {
                        unionConsequentRuns(bold.Inlines, run);
                        if (!run.Text.StartsWith("{") || !run.Text.EndsWith("}"))
                        {
                            t = extract(paragraph.Inlines, bold);
                            requiresExtraAnalysis = true;
                            continue;
                        }
                    }
                }
                else if (t is Run)
                {
                    var run = (Run)t;
                    unionConsequentRuns(paragraph.Inlines, run);

                    var p = new Regex(@"\{[^\}]+\}");
                    var res = new List<Inline>();
                    var lastindex = 0;
                    foreach (var m in p.Matches(run.Text).Cast<Match>())
                    {
                        var text = run.Text.Substring(lastindex, m.Index - lastindex);
                        res.Add(new Run(text));
                        var boldtext = run.Text.Substring(m.Index, m.Length);
                        res.Add(new Bold(new Run(boldtext)));
                        lastindex = m.Index + m.Length;
                    }
                    if (lastindex < run.Text.Length)
                    {
                        var text = run.Text.Substring(lastindex);
                        res.Add(new Run(text));
                    }
                    foreach (var i in res)
                        paragraph.Inlines.InsertBefore(run, i);

                    t = run.NextInline;

                    paragraph.Inlines.Remove(run);
                    continue;
                }
                else if (t is Span)
                {
                    var span = (Span)t;
                    t = extract(paragraph.Inlines, span);
                    requiresExtraAnalysis = true;
                    continue;
                }

                t = t.NextInline;
            }

            return requiresExtraAnalysis;
        }

        private static void unionConsequentRuns(InlineCollection collection, Run run)
        {
            while (run.NextInline is Run)
            {
                run.Text += ((Run)run.NextInline).Text;
                collection.Remove(run.NextInline);
            }
        }

        private static Inline extract(InlineCollection parentCollection, Span span)
        {
            var i = span.Inlines.FirstInline;
            while (i != null)
            {
                var next = i.NextInline;
                span.Inlines.Remove(i);
                parentCollection.InsertBefore(span, i);
                i = next;
            }

            var res = span.NextInline;
            parentCollection.Remove(span);
            return res;
        }

        private void updateText(Paragraph paragraph)
        {
            while (Node.ChildNodes.Count > 0)
                Node.RemoveChild(Node.ChildNodes[0]);

            foreach (var i in paragraph.Inlines)
            {
                if (i is Run)
                {
                    Node.AppendChild(Node.OwnerDocument.CreateTextNode(((Run)i).Text));
                }
                else if (i is Bold)
                {
                    var run = (Run)((Bold)i).Inlines.FirstInline;
                    if (run == null)
                        continue;

                    var element = Node.OwnerDocument.CreateElement("xsl", "value-of", XSL_NAMESPACE);
                    var text = run.Text.Substring(1, run.Text.Length - 2);
                    element.SetAttribute("select", text);
                    Node.AppendChild(element);
                }
            }
        }
    }
}