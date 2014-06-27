using System.Collections.ObjectModel;
using ScriptModule.Scripts;
using ScriptModule.Utils;
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
            ScriptItems.CollectionChanged += (sender, args) =>
            {
                SingleElementComposite = ScriptItems.Count == 1;

            };
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

        private CompositeScriptItemViewModel _singelItem;

        public CompositeScriptItemViewModel SingleItem
        {
            get { return _singelItem; }
            set { _singelItem = value; OnPropertyChanged("SingleItem"); }
        }

    }
}
