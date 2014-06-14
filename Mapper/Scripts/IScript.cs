using System;
using System.ComponentModel;

namespace ScriptModule.Scripts
{
    public interface IScript
    {
        void Execute(object param = null);

        event ProgressChangedEventHandler ProgressChanged;
        event RunWorkerCompletedEventHandler ExecutionComplited;
    }
}
