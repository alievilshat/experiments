using System;
using System.IO;
using Microsoft.Win32;

namespace ScriptModule.Scripts.Input
{
    public class InputLocalFile : UIScript
    {
        public string FileName { get; set; }
        public string Filter { get; set; }

        protected override object ExecuteScript(object param = null)
        {
            var dialog = new OpenFileDialog { Filter = Filter, FileName = FileName };
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                return File.ReadAllText(dialog.FileName);
            }
            throw new OperationCanceledException("Execution was canceled by user");
        }
    }
}
