using Npgsql;

namespace ScriptModule.DAL
{
    // TODO: Move to EF
    public class ScriptManager
    {
        public static ScriptRow GetScriptRow(string login, string password, string userhostaddress, int scriptid)
        {
            using (var con = CDBA.GetConnection(login, password, userhostaddress))
            {
                con.Open();
                return GetScriptRow(con, scriptid);
            }
        }

        public static ScriptRow GetScriptRow(NpgsqlConnection con, int scriptid)
        {
            using (var command = new NpgsqlCommand("select * from script where scriptid = @scriptid", con))
            {
                command.Parameters.AddWithValue("scriptid", scriptid);
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                        return null;

                    return new ScriptRow
                    {
                        ScriptId = (int)reader["scriptid"],
                        ScriptName = (string)reader["scriptname"],
                        ScriptText = (string)reader["scripttext"]
                    };
                }
            }
        }
    }
}
