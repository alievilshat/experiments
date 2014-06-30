using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptModule.Designers;
using ScriptModule.Scripts;

namespace ScriptModule.ViewModels
{
    public class DesignerViewModelBase : ViewModelBase
    {
        public enum DesingMode { Design = 0, Source = 1 }

        private DesingMode _designMode = DesingMode.Design;
        public int DesignMode
        {
            get { return (int)_designMode; }
            set { _designMode = (DesingMode)value; OnTabIndexChanged(); }
        }

        private string _scriptText;
        public string ScriptText 
        {
            get { return _scriptText; }
            set { _scriptText = value; OnPropertyChanged("ScriptText"); }
        }

        private void OnTabIndexChanged()
        {
            switch (_designMode)
            {
                case DesingMode.Design:
                    Model = ScriptBase.GetScript(ScriptText);
                    break;
                case DesingMode.Source:
                    ScriptText = ScriptBase.GetScriptText((IScript)Model);
                    break;
            }
            OnPropertyChanged("DesignMode");
        }
    }
}
