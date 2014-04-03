using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace Mapper
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public const string XSL_NAMESPACE = "http://www.w3.org/1999/XSL/Transform";
        public const string MAPPER_NAMESPACE = "http://www.navitas.nl/2014/Mapper";

        public event PropertyChangedEventHandler PropertyChanged;
        protected bool ThrowOnInvalidPropertyName;

        protected virtual void OnPropertyChanged(string propertyName) 
        { 
            this.VerifyPropertyName(propertyName); 
            PropertyChangedEventHandler handler = this.PropertyChanged; 
            if (handler != null) 
            { 
                var e = new PropertyChangedEventArgs(propertyName); 
                handler(this, e); 
            } 
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

        protected static RichTextBox CreateRichTextBox()
        {
            var rtb = new RichTextBox();
            rtb.Document = new FlowDocument();
            BindingOperations.SetBinding(rtb, RichTextBox.WidthProperty,
                new Binding("ActualWidth") { RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(ContentControl), 1) });
            return rtb;
        }
    }
}
