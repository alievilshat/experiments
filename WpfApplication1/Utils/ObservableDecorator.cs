using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Dynamic;
using System.Reflection;
using WPG;
using System;

namespace WpfApplication1
{
    public class ObservableDecorator : DynamicObject, INotifyPropertyChanged, ITypeDecorator
    {
        protected readonly object _target;
        public event PropertyChangedEventHandler PropertyChanged;

        protected ObservableDecorator(object target)
        {
            this._target = target;
        }

        private PropertyInfo getProperty(string name)
        {
            var property = _target.GetType().GetProperty(name);
            return property;
        }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            if (binder.Type.IsInstanceOfType(_target))
            {
                result = _target;
                return true;
            }
            return base.TryConvert(binder, out result);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var p = getProperty(binder.Name);
            if (p == null)
            {
                result = null;
                return false;
            }
            result = p.GetValue(_target, null);
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var p = getProperty(binder.Name);
            if (p == null)
                return false;

            p.SetValue(_target, value, null);
            notifyPropertyChanged(p.Name);
            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var method = _target.GetType().GetMethod(binder.Name, args.Select(a => a.GetType()).ToArray());
            if (method == null)
            {
                result = null;
                return false;
            }
            result = method.Invoke(_target, args);
            return true;
        }

        protected void notifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        protected static Dictionary<object, ObservableDecorator> wrappers = new Dictionary<object, ObservableDecorator>();

        public static ObservableDecorator Get(object target)
        {
            if (target == null)
                return null;

            ObservableDecorator res;
            if (wrappers.TryGetValue(target, out res))
                return res;

            res = new ObservableDecorator(target);
            wrappers[target] = res;
            return res;
        }

        Type ITypeDecorator.GetDecoratedType()
        {
            return _target.GetType();
        }

        object ITypeDecorator.GetValue(PropertyDescriptor property)
        {
            return property.GetValue(_target);
        }

        void ITypeDecorator.SetValue(PropertyDescriptor property, object value)
        {
            property.SetValue(_target, value);
            notifyPropertyChanged(property.Name);
        }

        public virtual void Update(string prop)
        {
            notifyPropertyChanged(prop);
        }

        public virtual void Update()
        {
            foreach (var prop in _target.GetType().GetProperties().Select(i => i.Name))
            {
                notifyPropertyChanged(prop);
            }
        }
    }
}
