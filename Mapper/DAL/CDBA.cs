using Npgsql;
using NpgsqlTypes;
using ScriptModule.Properties;
using System;
using System.Data;

namespace ScriptModule.DAL
{
    public class CDBA
    {
        public static NpgsqlConnection GetConnection(string login, string password, string ipaddress)
        {
            return GetConnection(GetConnectionByUserLogin(login, password, ipaddress));
        }

        public static NpgsqlConnection GetPgConnection()
        {
            return GetConnection(GetConnectionStringBySettings());
        }
        private static NpgsqlConnection GetConnection(string connstr)
        {
            return new NpgsqlConnection(connstr);
        }

        private static string GetConnectionByUserLogin(string userLogin, string userPassword, string userIpAddress)
        {
            string connectionString = String.Empty;

            try
            {
                using (NpgsqlConnection sconn = CDBA.GetPgConnection())
                {
                    using (var cmd = new NpgsqlCommand("usp_nsa_getconnectionstringbyloginpass", sconn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("_login", NpgsqlDbType.Varchar).Value = userLogin;
                        cmd.Parameters.Add("_password", NpgsqlDbType.Varchar).Value = userPassword;
                        cmd.Parameters.Add("_ipaddress", NpgsqlDbType.Varchar).Value = userIpAddress;

                        sconn.Open();

                        var res = (string)cmd.ExecuteScalar();
                        return res;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot connect to the master database", ex);
            }
        }

        private static string GetConnectionString(string serverName, string databaseName, string userName, string password, int connectionTimeout, int port, bool _sslEnabled)
        {
            return String.Format("User ID={0};Password={1};Database={2};Server={3};CommandTimeout={4};ConnectionLifeTime={4};Port={5};SSL={6};", userName, password, databaseName, serverName, connectionTimeout, port, _sslEnabled == true ? "true; SSLMode=Require;" : "false");
        }

        private static string GetConnectionStringBySettings()
        {
            var s = ApplicationSettings.Default;

            return GetConnectionString(s.ServerName.ToLower(), s.DatabaseName.ToLower(),
                                       s.UserName.ToLower(), s.Password, s.ConnectionTimeout, s.Port, s.SSLEnabled);
        }
    }
}