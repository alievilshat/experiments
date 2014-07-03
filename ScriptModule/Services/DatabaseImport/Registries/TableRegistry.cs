using System.Collections.Generic;
using System.Data;
using Npgsql;

namespace ScriptModule.Services.DatabaseImport.Registries
{
    class TableRegistry
    {
        // Tablename, PrimaryKey
        private Dictionary<string, string> registry;

        public TableRegistry(NpgsqlConnection connection)
        {
            registry = getRegistry(connection);
        }

        private Dictionary<string, string> getRegistry(NpgsqlConnection connection)
        {
            var tableRegistry = new Dictionary<string, string>();

            using (NpgsqlCommand command = new NpgsqlCommand("importmanager_tableregistery", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        string tablename = reader["tablename"].ToString();
                        string primaryKey = reader["pkname"].ToString();
                        if (!tableRegistry.ContainsKey(tablename))
                        {
                            tableRegistry.Add(tablename, primaryKey);
                        }
                    }
                }
            }
            return tableRegistry;
        }

        public bool IsTableRegistered(string tablename)
        {
            return registry.ContainsKey(tablename);
        }

        public string GetPrimaryKey(string tablename)
        {
            return registry[tablename];
        }
    }
}
