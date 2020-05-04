using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using SmartControl.Api.Extensions;
using System.Linq;
using System.Windows.Data;

//TODO: Remove many unnecessary new and dispose when releasing and knowing as disposable
namespace SmartControl.Api.Data
{
    public class DataManager : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly object _itemsLock = new object();

        public DataManager()
        {
            Task = new ReadOnlyCollection<ObservableCollection<CalendarTask>>(_Task);

            BindingOperations.EnableCollectionSynchronization(Parameters, _itemsLock);
            foreach (var t in _Task)
            {
                BindingOperations.EnableCollectionSynchronization(t, _itemsLock);
            }
            BindingOperations.EnableCollectionSynchronization(Modes, _itemsLock);
        }

        #region Parameters
        public ObservableCollection<int> Parameters { get; } = new ObservableCollection<int>().Populate(256);

        public void UpdateParameters(Dictionary<int, int> value)
        {
            BindingOperations.AccessCollection(Parameters, () =>
                {
                    Parameters.Populate(value.Keys.Max() + 1);
                    foreach (KeyValuePair<int, int> item in value)
                    {
                        Parameters[item.Key] = item.Value;
                    }
                }, true);
        }

        /// <summary>
        /// What should be changed to get the same values
        /// </summary>
        /// <param name="to">To change</param>
        /// <returns></returns>
        public Dictionary<int, int> DiffParameters(DataManager to)
        {
            var result = new Dictionary<int, int>();

            BindingOperations.AccessCollection(Parameters, () =>
            {
                BindingOperations.AccessCollection(to.Parameters, () =>
                {
                    for (int i = 0; i < Parameters.Count; ++i)
                    {
                        var c = Parameters[i];
                        if (i >= to.Parameters.Count || c != to.Parameters[i])
                        {
                            result[i] = c;
                        }
                    }
                }, false);
            }, false);

            return result;
        }

        #endregion

        #region Task
        private readonly List<ObservableCollection<CalendarTask>> _Task = new List<ObservableCollection<CalendarTask>>().Resize(7);

        public readonly ReadOnlyCollection<ObservableCollection<CalendarTask>> Task;
        public void UpdateTask(DayOfWeek day, List<CalendarTask> list)
        {
            BindingOperations.AccessCollection(_Task[(int)day], () =>
            {
                var v = _Task[(int)day];
                v.Copy(list);
            }, true);
        }

        /// <summary>
        /// What should be changed to get the same values
        /// </summary>
        /// <param name="to">To change</param>
        /// <returns></returns>
        public Dictionary<DayOfWeek, List<CalendarTask>> DiffTask(DataManager to)
        {
            var result = new Dictionary<DayOfWeek, List<CalendarTask>>();

            for (int i = 0; i < Task.Count; ++i)
            {
                BindingOperations.AccessCollection(Task[i], () =>
                {
                    BindingOperations.AccessCollection(to.Task[i], () =>
                    {
                        if (!Task[i].SequenceEqual(to.Task[i]))
                        {
                            result[(DayOfWeek)i] = Task[i].ToList();
                        }
                    }, false);
                }, false);
            }


            return result;
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

        /// <summary>
        /// Modes of work
        /// 0. Boost
        /// 1. Airing
        /// 2. Sleep
        /// 3. Holiday
        /// 4. Max Vent
        /// </summary>
        public ObservableCollection<ModesStatus> Modes { get; } = new ObservableCollection<ModesStatus>().Populate(5);

        public void UpdateModes(Dictionary<int, ModesStatus> value)
        {
            BindingOperations.AccessCollection(Modes, () =>
            {
                Modes.Populate(value.Keys.Max() + 1);
                foreach (KeyValuePair<int, ModesStatus> item in value)
                {
                    Modes[item.Key] = item.Value;
                }
            }, true);
        }

        /// <summary>
        /// What should be changed to get the same values
        /// </summary>
        /// <param name="to">To change</param>
        /// <returns></returns>
        public Dictionary<int, ModesStatus> DiffModes(DataManager to)
        {
            var result = new Dictionary<int, ModesStatus>();
            BindingOperations.AccessCollection(Modes, () =>
            {
                BindingOperations.AccessCollection(to.Modes, () =>
                {
                    for (int i = 0; i < Modes.Count; ++i)
                    {
                        var c = Modes[i];
                        if (i >= to.Modes.Count || c != to.Modes[i])
                        {
                            result[i] = c;
                        }
                    }
                }, false);
            }, false);

            return result;
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

        #region Version

        MyVersion _ApiVersion;
        public MyVersion ApiVersion
        {
            get => _ApiVersion;
            set
            {
                _ApiVersion = value;
                NotifyPropertyChanged();
            }
        }

        MyVersion _ServerVersion;
        public MyVersion ServerVersion
        {
            get => _ServerVersion;
            set
            {
                _ServerVersion = value;
                NotifyPropertyChanged();
            }
        }

        MyVersion _DeviceVersion;
        public MyVersion DeviceVersion
        {
            get => _DeviceVersion;
            set
            {
                _DeviceVersion = value;
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
