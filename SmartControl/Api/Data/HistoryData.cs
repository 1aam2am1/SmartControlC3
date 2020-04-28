using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace SmartControl.Api.Data
{
    public class HistoryData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Dictionary<int, Dictionary<long, int>> _data = new Dictionary<int, Dictionary<long, int>>();
        public ReadOnlyDictionary<int, Dictionary<long, int>> Data
        {
            get => new ReadOnlyDictionary<int, Dictionary<long, int>>(_data);
        }

        public void UpdateParameters(Dictionary<int, List<ValueInTime>> value)
        {
            foreach (var v in value)
            {
                foreach (var k in v.Value)
                {
                    _data[v.Key][k.Time] = k.Value;
                }
            }

            NotifyPropertyChanged("Data");
        }

        public void UpdateTarameters(int key, List<ValueInTime> value)
        {
            foreach (var k in value)
            {
                _data[key][k.Time] = k.Value;
            }

            NotifyPropertyChanged("Data");
        }


        void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
