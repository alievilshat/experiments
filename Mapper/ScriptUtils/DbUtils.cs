using System.Data;
using Npgsql;

namespace ScriptModule
{
    public class DbUtils
    {
        public static NpgsqlConnection Connection { get; set; }
        public static NpgsqlTransaction Transaction { get; set; }

        public static void BeginTransaction()
        {
            Transaction = Connection.BeginTransaction();
        }

        public static void CommitTransaction()
        {
            Transaction.Commit();
        }

        public static void DisposeConnection()
        {
            Connection.Close();
        }

        public static DataTable Query(string query, string connectionString)
        {
            using (var con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                return performQuery(query, con);
            }
        }

        public static DataTable Query(string query)
        {
            return performQuery(query, Connection);
        }

        public static void SetConnection(string con)
        {
            Connection = new NpgsqlConnection(con);
            Connection.Open();
        }

        private static DataTable performQuery(string query, NpgsqlConnection con)
        {
            using (var cmd = con.CreateCommand())
            {
                cmd.CommandText = query;

                var table = new DataTable("result");

                using (var reader = cmd.ExecuteReader())
                {
                    table.Load(reader);
                }
                return table;
            }
        }
    }
}