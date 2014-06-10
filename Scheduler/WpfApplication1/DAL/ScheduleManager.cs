using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfApplication1.BL;

namespace WpfApplication1
{
    public class ScheduleManager
    {
         const string connectionString = "Server=85.92.146.196;port=5432;Database=bodyview3;UserID=postgres;Password=Banek12";
        public static void AddScriptSchedule(ScriptSchedule scriptSchedule)
        {
            var res = Serializer.Serialize(scriptSchedule.Schedule);

            
            string insertQuery = "INSERT INTO scriptsschedule(scriptid, schedule, schedulename) VALUES (@scriptid, @schedule, @schedulename)";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {

                NpgsqlCommand commandInsert = new NpgsqlCommand(insertQuery, connection);
                // ScriptID
                commandInsert.Parameters.Add("@scriptid", scriptSchedule.ScriptId);

                //// serialized schedule
                commandInsert.Parameters.Add("@schedule", res);

                //// schecudleName
                commandInsert.Parameters.Add("@schedulename", scriptSchedule.ScheduleName);

                try
                {
                    connection.Open();
                    commandInsert.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static void UpdateScriptSchedule(ScriptSchedule scriptSchedule)
        {
            // TODO: Update existing script schedule in database
             var res = Serializer.Serialize(scriptSchedule.Schedule);

           
            string updateQuery = "update  scriptsschedule set scriptid="+ scriptSchedule.ScriptId + " , schedule='"+ res +  "', schedulename='" + scriptSchedule.ScheduleName +
                 "' where scriptsscheduleid=" + scriptSchedule.ScriptScheduleId;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                
                NpgsqlCommand commandUpdate= new NpgsqlCommand(updateQuery, connection);
            
                try
                {
                    connection.Open();
                    commandUpdate.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }

        public static void DeleteScriptSchedule(int scriptscheduleId)
        {
            // TODO: Delete existing script schedule

            string deleteQuery = "delete from scriptsschedule where scriptsscheduleid  = " + scriptscheduleId;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {

                NpgsqlCommand commandDelete = new NpgsqlCommand(deleteQuery, connection);

                try
                {
                    connection.Open();
                    commandDelete.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }

        public static List<ScriptSchedule> GetScriptschedules()
        {
            string connectionString = "Server=85.92.146.196;port=5432;Database=bodyview3;UserID=postgres;Password=Banek12";
            string selectQuery = "select * from scriptsschedule";
            var listOfschedules = new List<ScriptSchedule>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {

                connection.Open();
                NpgsqlCommand commandSelect = new NpgsqlCommand(selectQuery, connection);

                // Execute the query and obtain a result set
                NpgsqlDataReader dr = commandSelect.ExecuteReader();

                // Output rows
                while (dr.Read())
                {

                    listOfschedules.Add(new ScriptSchedule()
                    {
                        ScriptScheduleId = (int)dr["scriptsscheduleid"],
                        ScheduleName = (string)dr["schedulename"],
                        ScriptId = (int)dr["scriptid"],
                        Schedule = Serializer.Deserialize<Schedule>((string)dr["schedule"])
                    });
                }
            }
            return listOfschedules;
        }
       
    }
}
