using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;

namespace SmartControl.Api.Data
{
    public class DataManager : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Parameters
        private readonly Dictionary<int, int> _Parameters = new Dictionary<int, int>();
        public ReadOnlyDictionary<int, int> Parameters
        {
            get => new ReadOnlyDictionary<int, int>(_Parameters);
        }
        public void UpdateParameters(Dictionary<int, int> value)
        {
            foreach (KeyValuePair<int, int> item in value)
            {
                _Parameters[item.Key] = item.Value;
            }
            NotifyPropertyChanged("Parameters");
        }
        #endregion

        #region Task
        private readonly Dictionary<DayOfWeek, List<CalendarTask>> _Task = new Dictionary<DayOfWeek, List<CalendarTask>>();
        public ReadOnlyDictionary<DayOfWeek, List<CalendarTask>> Task
        {
            get => new ReadOnlyDictionary<DayOfWeek, List<CalendarTask>>(_Task);
        }
        public void UpdateTask(DayOfWeek day, List<CalendarTask> list)
        {
            _Task[day] = list;
            NotifyPropertyChanged("Task");
        }
        #endregion

        #region Calendar
        int _ActiveDays;
        public int ActiveDays
        {
            get => _ActiveDays;
            set
            {
                _ActiveDays = value;
                NotifyPropertyChanged();
            }
        }

        int _CalState;
        public int CalState
        {
            get => _CalState;
            set
            {
                _CalState = value;
                NotifyPropertyChanged();
            }
        }

        DateTime _CalDate;
        public DateTime CalDate
        {
            get => _CalDate;
            set
            {
                _CalDate = value;
                NotifyPropertyChanged();
            }
        }

        bool _CalEnabled;
        public bool CalEnabled
        {
            get => _CalEnabled;
            set
            {
                _CalEnabled = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Modes
        private readonly Dictionary<int, ModesStatus> _Modes = new Dictionary<int, ModesStatus>();

        /// <summary>
        /// Modes of work
        /// 1. Boost
        /// 2. Airing
        /// 3. Sleep
        /// 4. Holiday
        /// 5. Max Vent
        /// </summary>
        public ReadOnlyDictionary<int, ModesStatus> Modes
        {
            get => new ReadOnlyDictionary<int, ModesStatus>(_Modes);
        }
        public void UpdateModes(Dictionary<int, ModesStatus> value)
        {
            foreach (KeyValuePair<int, ModesStatus> item in value)
            {
                _Modes[item.Key] = item.Value;
            }
            NotifyPropertyChanged("Modes");
        }
        #endregion

        #region Status

        WorkStatus _Status;
        public WorkStatus Status
        {
            get => _Status;
            set
            {
                _Status = value;
                NotifyPropertyChanged();
            }
        }

        ModStatus _Work;
        public ModStatus Work
        {
            get => _Work;
            set
            {
                _Work = value;
                NotifyPropertyChanged();
            }
        }

        bool _Heater;
        public bool Heater
        {
            get => _Heater;
            set
            {
                _Heater = value;
                NotifyPropertyChanged();
            }
        }

        bool _ByPass;
        public bool ByPass
        {
            get => _ByPass;
            set
            {
                _ByPass = value;
                NotifyPropertyChanged();
            }
        }

        int _Errors;
        /// <summary>
        /// Errors and parts of calendar
        /// </summary>
        public int Errors
        {
            get => _Errors;
            set
            {
                _Errors = value;
                NotifyPropertyChanged();
            }
        }

        bool _BateryLow;
        public bool BateryLow
        {
            get => _BateryLow;
            set
            {
                _BateryLow = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
