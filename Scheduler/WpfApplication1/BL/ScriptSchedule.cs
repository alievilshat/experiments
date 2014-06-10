using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace WpfApplication1.BL
{
    public class ScriptSchedule : ModelBase
    {
        public ScriptSchedule()
        {
            Schedule = new Schedule();
        }

        private int _scriptScheduleId;
        public int ScriptScheduleId
        {
            get { return _scriptScheduleId; }
            set { _scriptScheduleId = value; OnPropertyChanged("ScriptScheduleId"); }
        }

        private string _scheduleName;
        public string ScheduleName
        {
            get { return _scheduleName; }
            set { _scheduleName = value; OnPropertyChanged("ScheduleName"); }
        }

        private int _scriptId;
        public int ScriptId
        {
            get { return _scriptId; }
            set { _scriptId = value; OnPropertyChanged("ScriptId"); }
        }

        private Schedule _schedule;
        public Schedule Schedule
        {
            get { return _schedule; }
            set { _schedule = value; OnPropertyChanged("Schedule"); }
        }
    }
}
