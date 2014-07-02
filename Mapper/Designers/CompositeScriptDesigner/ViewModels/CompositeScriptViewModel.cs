using ScriptModule.Scripts;
using ScriptModule.Utils.Collections;
using ScriptModule.ViewModels;

namespace ScriptModule.Designers.CompositeScriptDesigner.ViewModels
{
    class CompositeScriptViewModel : ViewModelBase
    {
        private readonly CompositeScript _script;

        public override object Model
        {
            get { return _script; }
        }

        public CompositeScriptViewModel(CompositeScript script)
        {
            this._script = script;
            InitializeListeners();
        }

        private void InitializeListeners()
        {
            ScriptItems = new ViewModelCollection<CompositeScriptItemViewModel, IScript>(_script.Scripts);
        }

        private ViewModelCollection<CompositeScriptItemViewModel, IScript> _scriptItems;
        public ViewModelCollection<CompositeScriptItemViewModel, IScript> ScriptItems
        {
            get { return _scriptItems; }
            set { _scriptItems = value; OnPropertyChanged("ScriptItems"); }
        }

        private bool _singleElementComposite;
        public bool SingleElementComposite
        {
            get { return _singleElementComposite; }
            set { _singleElementComposite = value; OnPropertyChanged("SingleElementComposite"); }
        }


        public void MoveUp(CompositeScriptItemViewModel item)
        {
            var index = ScriptItems.IndexOf(item);
            if (index-1 >= 0)
                ScriptItems.Move(index, index-1);
        }

        public void MoveDown(CompositeScriptItemViewModel item)
        {
            var index = ScriptItems.IndexOf(item);
            if (index + 1 < ScriptItems.Count)
                ScriptItems.Move(index, index + 1);
        }

        public void Remove(CompositeScriptItemViewModel item)
        {
            ScriptItems.Remove(item);
        }
    }
}
