using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    public static class ObservableDecoratorExtensions
    {
        public static ObservableDecorator AsObservable(this object obj)
        {
            if (obj is IList)
                return ObservableCollectionDecorator.Get(obj);
            return ObservableDecorator.Get(obj);
        }
    }
}
