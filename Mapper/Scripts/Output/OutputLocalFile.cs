using System;
using System.IO;
using Microsoft.Win32;

namespace ScriptModule.Scripts.Output
{
    public class OutputLocalFile : ScriptBase
    {
        public string FileName { get; set; }
        public string Filter { get; set; }

        protected override object ExecuteScript(object param = null)
        {
            if (param == null)
                return null;

            var dialog = new SaveFileDialog {Filter = Filter, FileName = FileName };
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                File.WriteAllText(dialog.FileName, param.ToString());
                return param;
            }
            throw new OperationCanceledException("Execution was canceled by user");
        }
    }
}
