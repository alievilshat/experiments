using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Mapper
{
    public class XsltViewModelBase : ViewModelBase
    {
        public const string XSL_NAMESPACE = "http://www.w3.org/1999/XSL/Transform";

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
                Children = new ObservableCollection<XsltViewModelBase>(_node.ChildNodes.Cast<XmlNode>().Select(i => createViewModel(i)));
            }
            OnPropertyChanged("Node"); 
        }

        public MapperViewModel MapperViewModel { get; set; }

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
            if (children.Any(i => i.LocalName == "value-of"))
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

        private ObservableCollection<XsltViewModelBase> _children = new ObservableCollection<XsltViewModelBase>();
        public ObservableCollection<XsltViewModelBase> Children
        {
            get { return _children; }
            set { _children = value; OnPropertyChanged("Children"); }
        }
    }
}
