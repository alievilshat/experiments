using ScriptModule.Designers.Default.ViewModels;
using ScriptModule.Scripts;

namespace ScriptModule.Designers.Default
{
    /// <summary>
    /// Interaction logic for DefaultDesigner.xaml
    /// </summary>
    public partial class DefaultDesigner : DesignerControl
    {
        private IScript _script;

        public DefaultDesigner(IScript script)
        {
            this._script = script;
            DataContext = new DefaultDesignerViewModel(script);
            InitializeComponent();
        }
    }
}
