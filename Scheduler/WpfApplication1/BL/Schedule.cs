using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using WpfApplication1.BL;

namespace WpfApplication1
{
    [Serializable]
    public class Schedule : ModelBase
    {
        private bool _oneTimeChecked = true;
        public bool OneTimeChecked
        {
            get { return _oneTimeChecked; }
            set { _oneTimeChecked = value; OnPropertyChanged("OneTimeChecked"); }
        }

        private bool _daily;
        public bool DailyChecked
        {
            get { return _daily; }
            set { _daily = value; OnPropertyChanged("DailyChecked"); }
        }

        private short _dailyValue;
        public short DailyCheckedValue
        {
            get { return _dailyValue; }
            set { _dailyValue = value; OnPropertyChanged("DailyCheckedValue"); }
        }

        private bool _weeklyChecked;
        public bool WeeklyCheked
        {
            get { return _weeklyChecked; }
            set { _weeklyChecked = value; OnPropertyChanged("WeeklyCheked"); }
        }

        private WeeklySettingsGroup _weeklySettings = new WeeklySettingsGroup();
        public WeeklySettingsGroup WeeklySettings
        {
            get { return _weeklySettings; }
            set { _weeklySettings = value; OnPropertyChanged("WeeklySettings"); }
        }

        private DateTime _taskStartDate;
        public DateTime TaskStartDate
        {
            get { return _taskStartDate; }
            set { _taskStartDate = value; OnPropertyChanged("TaskStartDate"); }
        }

        private DateTime _taskStartTime;
        public DateTime TaskStartTime
        {
            get { return _taskStartTime; }
            set { _taskStartTime = value; OnPropertyChanged("TaskStartTime"); }
        }

        private bool _synchronizeAcrossTimeZones;
        public bool SynchronizeAcrossTimeZones
        {
            get { return _synchronizeAcrossTimeZones; }
            set { _synchronizeAcrossTimeZones = value; OnPropertyChanged("SynchronizeAcrossTimeZones"); }
        }
       
        private bool _delayTaskForUpTo;
        public bool DelayTaskForUpTo
        {
            get { return _delayTaskForUpTo; }
            set { _delayTaskForUpTo = value;OnPropertyChanged("DelayTaskForUpTo");  }
        }

        private TimeSpan _delayTaskForUpToValue;
        public TimeSpan DelayTaskForUpToValue
        {
            get { return _delayTaskForUpToValue; }
            set { _delayTaskForUpToValue = value; OnPropertyChanged("DelayTaskForUpTo"); }
        }

        private TimeSpan _forDurationOf;
        public TimeSpan ForDurationOf
        {
            get { return _forDurationOf; }
            set { _forDurationOf = value; OnPropertyChanged("ForDurationOf"); }
        }

        private bool _repateTaskEvery;
        public bool RepateTaskEvery
        {
            get { return _repateTaskEvery; }
            set { _repateTaskEvery = value;OnPropertyChanged("RepateTaskEvery"); }
        }

        private TimeSpan _repateTaskEveryValue;
        public TimeSpan RepateTaskEveryValue
        {
            get { return _repateTaskEveryValue; }
            set { _repateTaskEveryValue = value; OnPropertyChanged("RepateTaskEveryValue"); }
        }

        private bool _stopAllRunningTaskAtEndOfRepetitionDuration;
        public bool StopAllRunningTaskAtEndOfRepetitionDuration
        {
            get { return _stopAllRunningTaskAtEndOfRepetitionDuration; }
            set { _stopAllRunningTaskAtEndOfRepetitionDuration = value; OnPropertyChanged("StopAllRunningTaskAtEndOfRepetitionDuration");}
        }

        private bool _stopTaskIfItRunsLongerThanEnabled;
        public bool StopTaskIfItRunsLongerThanEnabled
        {
            get { return _stopTaskIfItRunsLongerThanEnabled; }
            set { _stopTaskIfItRunsLongerThanEnabled = value; OnPropertyChanged("StopTaskIfItRunsLongerThanEnabled"); }
        }

        private TimeSpan _stopTaskIfItRunsLongerThanvalue;
        public TimeSpan StopTaskIfItRunsLongerThanvalue
        {
            get { return _stopTaskIfItRunsLongerThanvalue; }
            set { _stopTaskIfItRunsLongerThanvalue = value; OnPropertyChanged("StopTaskIfItRunsLongerThanvalue"); }
        }
        private bool _expire;
        public bool Expire
        {
            get { return _expire; }
            set { _expire = value;OnPropertyChanged("Expire"); }
        }

        private DateTime _expireTaskStartDate;
        public DateTime ExpireTaskStartDate
        {
            get { return _expireTaskStartDate; }
            set { _expireTaskStartDate = value; OnPropertyChanged("ExpireTaskStartDate"); }
        }

        private DateTime _expireTaskStartTime;
        public DateTime ExpireTaskStartTime
        {
            get { return _expireTaskStartTime; }
            set { _expireTaskStartTime = value; OnPropertyChanged("ExpireTaskStartTime"); }
        }

        private bool _expireSynchronizeAcrossTimeZones;
        public bool ExpireSynchronizeAcrossTimeZones
        {
            get { return _expireSynchronizeAcrossTimeZones; }
            set { _expireSynchronizeAcrossTimeZones = value; OnPropertyChanged("ExpireSynchronizeAcrossTimeZones"); }
        }

        private bool _taskEnabled;
        public bool TaskEnabled
        {
            get { return _taskEnabled; }
            set { _taskEnabled = value; OnPropertyChanged("TaskEnabled"); }
        }
    }
}
