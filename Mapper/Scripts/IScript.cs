using System;
using System.ComponentModel;

namespace ScriptModule.Scripts
{
    public interface IScript
    {
        void Execute();

        event ProgressChangedEventHandler ProgressChanged;
        event RunWorkerCompletedEventHandler ExecutionComplited;
    }
}
