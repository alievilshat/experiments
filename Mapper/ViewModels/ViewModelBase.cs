using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace ScriptModule.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged, IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected bool ThrowOnInvalidPropertyName;

        public virtual object Model
        {
            get { throw new NotSupportedException("Model property must be overrided by subclass"); }
            set { throw new NotSupportedException("Model property must be overrided by subclass"); }
        }

        protected virtual void OnPropertyChanged(string propertyName, bool verify = true)
        {
            if (verify)
                VerifyPropertyName(propertyName); 

            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        [DebuggerStepThrough]
        [Conditional("DEBUG")]
        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real, 
            // public, instance property on this object. 
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;
                if (this.ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                else
                    Debug.Fail(msg);
            }
        }

        public virtual void AllPropertiesChanged()
        {
            foreach (var p in TypeDescriptor.GetProperties(this).Cast<PropertyDescriptor>())
            {
                OnPropertyChanged(p.Name, false);
            }
        }
    }
}
