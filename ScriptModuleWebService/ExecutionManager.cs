using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using ScriptModule.Scripts;

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

        public static ExecutionProgress GetExecutionProgress(int id)
        {
            return s_scriptExecutors[id].Progress;
        }

        public static void AbortExecution(int id)
        {
            s_scriptExecutors[id].Abort();
        }
    }
}