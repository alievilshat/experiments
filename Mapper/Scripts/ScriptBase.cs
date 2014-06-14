using System;
using System.ComponentModel;

namespace ScriptModule.Scripts
{
    public abstract class ScriptBase : IScript
    {
        public void Execute(object param = null)
        {
            try
            {
                var result = ExecuteScript(param);
                OnProgressChanged("Completed", 100);
                OnExecutionComplited(result, null, false);
            }
            catch (OperationCanceledException)
            {
                OnExecutionComplited(null, null, true);
            }
            catch (Exception ex)
            {
                OnExecutionComplited(null, ex, false);
            }
        }

        protected abstract object ExecuteScript(object param = null);

        public event ProgressChangedEventHandler ProgressChanged;
        public event RunWorkerCompletedEventHandler ExecutionComplited;

        protected void OnProgressChanged(string state, int percentage)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged(this, new ProgressChangedEventArgs(percentage, state));
            }
        }

        protected void OnExecutionComplited(object result, Exception error, bool cancelled)
        {
            if (ExecutionComplited != null)
                ExecutionComplited(this, new RunWorkerCompletedEventArgs(result, error, cancelled));
        }
    }
}
