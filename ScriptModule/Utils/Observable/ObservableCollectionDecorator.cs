using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptModule
{
    public class ObservableCollectionDecorator : ObservableDecorator, IEnumerable, INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private ObservableCollectionDecorator(object target)
            : base(target)
        { }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var res = base.TryInvokeMember(binder, args, out result);
            if (!res)
                return false;

            switch (binder.Name)
            {
                case "Add":
                case "Insert":
                    notifyCollectionChanged();
                    break;

                case "Clear":
                    notifyCollectionChanged();
                    break;

                case "Remove":
                case "RemoveAt":
                    notifyCollectionChanged();
                    break;
            }
            return true;
        }

        public override void Update()
        {
            notifyCollectionChanged();
        }

        private void notifyCollectionChanged()
        {
            if (CollectionChanged != null)
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public new static ObservableCollectionDecorator Get(object target)
        {
            if (target == null)
                return null;

            ObservableDecorator res;
            if (wrappers.TryGetValue(target, out res) && res is ObservableCollectionDecorator)
                return (ObservableCollectionDecorator)res;

            res = new ObservableCollectionDecorator(target);
            wrappers[target] = res;
            return (ObservableCollectionDecorator)res;
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)_target).GetEnumerator();
        }

    }
}
