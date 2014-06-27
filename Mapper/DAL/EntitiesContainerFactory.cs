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
            return new ScriptModuleEntities();
        }

        public static ScriptModuleEntities CreateEntitiesContainer(string login, string password)
        {
            var factory = new ScriptManager(login, password);
            return factory.CreateEntitiesContainer();
        }
    }
}
