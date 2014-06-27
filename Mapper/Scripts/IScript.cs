using System;
using System.ComponentModel;

namespace ScriptModule.Scripts
{
    public interface IScript
    {
        object Execute(object param = null);

        event ProgressChangedEventHandler ProgressChanged;
    }
}
