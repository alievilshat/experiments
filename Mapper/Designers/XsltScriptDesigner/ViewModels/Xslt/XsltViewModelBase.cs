using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Xml;
using System.Xml.Schema;

namespace ScriptModule.Designers.XsltScriptDesigner.ViewModels.Xslt
{
    public class XsltViewModelBase : XsltDesignerViewModelBase
    {
        private XmlNode _node;
        public XmlNode Node
        {
            get { return _node; }
            set
            {
                _node = value;
                OnNodeChanged();
            }
        }

        protected virtual void OnNodeChanged()
        {
            if (_node != null)
            {
                _node.AsObservable().PropertyChanged += (o, e) =>
                    {
                        if (e.PropertyName == "ChildNodes")
                            OnPropertyChanged("Children");
                    };
            }
            OnPropertyChanged("Node"); 
        }

        public override object Model
        {
            get { return _node; }
        }

        MapperViewModel _mapperViewModel;
        public virtual MapperViewModel MapperViewModel
        {
            get { return _mapperViewModel; }
            set 
            { 
                _mapperViewModel = value;
                _mapperViewModel.Initialized += OnInitialized;
                _mapperViewModel.SizeChanged += OnSizeChanged;
            }
        }

        protected virtual void OnInitialized(object sender, EventArgs e)
        { }

        protected virtual void OnSizeChanged(object sender, EventArgs e)
        { }

        protected virtual XsltViewModelBase createViewModel(XmlNode node)
        {
            var model = createViewModelForNode(node);
            model.MapperViewModel = MapperViewModel;
            model.Node = node;
            return model;
        }

        private static XsltViewModelBase createViewModelForNode(XmlNode node)
        {
            var children = node.ChildNodes.Cast<XmlNode>();
            if (node is XmlDeclaration
                || node.NamespaceURI == MSXSL_NAMESPACE)
                return new XsltSkipNodeViewModel();

            if (children.Any(i => i.LocalName == "value-of") || children.All(i => i is XmlText))
            {
                return new XsltMixedContentViewModel();
            }

            if (node.NamespaceURI != XSL_NAMESPACE)
                return new XsltContentNodeViewModel();

            if (node.LocalName == "template")
            {
                var match = node.Attributes["match"];
                if (match != null && match.Value == "/")
                    return new XsltRootTemplateNodeViewModel();

                return new XsltTemplateNodeViewModel();
            }
            if (node.LocalName == "for-each")
            {
                return new XsltForEachViewModel();
            }
            return new XsltUnknownNodeViewModel();
        }

        public IEnumerable<XsltViewModelBase> Children
        {
            get 
            { 
                return _node == null ? null 
                    : _node.ChildNodes.Cast<XmlNode>().Select(createViewModel); 
            }
        }

        public int Left
        {
            get { return getPosition("left"); }
            set { updatePosition("Left", value); }
        }

        public int Top
        {
            get { return getPosition("top"); }
            set { updatePosition("Top", value); }
        }

        private int getPosition(string prop)
        {
            var attr = _node.Attributes[prop, MAPPER_NAMESPACE];
            int res;
            if (attr == null || !int.TryParse(attr.Value, out res))
                return 0;

            return res;
        }

        private void updatePosition(string propertyName, int value)
        {
            var prop = propertyName.ToLower();
            var attr = _node.Attributes[prop, MAPPER_NAMESPACE];
            if (attr == null)
            {
                attr = _node.OwnerDocument.CreateAttribute("m", prop, MAPPER_NAMESPACE);
                _node.Attributes.Append(attr);
            }
            attr.Value = value.ToString();
            OnPropertyChanged(propertyName);
            OnPositionChanged();
        }

        protected virtual void OnPositionChanged()
        { }

        #region Util methods for inherited classes
        protected string GetTargetContext(XmlNode n)
        {
            var path = new Stack<string>(20);
            while (!(n is XmlDocument))
            {
                if (n.NamespaceURI != XSL_NAMESPACE)
                    path.Push(n.LocalName);
                n = n.ParentNode;
            }
            var context = "/" + string.Join("/", path);
            return context;
        }

        protected string getSourceContext(XmlNode n)
        {
            var path = new Stack<string>(20);
            while (!(n is XmlDocument))
            {
                var attr = getXslPathAttribute(n);

                if (attr != null)
                    path.Push(attr.Value);

                n = n.ParentNode;
            }

            var context = string.Join("/", path);
            return context;
        }

        protected XmlAttribute getXslPathAttribute(XmlNode n)
        {
            if (n.NamespaceURI != XSL_NAMESPACE)
                return null;

            return n.Attributes["select"] ?? n.Attributes["match"];
        }

        protected Thumb GetNodePort(FrameworkElement schema, string path)
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
                    .FirstOrDefault(i => string.Compare(((XmlSchemaElement)i.DataContext).Name, p, StringComparison.OrdinalIgnoreCase) == 0);

                if (node == null)
                    return null;
            }

            if (!node.IsVisible)
                return null;

            return node.GetChildren().OfType<Thumb>().FirstOrDefault();
        }
        #endregion
    }
}
