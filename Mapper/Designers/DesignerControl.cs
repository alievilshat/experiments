using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using ScriptModule.Scripts;

namespace ScriptModule.Designers
{
    public class DesignerControl : ContentControl
    {
        static DesignerControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DesignerControl),
                       new FrameworkPropertyMetadata(typeof(DesignerControl)));
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