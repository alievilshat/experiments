using System.Collections.Generic;
using System.Data;
using Npgsql;

namespace ScriptModule.Services.DatabaseImport.Registries
{
    class TableKeysRegistry
    {
        // Tablename, ImportedId, SystemId
        Dictionary<string, Dictionary<string, string>> registry;
        string providernamespace;

        public TableKeysRegistry(NpgsqlConnection connection, string namespaces)
        {
            providernamespace = namespaces;
            registry = getRegistry(connection);
        }

        private Dictionary<string, Dictionary<string, string>> getRegistry(NpgsqlConnection connection)
        {
            var tableRegistry = new Dictionary<string, Dictionary<string, string>>();
            using (NpgsqlCommand command = new NpgsqlCommand("importmanager_tablekeysregistery", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("source", providernamespace);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tablename = reader["tablename"].ToString();
                        var importeid = reader["importedid"].ToString();
                        var systemid = reader["systemid"].ToString();
                        if (!tableRegistry.ContainsKey(tablename))
                            tableRegistry.Add(tablename, new Dictionary<string, string>());

                        tableRegistry[tablename].Add(importeid.ToLower(), systemid);

                    }
                }
            }
            return tableRegistry;
        }

        public bool IsImportedIdRegistered(string tablename, string pkval)
        {
            return registry.ContainsKey(tablename) && registry[tablename].ContainsKey(pkval.ToLower());
        }

        public string GetSystemId(string table, string importedid)
        {
            return registry[table][importedid.ToLower()];
        }

        public void RegisterKey(string tablename, string importedid, string systemid, NpgsqlConnection connection)
        {
            if (!registry.ContainsKey(tablename))
                registry[tablename] = new Dictionary<string, string>();

            registry[tablename].Add(importedid.ToLower(), systemid);

            NpgsqlCommand commandinsertkeys = new NpgsqlCommand("importmanager_registerimportedidentifier", connection);
            commandinsertkeys.CommandType = CommandType.StoredProcedure;
            commandinsertkeys.Parameters.AddWithValue("_tablename", tablename);
            commandinsertkeys.Parameters.AddWithValue("_importedid", importedid);
            commandinsertkeys.Parameters.AddWithValue("_systemid", systemid);
            commandinsertkeys.Parameters.AddWithValue("_providernamespace", providernamespace);
            commandinsertkeys.ExecuteNonQuery();
        }
    }
}
