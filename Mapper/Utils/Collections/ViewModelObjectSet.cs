using System;
using System.Collections.Generic;
using System.Data.Objects;
using ScriptModule.ViewModels;

namespace ScriptModule.Utils.Collections
{
    public class ViewModelObjectSet<TViewModel, TModel> : ViewModelCollectionBase<TViewModel, TModel>
        where TViewModel : class, IViewModel
        where TModel : class
    {
        private readonly IObjectSet<TModel> _set;

        public ViewModelObjectSet(IObjectSet<TModel> set, bool autoFetch = true)
        {
            _set = set;
            Initialize(autoFetch);
        }

        protected override IEnumerable<TModel> GetModelCollection()
        {
            return _set;
        }

        protected override void RemoveModel(TModel m)
        {
            _set.Detach(m);
        }

        protected override void AddModel(TModel m)
        {
            _set.Attach(m);
        }

        protected override void ClearColleciton()
        {
            throw new NotSupportedException("Clear operation on object set is not supported");
        }
    }
}
