using System;

namespace ScriptModule.Designers
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    internal sealed class ScriptDesignerAttribute : Attribute
    {
        public Type DesignerType { get; set; }

        public ScriptDesignerAttribute(Type designerType)
        {
            DesignerType = designerType;
        }
    }
}
