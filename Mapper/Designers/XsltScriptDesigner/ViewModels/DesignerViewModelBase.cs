using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using ScriptModule.ViewModels;

namespace ScriptModule.Designers.XsltScriptDesigner.ViewModels
{
    public abstract class DesignerViewModelBase : ViewModelBase
    {
        public const string XSL = "xsl";
        public const string XSL_NAMESPACE = "http://www.w3.org/1999/XSL/Transform";
        public const string MSXSL_NAMESPACE = "urn:schemas-microsoft-com:xslt";
        public const string MAPPER_NAMESPACE = "http://www.navitas.nl/2014/Mapper";

        protected static RichTextBox CreateRichTextBox()
        {
            var rtb = new RichTextBox { Document = new FlowDocument() };
            BindingOperations.SetBinding(rtb, FrameworkElement.WidthProperty,
                new Binding("ActualWidth") { RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(ContentControl), 1) });
            return rtb;
        }
    }
}
