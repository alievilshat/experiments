using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using ScriptModule.Scripts;

namespace ScriptModule.Designers
{
    public class DesignerControl : ContentControl
    {
        public IWindowManger WindowManger { get; set; }

        static DesignerControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DesignerControl),
                       new FrameworkPropertyMetadata(typeof(DesignerControl)));
        }

        protected void ShowDesigner(DesignerControl designer)
        {
            if (WindowManger != null)
                WindowManger.ShowWindow(designer);
        }

        public virtual IScript GetScript()
        {
            throw new NotImplementedException("GetScript implementation for " + GetType() + " is missing.");
        }

        public static DesignerControl CreateDesigner(IScript script)
        {
            var attr = (ScriptDesignerAttribute)script.GetType().GetCustomAttribute(typeof(ScriptDesignerAttribute));
            if (attr == null)
                throw new ApplicationException("Designer for " + script.GetType() + "is missing.");

            return (DesignerControl)Activator.CreateInstance(attr.DesignerType, script);
        }
    }
}