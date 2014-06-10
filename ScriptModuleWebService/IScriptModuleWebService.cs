using System.ServiceModel;

namespace ScriptModuleWebService
{
    [ServiceContract]
    public interface IScriptModuleWebService
    {
        [OperationContract]
        void Execute(string login, string password, int scriptid);
    }
}
