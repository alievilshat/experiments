using System.Windows;

namespace ScriptModule.Designers.XsltScriptDesigner.ViewModels
{
    public interface IMapperHost
    {
        FrameworkElement SourceSchemaControl { get; }
        FrameworkElement TargetSchemaControl { get; }
    }
}
