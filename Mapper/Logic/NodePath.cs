using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;

namespace Mapper
{
    using Candidate = KeyValuePair<NodeProperties, LinkedListNode<XmlSchemaElement>>;

    abstract class NodePath
    {
        public const string XSL_NAMESPACE = "http://www.w3.org/1999/XSL/Transform";

        private XmlSchemaElement _node;
        private LinkedListNode<XmlSchemaElement> _current;

        public NodePath(XmlSchemaElement node)
        {
            this._node = node;
            this._current = new LinkedList<XmlSchemaElement>(node.Path().OfType<XmlSchemaElement>()).First;
        }

        public virtual bool Reduce(XmlNode e)
        {
            var p = GetNextNodeCandidates(_current).Where(i => i.Key.Match(e)).Select(i => i.Value).FirstOrDefault();
            if (p == null)
                return false;

            _current = p;
            return true;
        }

        public virtual bool Empty()
        {
            return _current == null;
        }

        protected abstract IEnumerable<KeyValuePair<NodeProperties, LinkedListNode<XmlSchemaElement>>> GetNextNodeCandidates(LinkedListNode<XmlSchemaElement> current);

        public IEnumerable<NodeProperties> GetPath()
        {
            var c = _current;

            while (c != null)
            {
                var n = GetNextNodeCandidates(c).First();
                yield return n.Key;
                c = n.Value;
            }
        }
    }

    class TargetNodePath : NodePath
    {
        public TargetNodePath(XmlSchemaElement node)
            : base(node)
        { }

        protected override IEnumerable<Candidate> GetNextNodeCandidates(LinkedListNode<XmlSchemaElement> current)
        {
            yield return new Candidate(new NodeProperties { Name = current.Value.Name }, current.Next);
        }
    }

    class SourceNodePath : NodePath
    {
        public SourceNodePath(XmlSchemaElement node)
            : base(node)
        { }

        protected override IEnumerable<Candidate> GetNextNodeCandidates(LinkedListNode<XmlSchemaElement> current)
        {
            var element = current.Value;
            if (element.Parent is XmlSchema)
            {
                if (current.Next != null)
                {
                    yield return new Candidate(
                            new NodeProperties
                            {
                                Name = "template",
                                Namespace = XSL_NAMESPACE,
                                Attributes = new Dictionary<string, string> { { "match", "/" + current.Next.Value.Name } }
                            },
                            current.Next.Next
                        );
                }
                yield return new Candidate(
                        new NodeProperties
                        {
                            Name = "template",
                            Namespace = XSL_NAMESPACE,
                            Attributes = new Dictionary<string, string> { { "match", "/" } }
                        },
                        current.Next
                    );

                yield break;
            }

            var last = current.List.Last;

            while (last != current.Previous)
            {
                var elementname = last.Value.MaxOccursString == "unbounded" ? "for-each" : "value-of";

                yield return new Candidate(
                            new NodeProperties
                            {
                                Name = elementname,
                                Namespace = XSL_NAMESPACE,
                                Attributes = new Dictionary<string, string> { { "select", string.Join("/", GetPath(current, last)) } }
                            },
                            last.Next
                        );

                last = last.Previous;
            }
        }

        private IEnumerable<string> GetPath(LinkedListNode<XmlSchemaElement> current, LinkedListNode<XmlSchemaElement> last)
        {
            while(current != last.Next)
            {
                yield return current.Value.Name;
                current = current.Next;
            }
        }
    }
}
