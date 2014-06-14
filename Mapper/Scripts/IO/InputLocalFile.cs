using System;
using System.IO;
using Microsoft.Win32;

namespace ScriptModule.Scripts.IO
{
    public class InputLocalFile : UIScript
    {
        public string Filter { get; set; }

        protected override object ExecuteScript(object param = null)
        {
            var dialog = new OpenFileDialog { Filter = Filter };
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                return File.ReadAllText(dialog.FileName);
            }
            throw new OperationCanceledException("Execution was canceled by user");
        }
    }
}
