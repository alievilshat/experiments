using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace SchemaEditor
{
    class UnionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new UnionCollection(values.OfType<IEnumerable>().ToArray());
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class UnionCollection : IEnumerable, INotifyCollectionChanged
    {
        IEnumerable[] _sources;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public UnionCollection(IEnumerable[] sources)
        {
            this._sources = sources;
            init();
        }

        private void init()
        {
            foreach (var s in _sources.OfType<INotifyCollectionChanged>())
            {
                s.CollectionChanged += (o, e) => notifyCollectionChanged(e);
            }
        }

        public IEnumerator GetEnumerator()
        {
            var res = _sources.SelectMany(i => i.Cast<object>()).ToArray();
            return res.GetEnumerator();
        }


        private void notifyCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
                CollectionChanged(this, e);
        }
    }
}
