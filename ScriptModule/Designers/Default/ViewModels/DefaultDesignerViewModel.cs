using ScriptModule.Scripts;
using ScriptModule.ViewModels;

namespace ScriptModule.Designers.Default.ViewModels
{
    class DefaultDesignerViewModel : DesignerViewModelBase
    {
        private IScript _script;

        public override object Model
        {
            get { return _script; }
            set { _script = (IScript)value; AllPropertiesChanged(); }
        }

        public DefaultDesignerViewModel(IScript script)
        {
            this._script = script;
        }
    }
}
