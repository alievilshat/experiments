using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Collections.ObjectModel;

namespace ScriptModule.Utils
{
    public class BindingHelper
    {
        class Sync<T>
        {
            private readonly ObservableCollection<T> _source;
            private readonly ObservableCollection<T> _destination;

            public Sync(ObservableCollection<T> source, ObservableCollection<T> destination)
            {
                this._source = source;
                this._destination = destination;
                InitializeCollections(source, destination);

                _source.CollectionChanged += SourceUpdated;
                _destination.CollectionChanged += DestinationUpdated;
            }

            private void InitializeCollections(ObservableCollection<T> source, ObservableCollection<T> destination)
            {
                RemoveRange(destination, destination.Where(i => !source.Contains(i)));
                AddRange(destination, source.Where(i => !destination.Contains(i)));
            }

            private void SourceUpdated(object sender, NotifyCollectionChangedEventArgs e)
            {
                _destination.CollectionChanged -= DestinationUpdated;
                SyncCollection(_destination, e);
                _destination.CollectionChanged += DestinationUpdated;
            }

            private void DestinationUpdated(object sender, NotifyCollectionChangedEventArgs e)
            {
                _source.CollectionChanged -= SourceUpdated;
                SyncCollection(_source, e);
                _source.CollectionChanged += SourceUpdated;
            }

            private void SyncCollection(ObservableCollection<T> destination, NotifyCollectionChangedEventArgs e)
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        AddRange(destination, e.NewItems.Cast<T>());
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        RemoveRange(destination, e.OldItems.Cast<T>());
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }


            private void AddRange(ObservableCollection<T> collection, IEnumerable<T> addItems)
            {
                foreach (var i in addItems)
                    collection.Add(i);
            }

            private void RemoveRange(ObservableCollection<T> collection, IEnumerable<T> removeItems)
            {
                foreach (var i in removeItems)
                    collection.Remove(i);
            }
        }

        public static void BindCollections<T>(ObservableCollection<T> source, ObservableCollection<T> destination)
        {
            new Sync<T>(source, destination);
        }
    }
}