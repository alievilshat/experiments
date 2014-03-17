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
            try
            {
                if (item is XmlNode)
                {
                    var node = (XmlNode)item;

                    var children = node.ChildNodes.Cast<XmlNode>();
                    if (children.Any(i => i.LocalName == "value-of"))
                    {
                        return getTemplate(container, "mixed-value-of");
                    }

                    if (node.NamespaceURI != xsl)
                        return getTemplate(container, "resultNodeTemplate");

                    if (node.LocalName == "template")
                    {
                        var match = node.Attributes["match"];
                        if (match != null && match.Value == "/")
                            return getTemplate(container, "root_template");
                    }
                    return getTemplate(container, node.LocalName);
                }
                return base.SelectTemplate(item, container);
            }
            catch (ResourceReferenceKeyNotFoundException)
            {
                return getTemplate(container, "unknownNodeTemplate");
            }
        }

        private static DataTemplate getTemplate(DependencyObject element, string template)
        {
            return (DataTemplate)((FrameworkElement)element).FindResource(template);
        }
    }
}
