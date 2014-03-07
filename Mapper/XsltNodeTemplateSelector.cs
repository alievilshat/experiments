using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace Mapper
{
    class XsltNodeTemplateSelector : DataTemplateSelector
    {
        const string xsl = "http://www.w3.org/1999/XSL/Transform";

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is XmlNode)
            {
                var node = (XmlNode)item;
                switch (node.LocalName)
                {
                    case "template":
                        var match = node.Attributes["match"];
                        if (match != null && match.Value == "/")
                            return getTemplate(container, "xslRootTemplate");
                        else
                            return getTemplate(container, "xslTemplate");

                    case "value-of":
                        return getTemplate(container, "xslValueOf");

                    case "for-each":
                        return getTemplate(container, "xslForEach");

                    default:
                        return getTemplate(container, "xslDefaultTemplate");
                        break;
                }
            }
            return base.SelectTemplate(item, container);
        }

        private static DataTemplate getTemplate(DependencyObject element, string template)
        {
            return (DataTemplate)((FrameworkElement)element).FindResource(template);
        }
    }
}
