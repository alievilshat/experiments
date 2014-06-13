using ScriptModule.Scripts;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace ScriptModuleWebService
{
    public class ExecutionManager
    {
        private static int s_nextJobId;
        private static readonly Dictionary<int, AsyncExecutor> s_scriptExecutors = new Dictionary<int, AsyncExecutor>();

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static int ExecuteScriptAsync(IScript script)
        {
            s_nextJobId++;

            var executor = new AsyncExecutor(script);
            s_scriptExecutors.Add(s_nextJobId, executor);
            executor.Start();

            return s_nextJobId;
        }
    }

    public class AsyncExecutor
    {
        public IScript Script { get; set; }
        public object UserState { get; set; }
        public int ProgressPercentage { get; set; }
        public bool Completed { get; set; }
        public object Result { get; set; }
        public bool Canceled { get; set; }
        public Exception Error { get; set; }
        
        public bool HasException { get { return Error != null; } }

        public AsyncExecutor(IScript script)
        {
            this.Script = script;
            initializeListeners();
        }

        private void initializeListeners()
        {
            Script.ProgressChanged += (o, e) =>
            {
                UserState = e.UserState;
                ProgressPercentage = e.ProgressPercentage;
            };
            Script.ExecutionComplited += (o, e) =>
            {
                Completed = true;

                if (e.Cancelled)
                {
                    Canceled = true;
                    return;
                }
                if (e.Error != null)
                {
                    Error = e.Error;
                    return;
                }
                Result = e.Result;
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