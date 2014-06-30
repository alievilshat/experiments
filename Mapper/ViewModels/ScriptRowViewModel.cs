using ScriptModule.Designers;
using ScriptModule.Scripts;
using ScriptModuleModel;

namespace ScriptModule.ViewModels
{
    public class ScriptRowViewModel : ViewModelBase
    {
        public enum DesingMode { Design = 0, Source = 1 }

        private readonly ScriptRow _scriptRow;

        public override object Model
        {
            get { return _scriptRow; }
        }

        public string ScriptName
        {
            get { return _scriptRow.Scriptname; }
            set { _scriptRow.Scriptname = value; OnPropertyChanged("ScriptName"); }
        }

        public string ScriptText
        {
            get { return _scriptRow.Scripttext; }
            set { _scriptRow.Scripttext = value; OnPropertyChanged("ScriptText"); }
        }

        private DesignerControl _scriptDesigner;
        public DesignerControl ScriptDesigner
        {
            get { return _scriptDesigner; }
            set { _scriptDesigner = value; OnPropertyChanged("ScriptDesigner"); }
        }

        public ScriptRowViewModel(ScriptRow scriptRow)
        {
            this._scriptRow = scriptRow;
            InitializeDesigner();
        }

        private void InitializeDesigner()
        {
            if (_scriptRow.Scripttext != null)
                ScriptDesigner = DesignerControl.CreateDesigner(ScriptBase.GetScript(_scriptRow.Scripttext));
        }

        private DesingMode _designMode = DesingMode.Design;
        public int DesignMode
        {
            get { return (int)_designMode; }
            set { _designMode = (DesingMode)value; OnTabIndexChanged(); }
        }

        private void OnTabIndexChanged()
        {
            switch (_designMode)
            {
                case DesingMode.Design:
                    ScriptDesigner = DesignerControl.CreateDesigner(ScriptBase.GetScript(_scriptRow.Scripttext));
                    break;
                case DesingMode.Source:
                    if (_scriptDesigner != null)
                        ScriptText = ScriptBase.GetScriptText(_scriptDesigner.GetScript());
                    break;
            }
            OnPropertyChanged("DesignMode");
        }

        private bool _renameMode;
        public bool RenameMode
        {
            get { return _renameMode; }
            set { _renameMode = value; OnPropertyChanged("RenameMode"); }
        }
    }
}
