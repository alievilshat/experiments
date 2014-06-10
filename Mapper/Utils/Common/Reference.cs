using System.Windows;

namespace ScriptModule
{
    public class Reference<T>
        where T:class
    {
        public T Target { get; set; }

        public Reference(T target)
        {
            this.Target = target;
        }
    }
}
