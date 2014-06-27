using ScriptModule.Scripts;
using System;
using System.Threading;

namespace ScriptModuleWebService
{
    public class ExecutionProgress
    {
        public object UserState { get; set; }
        public int ProgressPercentage { get; set; }
        public object Result { get; set; }

        public bool Completed { get; set; }
        public bool Canceled { get; set; }
    }

    public class AsyncExecutor
    {
        public IScript Script { get; private set; }

        private ExecutionProgress _progress;
        public ExecutionProgress Progress
        {
            get
            {
                if (HasError) throw Error;
                return _progress;
            }
            private set { _progress = value; }
        }

        public Exception Error { get; set; }
        public bool HasError { get { return Error != null; } }

        public AsyncExecutor(IScript script)
        {
            this.Script = script;
            this.Progress = new ExecutionProgress();

            initializeListeners();
        }

        private void initializeListeners()
        {
            Script.ProgressChanged += (o, e) =>
            {
                Progress.UserState = e.UserState;
                Progress.ProgressPercentage = e.ProgressPercentage;
            };
        }

        private Thread _thread;

        public void Start()
        {
            _thread = new Thread(() =>
            {
                try
                {
                    var res = Script.Execute(null);

                    Progress.ProgressPercentage = 100;
                    Progress.Result = res;
                }
                catch (OperationCanceledException)
                {
                    Progress.Canceled = true;
                }
                catch (Exception ex)
                {
                    Error = ex;
                }
                Progress.Completed = true;
            });
            _thread.Start();
        }

        public void Abort()
        {
            if (_thread == null)
                return;
            _thread.Abort();
        }
    }
}