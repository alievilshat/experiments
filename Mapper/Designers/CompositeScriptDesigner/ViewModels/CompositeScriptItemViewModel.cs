using ScriptModule.Scripts;
using ScriptModule.ViewModels;

namespace ScriptModule.Designers.CompositeScriptDesigner.ViewModels
{
    class CompositeScriptItemViewModel : ViewModelBase
    {
        private readonly IScript _script;

        public override object Model
        {
            get { return _script; }
        }

        public string ScriptName
        {
            get { return _script.GetType().Name; }
        }

        private DesignerControl _scriptDesigner;
        public DesignerControl ScriptDesigner
        {
            get { return _scriptDesigner; }
            set { _scriptDesigner = value; OnPropertyChanged("ScriptDesigner"); }
        }

        public CompositeScriptItemViewModel(IScript script)
        {
            this._script = script;
            ScriptDesigner = DesignerControl.CreateDesigner(script);
        }
    }
}
