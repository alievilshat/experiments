using System;
using System.Windows.Controls.Primitives;
using ScriptModule.Utils;

namespace ScriptModule.Designers.XsltScriptDesigner.ViewModels.Xslt
{
    public class XsltForEachViewModel : XsltViewModelBase
    {
        private string _sourcePath;
        private Reference<Thumb> _sourcePort;
        public Reference<Thumb> SourcePort
        {
            get { return _sourcePort; }
            set { _sourcePort = value; OnPropertyChanged("SourcePort"); }
        }

        private string _targetPath;
        private Reference<Thumb> _targetPort;
        public Reference<Thumb> TargetPort
        {
            get { return _targetPort; }
            set { _targetPort = value; OnPropertyChanged("TargetPort"); }
        }

        protected override void OnNodeChanged()
        {
            // Target path
            _targetPath = GetTargetContext(Node) + "/" + Node.FirstChild.Name;

            // Source paths
            _sourcePath = getSourceContext(Node);

            base.OnNodeChanged();
        }

        protected override void OnInitialized(object sender, EventArgs e)
        {
            base.OnInitialized(sender, e);
            updateSourcePort();
            updateTargetPort();
        }

        private void updateSourcePort()
        {
            var port = GetNodePort(MapperViewModel.Host.SourceSchemaControl, _sourcePath);
            if (port != SourcePort.As<Thumb>())
                SourcePort = new Reference<Thumb>(port);
        }

        private void updateTargetPort()
        {
            var port = GetNodePort(MapperViewModel.Host.TargetSchemaControl, _targetPath);
            if (port != TargetPort.As<Thumb>())
                TargetPort = new Reference<Thumb>(port);
        }
    }
}
