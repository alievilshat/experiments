using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using ScriptModule.Designers.Default;
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

        public virtual void AddScript(IScript script)
        {
            ShowMessage("Action is not valid");
        }

        protected void ShowDesigner(DesignerControl designer, string title = null)
        {
            WindowManger.Current.ShowWindow(designer, title);
        }

        protected void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        public virtual IScript GetScript()
        {
            throw new NotImplementedException("GetScript implementation for " + GetType() + " is missing.");
        }

        public static DesignerControl CreateDesigner(IScript script)
        {
            var attr = (ScriptDesignerAttribute)script.GetType().GetCustomAttribute(typeof(ScriptDesignerAttribute));
            if (attr == null)
                return new DefaultDesigner(script);

            return (DesignerControl)Activator.CreateInstance(attr.DesignerType, script);
        }
    }
}