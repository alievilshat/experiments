using System.Collections.ObjectModel;
using ScriptModule.Designers;
using ScriptModule.Designers.CompositeScriptDesigner;

namespace ScriptModule.Scripts
{
    public class ScriptCollection : ObservableCollection<IScript>
    { }

    [ScriptDesigner(typeof(CompositeScriptDesignerControl))]
    public class CompositeScript : ScriptBase
    {
        private ScriptCollection _scripts = new ScriptCollection();
        public ScriptCollection Scripts
        {
            get { return _scripts; }
            set { _scripts = value; OnPropertyChanged("Scripts"); }
        }

        protected override object ExecuteScript(object param)
        {
            var res = param;
            foreach (var script in _scripts)
            {
                res = script.Execute(res);
            }
            return res;
        }
    }
}