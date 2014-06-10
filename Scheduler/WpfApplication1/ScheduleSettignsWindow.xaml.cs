using System;
using System.Windows;
using Microsoft.Win32.TaskScheduler;
using Npgsql;
using System.Linq;
using System.Collections.Generic;
using WpfApplication1.ViewModels;
using WpfApplication1.BL;

namespace WpfApplication1
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class ScheduleSettignsWindow : Window
    {
        public ScheduleSettignsWindow()
            : this(null)
        {
        }

        public ScheduleSettignsWindow(ScriptSchedule scriptSchedule)
        {
            InitializeComponent();

            var context = new ScriptSchedulerViewModel();

            context.ScriptSchedule = scriptSchedule ?? new ScriptSchedule();

            string connectionString = "Server=85.92.146.196;port=5432;Database=bodyview3;UserID=postgres;Password=Banek12";
            string selectQuery = "select scriptid ,scriptname from script";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                NpgsqlCommand commandSelect = new NpgsqlCommand(selectQuery, connection);

                // Execute the query and obtain a result set
                NpgsqlDataReader dr = commandSelect.ExecuteReader();

                // Output rows
                while (dr.Read())
                {
                    context.Scripts.Add(new ScriptItem((int)dr["scriptid"], dr["scriptname"].ToString()));
                }
            }
            DataContext = context;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            var context = DataContext.As<ScriptSchedulerViewModel>();
            DialogResult = true;
            Close();
        }

        private void updatetasskwindow()
        {
            var context = DataContext.As<ScriptSchedulerViewModel>().ScriptSchedule;
            ScheduleManager.UpdateScriptSchedule(context);
        }


        private void SaveSchedulerToDatabase()
        {
            var context = DataContext.As<ScriptSchedulerViewModel>().ScriptSchedule;
            
        }

        private void Synchronize()
        {
            var scriptsschedules = ScheduleManager.GetScriptschedules();
            foreach (var schedule in scriptsschedules)
            {

               
                // Connect to the computer "REMOTE" using credentials
                // TaskService ts = new TaskService("REMOTE", "myusername", "MYDOMAIN", "mypassword");

                using (TaskService ts = new TaskService())
                {
                    Task rt = ts.GetTask(schedule.ScheduleName);
                    CreateNewTask(ts, schedule.Schedule);
                }
            }
        }

        private void CreateNewTask(TaskService ts, Schedule schedulerData)
        {
            Microsoft.Win32.TaskScheduler.Trigger trigger = null;

            // create a new task definition and assign properties
            TaskDefinition td = ts.NewTask();
            td.RegistrationInfo.Description = "does something";

            // create a trigger 
            if (schedulerData.OneTimeChecked)
            {
                trigger = td.Triggers.Add(new TimeTrigger
                {
                    RandomDelay = schedulerData.DelayTaskForUpToValue
                });
            }

            else if (schedulerData.DailyChecked)
            {
                trigger = td.Triggers.Add(new DailyTrigger
                {
                    DaysInterval = schedulerData.DailyCheckedValue,
                    RandomDelay = schedulerData.DelayTaskForUpToValue
                });
            }
            else if (schedulerData.WeeklyCheked)
            {
                trigger = td.Triggers.Add(new WeeklyTrigger
                {
                    WeeksInterval = schedulerData.WeeklySettings.WeeklyChekedValue,
                    RandomDelay = schedulerData.DelayTaskForUpToValue,
                    DaysOfWeek = (DaysOfTheWeek)((schedulerData.WeeklySettings.Monday ? (int)DaysOfTheWeek.Monday : 0)
                                                | (schedulerData.WeeklySettings.Tuesday ? (int)DaysOfTheWeek.Tuesday : 0)
                                                | (schedulerData.WeeklySettings.Wednesday ? (int)DaysOfTheWeek.Wednesday : 0)
                                                | (schedulerData.WeeklySettings.Thursday ? (int)DaysOfTheWeek.Thursday : 0)
                                                | (schedulerData.WeeklySettings.Friday ? (int)DaysOfTheWeek.Friday : 0)
                                                | (schedulerData.WeeklySettings.Saturday ? (int)DaysOfTheWeek.Saturday : 0)
                                                | (schedulerData.WeeklySettings.Sunday ? (int)DaysOfTheWeek.Sunday : 0))
                });

            }

            trigger.StartBoundary = new DateTime(schedulerData.TaskStartDate.Year, schedulerData.TaskStartDate.Month, schedulerData.TaskStartDate.Day,
                                                schedulerData.TaskStartTime.Hour, schedulerData.TaskStartTime.Minute, schedulerData.TaskStartTime.Second);
            trigger.Repetition.Interval = schedulerData.RepateTaskEveryValue;
            trigger.Repetition.Duration = schedulerData.ForDurationOf;
            trigger.ExecutionTimeLimit = schedulerData.StopTaskIfItRunsLongerThanvalue;
            trigger.EndBoundary = new DateTime(schedulerData.ExpireTaskStartDate.Year, schedulerData.ExpireTaskStartDate.Month, schedulerData.ExpireTaskStartDate.Day,
                                               schedulerData.ExpireTaskStartDate.Hour, schedulerData.ExpireTaskStartDate.Minute, schedulerData.TaskStartTime.Second);

            // register the task in the root folder
            td.Actions.Add(new ExecAction("notepad.exe", "c:\\test.log", null));
            //ts.RootFolder.CreateFolder
            //ts.GetFolder("Navitas").GetFolder("ScriptModule").RegisterTaskDefinition(schedulerData.TaskName, td);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
