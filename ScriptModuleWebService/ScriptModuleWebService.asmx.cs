using ScriptModule.DAL;
using System.ComponentModel;
using System.Web.Services;

namespace ScriptModuleWebService
{
    [WebService(Namespace = "http://navitas.nl/ScriptModule")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class ScriptModuleWebService : WebService
    {
        [WebMethod]
        public int Execute(string login, string password, int scriptid)
        {
            var script = ScriptManager.GetScriptRow(login, password, Context.Request.UserHostAddress, scriptid).GetScript();
            return ExecutionManager.ExecuteScriptAsync(script);
        }

        [WebMethod]
        public ExecutionProgress GetExecutionProgress(int id)
        {
            return ExecutionManager.GetExecutionProgress(id);
        }

        [WebMethod]
        public void AbortExecution(int id)
        {
            ExecutionManager.AbortExecution(id);
        }
    }
}
