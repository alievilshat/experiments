using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using ScriptModule.Scripts;

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
            Script.ExecutionComplited += (o, e) =>
            {
                Progress.Completed = true;

                if (e.Cancelled)
                {
                    Progress.Canceled = true;
                    return;
                }
                if (e.Error != null)
                {
                    Error = e.Error;
                    return;
                }
                Progress.Result = e.Result;
            };
        }

        private Thread _thread;

        public void Start()
        {
            _thread = new Thread(Script.Execute);
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