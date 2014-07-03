using ScriptModule.Designers.CSharpScriptDesigner.ViewModels;
using ScriptModule.Scripts;
using ScriptModule.Scripts.Generic;

namespace ScriptModule.Designers.CSharpScriptDesigner
{
    /// <summary>
    /// Interaction logic for CSharpScriptDesignerControl.xaml
    /// </summary>
    public partial class CSharpScriptDesignerControl : DesignerControl
    {
        private readonly CSharpScript _script;

        public CSharpScriptDesignerControl()
            : this(null)
        { }

        public CSharpScriptDesignerControl(CSharpScript script)
        {
            this._script = script;
            DataContext = new CSharpScriptViewModel(script);
            InitializeComponent();
        }

        public override IScript GetScript()
        {
            return _script;
        }
    }
}
