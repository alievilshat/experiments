using System.Data.EntityClient;
using ScriptModuleModel;

namespace ScriptModule.DAL
{
    public class ScriptManager
    {
        private readonly string _connectionString;

        public ScriptManager(string login, string password)
        {
            _connectionString = CDBA.GetConnectionByUserLogin(login, password, "::1");
        }

        public ScriptManager()
        {
            // TODO: Remove default constructor
        }

        public ScriptModuleEntities CreateEntitiesContainer()
        {

            //HACK: EntityConnectionString does not accept some parameters
            var con = _connectionString
                .Replace("CommandTimeout=60;ConnectionLifeTime=60;Timeout=60;", string.Empty)
                .Replace("SSL=false;Pooling=true;MinPoolSize=1;MaxPoolSize=20;ApplicationName=WebService;", string.Empty);

            var connection = new EntityConnectionStringBuilder
            {
                Provider = "Devart.Data.PostgreSql",
                Metadata = "res://*/DAL.ScriptModuleModel.csdl|res://*/DAL.ScriptModuleModel.ssdl|res://*/DAL.ScriptModuleModel.msl",
                ProviderConnectionString = con
            };
            return new ScriptModuleEntities(connection.ToString());
        }

        public static ScriptModuleEntities CreateEntitiesContainer(string login, string password)
        {
            var factory = new ScriptManager(login, password);
            return factory.CreateEntitiesContainer();
        }
    }
}
