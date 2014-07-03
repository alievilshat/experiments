using System.Collections.Generic;
using System.Data;
using Npgsql;

namespace ScriptModule.Services.DatabaseImport.Registries
{
    class TableColumnRegistry
    {
        // Tablename, Column, Type
        private readonly Dictionary<string, Dictionary<string, string>> registry;

        public TableColumnRegistry(NpgsqlConnection connection)
        {
            registry = getRegistry(connection);
        }

        private Dictionary<string, Dictionary<string, string>> getRegistry(NpgsqlConnection connection)
        {
            var tableRegistry = new Dictionary<string, Dictionary<string, string>>();

            using (var command = new NpgsqlCommand("importmanager_tablecolumnsregistry", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tablename = reader["table_name"].ToString();
                        var columnname = reader["column_name"].ToString();
                        var columntype = reader["data_type"].ToString();

                        if (!tableRegistry.ContainsKey(tablename))
                            tableRegistry.Add(tablename, new Dictionary<string, string>());

                        tableRegistry[tablename].Add(columnname, columntype);
                    }
                }
            }

            return tableRegistry;
        }

        public bool IsColumnExists(string tablename, string column)
        {
            return registry.ContainsKey(tablename)
                && registry[tablename].ContainsKey(column);
        }
        public string GetColumnType(string tablename, string columnname)
        {
            return registry[tablename][columnname];
        }
    }
}
