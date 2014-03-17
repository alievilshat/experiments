using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Xml;

namespace Mapper
{
    [ValueConversion(typeof(object), typeof(RichTextBox))]   
    class ContentToDocumentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var rtb = new RichTextBox();
            BindingOperations.SetBinding(rtb, RichTextBox.WidthProperty,
                new Binding("ActualWidth") { RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(ContentControl), 1) });

            rtb.PreviewKeyDown += rtb_PreviewKeyDown;
            rtb.Document = new FlowDocument();
            var paragraph = new Paragraph();
            paragraph.Inlines.AddRange(value.As<IEnumerable>().Cast<XmlNode>().Select(i =>
                {
                    if (i is XmlText)
                        return (Inline)new Run(i.Value.Trim());
                    if (i.LocalName == "value-of" && i.Attributes["select"] != null)
                        return (Inline)new Bold(new Run("[" + i.Attributes["select"].Value.Trim() + "]"));
                    return (Inline)new Bold(new Run("[Unknown]"));
                }));
            
            rtb.Document.Blocks.Add(paragraph);
            return rtb;
        }

        void rtb_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var rtb = sender.As<RichTextBox>();
            if (e.Key == Key.Enter)
            {
                var newPointer = rtb.Selection.Start.InsertLineBreak();
                rtb.Selection.Select(newPointer, newPointer);
                e.Handled = true;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
