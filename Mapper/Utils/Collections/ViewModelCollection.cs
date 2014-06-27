using System.Collections.Generic;
using ScriptModule.ViewModels;

namespace ScriptModule.Utils.Collections
{
    public class ViewModelCollection<TViewModel, TModel> : ViewModelCollectionBase<TViewModel, TModel>
        where TViewModel : class, IViewModel
        where TModel : class
    {
        private readonly ICollection<TModel> _collection; 

        public ViewModelCollection(ICollection<TModel> collection, bool autoFetch = true)
        {
            _collection = collection;
            Initialize(autoFetch);
        }

        protected override IEnumerable<TModel> GetModelCollection()
        {
            return _collection;
        }

        protected override void ClearColleciton()
        {
            _collection.Clear();
        }

        protected override void RemoveModel(TModel m)
        {
            _collection.Remove(m);
        }

        protected override void AddModel(TModel m)
        {
            _collection.Add(m);
        }
    }
}
