using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace ScriptModule.Utils.Collections
{
    public class ObservableCollectionView<T, R> : ObservableCollection<R>
        where T:class
        where R: class
    {
        private Func<T, bool> _filter;
        private Func<T, R> _selector;

        public ObservableCollectionView(ObservableCollection<T> collection, Func<T, bool> filter = null, Func<T, R> selector = null)
        {
            _filter = filter ?? (i => true);
            _selector = selector ?? (i => i as R);

            AddRange(collection);
            CollectionChanged += ObservableCollectionView_CollectionChanged;
        }

        private void AddRange(IEnumerable<T> collection)
        {
            foreach (var i in collection.Where(_filter))
            {
                Add(_selector(i));
            }
        }

        void ObservableCollectionView_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    AddRange(e.NewItems.Cast<T>());
                    break;
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Move:
                case NotifyCollectionChangedAction.Replace:
                case NotifyCollectionChangedAction.Reset:
                default:
                    Clear();
                    AddRange((IEnumerable<T>)sender);
                    break;
            }
        }
    }
}
