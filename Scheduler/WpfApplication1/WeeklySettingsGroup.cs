using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WpfApplication1
{
    public class WeeklySettingsGroup : INotifyPropertyChanged
    {
       
        private short _weeklyValue;
        public short WeeklyChekedValue
        {
            get { return _weeklyValue; }
            set { _weeklyValue = value; OnPropertyChanged("WeeklyChekedValue"); }
        }

        private bool _sunday;
        public bool Sunday
        {
            get { return _sunday; }
            set { _sunday = value; OnPropertyChanged("Sunday"); }
        }

        private bool _monday;
        public bool Monday
        {
            get { return _monday; }
            set { _monday = value; OnPropertyChanged("Sunday"); }
        }

        private bool _tuesday;
        public bool Tuesday
        {
            get { return _tuesday; }
            set { _tuesday = value; OnPropertyChanged("Tuesday"); }
        }

        private bool _wednesday;
        public bool Wednesday
        {
            get { return _wednesday; }
            set { _wednesday = value; OnPropertyChanged("Wednesday"); }
        }

        private bool _thursday;
        public bool Thursday
        {
            get { return _thursday; }
            set { _thursday = value; OnPropertyChanged("Thursday"); }
        }

        private bool _friday;
        public bool Friday
        {
            get { return _friday; }
            set { _friday = value; OnPropertyChanged("Friday"); }
        }

        private bool _saturday;
        public bool Saturday
        {
            get { return _saturday; }
            set { _saturday = value; OnPropertyChanged("Saturday"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
