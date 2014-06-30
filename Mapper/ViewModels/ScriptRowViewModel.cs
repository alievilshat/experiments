using System.Data.Objects;
using ScriptModule.Designers;
using ScriptModule.Scripts;
using ScriptModule.Utils.Collections;
using ScriptModuleModel;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ScriptModule.ViewModels
{
    public class ScriptRowViewModel : ViewModelBase
    {
        private readonly ScriptRow _scriptRow;

        public override object Model
        {
            get { return _scriptRow; }
        }

        public int ScriptId
        {
            get { return _scriptRow.Scriptid; }
            set { _scriptRow.Scriptid = value; OnPropertyChanged("ScriptId"); }
        }

        public int? ParentId
        {
            get { return _scriptRow.Parent; }
            set { _scriptRow.Parent = value; OnPropertyChanged("ParentId"); }
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

        private ObservableCollection<ScriptRowViewModel> _scripts;
        public ObservableCollection<ScriptRowViewModel> Scripts
        {
            get { return _scripts; }
            set { _scripts = value; OnPropertyChanged("Scripts"); }
        }
        
        private DesignerControl _scriptDesigner;
        public DesignerControl ScriptDesigner
        {
            get 
            {
                if (_scriptDesigner == null && ScriptText != null)
                {
                    _scriptDesigner = DesignerControl.CreateDesigner(ScriptBase.GetScript(_scriptRow.Scripttext));
                }
                return _scriptDesigner; 
            }
            set { _scriptDesigner = value; OnPropertyChanged("ScriptDesigner"); }
        }

        public ScriptRowViewModel(ObjectSet<ScriptRow> scripts, ScriptRow scriptRow)
        {
            this._scriptRow = scriptRow;
            this._scripts = new ObservableCollection<ScriptRowViewModel>(
                scripts.Where(i => i.Parent == (int?)_scriptRow.Scriptid)
                .ToArray().Select(i => new ScriptRowViewModel(scripts, i)));
        }

        private bool _renameMode;
        public bool RenameMode
        {
            get { return _renameMode; }
            set { _renameMode = value; OnPropertyChanged("RenameMode"); }
        }

        public void SaveChanges()
        {
            if (_scriptDesigner != null)
                _scriptRow.Scripttext = ScriptBase.GetScriptText(ScriptDesigner.GetScript());
            foreach (var s in Scripts)
            {
                s.SaveChanges();
            }
        }
    }
}
