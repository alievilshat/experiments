using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using ScriptModule.ViewModels;

namespace ScriptModule.Utils
{
    /// <summary>
    /// Observable collection of ViewModels that pushes changes to a related collection of models
    /// </summary>
    /// <typeparam name="TViewModel">Type of ViewModels in collection</typeparam>
    /// <typeparam name="TModel">Type of models in underlying collection</typeparam>
    public abstract class ViewModelCollectionBase<TViewModel, TModel> : ObservableCollection<TViewModel>
        where TViewModel : class, IViewModel
        where TModel : class
    {
        private bool _synchDisabled;

        protected Func<TModel, TViewModel> ViewModelConstructor;
        protected abstract IEnumerable<TModel> GetModelCollection();
        protected abstract void ClearColleciton();
        protected abstract void RemoveModel(TModel m);
        protected abstract void AddModel(TModel m);

        protected ViewModelCollectionBase()
        {
            CollectionChanged += ViewModelCollectionChanged;
        }

        protected void Initialize(bool autoFetch = true, Func<TModel, TViewModel> viewModelConstructor = null)
        {
            ViewModelConstructor = viewModelConstructor;

            var collection = GetModelCollection();
            if (collection is ObservableCollection<TModel>)
            {
                var observableModels = collection as ObservableCollection<TModel>;
                observableModels.CollectionChanged += ModelCollectionChanged;
            }
            if (autoFetch) FetchFromModels();
        }


        /// <summary>
        /// CollectionChanged event of the ViewModelCollection
        /// </summary>
        public override sealed event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add { base.CollectionChanged += value; }
            remove { base.CollectionChanged -= value; }
        }

        /// <summary>
        /// Load VM collection from model collection
        /// </summary>
        public void FetchFromModels()
        {
            // Deactivate change pushing
            _synchDisabled = true;

            // Clear collection
            Clear();

            // Create and add new VM for each model
            foreach (var model in GetModelCollection())
                AddForModel(model);

            // Reactivate change pushing
            _synchDisabled = false;
        }

        private void ViewModelCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Return if synchronization is internally disabled
            if (_synchDisabled) return;

            // Disable synchronization
            _synchDisabled = true;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var m in e.NewItems.OfType<IViewModel>().Select(v => v.Model).OfType<TModel>())
                        AddModel(m);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (var m in e.OldItems.OfType<IViewModel>().Select(v => v.Model).OfType<TModel>())
                        RemoveModel(m);
                    break;

                case NotifyCollectionChangedAction.Reset:
                    ClearColleciton();
                    foreach (var m in e.NewItems.OfType<IViewModel>().Select(v => v.Model).OfType<TModel>())
                        AddModel(m);
                    break;
            }

            //Enable synchronization
            _synchDisabled = false;
        }

        private void ModelCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_synchDisabled) return;
            _synchDisabled = true;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var m in e.NewItems.OfType<TModel>())
                        Add(CreateViewModel(m));
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (var m in e.OldItems.OfType<TModel>())
                        Remove(GetViewModelOfModel(m));
                    break;

                case NotifyCollectionChangedAction.Reset:
                    Clear();
                    FetchFromModels();
                    break;
            }

            _synchDisabled = false;
        }

        private TViewModel CreateViewModel(TModel model)
        {
            if (ViewModelConstructor != null)
                return ViewModelConstructor(model);

            // Constructor with 1 argument
            return (TViewModel)Activator.CreateInstance(typeof (TViewModel), model);
        }

        private TViewModel GetViewModelOfModel(TModel model)
        {
            if (model == null)
                return null;
            return Items.OfType<IViewModel>().FirstOrDefault(v => v.Model == model) as TViewModel;
        }

        /// <summary>
        /// Adds a new ViewModel for the specified Model instance
        /// </summary>
        /// <param name="model">Model to create ViewModel for</param>
        public void AddForModel(TModel model)
        {
            Add(CreateViewModel(model));
        }

        /// <summary>
        /// Adds a new ViewModel with a new model instance of the specified type,
        /// which is the ModelType or derived from the Model type
        /// </summary>
        /// <typeparam name="TSpecificModel">Type of Model to add ViewModel for</typeparam>
        public void AddNew<TSpecificModel>() where TSpecificModel : TModel, new()
        {
            var m = new TSpecificModel();
            Add(CreateViewModel(m));
        }
    }
}
