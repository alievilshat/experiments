using System.Collections.Generic;
using System.Data;
using Npgsql;

namespace ScriptModule.Services.DatabaseImport.Registries
{
    class TableForiegnkeysRegistry
    {
        // Tablename, Column, ReferencedTablename
        Dictionary<string, Dictionary<string, string>> registry;

        public TableForiegnkeysRegistry(NpgsqlConnection connection)
        {
            registry = getRegistry(connection);
        }

        private Dictionary<string, Dictionary<string, string>> getRegistry(NpgsqlConnection connection)
        {
            var tableRegistry = new Dictionary<string, Dictionary<string, string>>();

            using (NpgsqlCommand command = new NpgsqlCommand("importmanager_tableforiegnkeysregistry", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tablename = reader["tablename"].ToString();
                        var column = reader["column"].ToString();
                        var reftable = reader["reftable"].ToString();

                        if (!tableRegistry.ContainsKey(tablename))
                            tableRegistry.Add(tablename, new Dictionary<string, string>());

                        if (!tableRegistry[tablename].ContainsKey(column))
                        {
                            tableRegistry[tablename].Add(column, reftable);
                        }
                        else
                        {
                            //if (tableRegistry[tablename][column] != reftable)
                            //    throw new ApplicationException("Database schema has multiple column foreign keys: " + tablename + "." + column);
                        }
                    }
                }
            }
            return tableRegistry;
        }

        public bool IsForeignKey(string tablename, string column)
        {
            return registry.ContainsKey(tablename) && registry[tablename].ContainsKey(column);
        }

        public string GetReferencedTable(string tablename, string column)
        {
            return registry[tablename][column];
        }
    }
}