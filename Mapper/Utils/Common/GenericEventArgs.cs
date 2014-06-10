using System;

namespace ScriptModule
{
    public class GenericEventArgs<T> : EventArgs
    {
        public T Value { get; private set; }

        public GenericEventArgs(T value)
        {
            this.Value = value;
        }
    }
}
