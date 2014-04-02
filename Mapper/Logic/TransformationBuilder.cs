using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;

namespace Mapper
{
    class TransformationBuilder
    {
        private XmlDocument _document;

        public TransformationBuilder(XmlDocument document)
        {
            this._document = document;
        }

        public XmlNode BuildTransform(XmlSchemaElement source, XmlSchemaElement target)
        {
            var sourceNodePath = new SourceNodePath(source);
            var targetNodePath = new TargetNodePath(target);

            var node = navigate(_document.DocumentElement, sourceNodePath, targetNodePath);
            IEnumerable<NodeProperties> sourceNodes = sourceNodePath.GetPath().ToArray();
            IEnumerable<NodeProperties> targetNodes = targetNodePath.GetPath().ToArray();

            var t = node;

            if (t == t.OwnerDocument.DocumentElement)
            {
                t = buildNodes(t, sourceNodes.Take(1));
                sourceNodes = sourceNodes.Skip(1);
            }

            if (sourceNodes.Any() && sourceNodes.Last().Name == "for-each")
            {
                t = buildNodes(t, sourceNodes);
                t = buildNodes(t, targetNodes);
            }
            else
            {
                t = buildNodes(t, targetNodes);
                t = buildNodes(t, sourceNodes);
            }

            return node;
        }

        private XmlNode navigate(XmlNode node, NodePath sourcePath, NodePath targetPath)
        {
            foreach (var e in node.ChildNodes.Cast<XmlNode>())
            {
                if (sourcePath.Reduce(e) || targetPath.Reduce(e))
                    return navigate(e, sourcePath, targetPath);
            }
            return node;
        }

        private XmlNode buildNodes(XmlNode node, IEnumerable<NodeProperties> path)
        {
            foreach (var p in path)
	        {
                var t = string.IsNullOrEmpty(p.Namespace)
                    ? node.OwnerDocument.CreateElement(p.Name)
                    : node.OwnerDocument.CreateElement("xsl", p.Name, p.Namespace);
                if (p.Attributes != null)
                { 
                    foreach (var a in p.Attributes)
                    {
                        t.SetAttribute(a.Key, a.Value);
                    }
                }
                node.AppendChild(t);
                node = t;
	        }
            return node;
        }
    }
}
