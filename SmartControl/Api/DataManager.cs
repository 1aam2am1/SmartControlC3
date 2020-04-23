using SmartControl.Api.Server;
using SmartControl.Api.Server.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace SmartControl.Api
{
    public class DataManager : IConnectSettings, ILoginSettings, IDataSettings
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string _Url;
        public string Url
        {
            get => _Url;
            set => _Url = value;
        }

        int _Port;
        public int Port
        {
            get => _Port;
            set => _Port = value;
        }

        Credentials _credentials = new Credentials();
        public Credentials Credentials
        {
            get => _credentials;
            set => _credentials = value;
        }

        StatusResponse _status = new StatusResponse();
        public StatusResponse Status
        {
            get => _status;
            set
            {
                _status = value;
                NotifyPropertyChanged();
            }
        }

        CalendarResponse _calendar = new CalendarResponse();
        public CalendarResponse Calendar
        {
            get => _calendar;
            set
            {
                _calendar = value;
                NotifyPropertyChanged();
            }
        }

        MyVersion _version = new MyVersion();
        public MyVersion Version
        {
            get => _version;
            set
            {
                _version = value;
                NotifyPropertyChanged();
            }
        }

        CalendarDays _calendarTasks = new CalendarDays();
        public CalendarDays CalendarTasks
        {
            get => _calendarTasks;
            set
            {
                _calendarTasks = value;
                NotifyPropertyChanged();
            }
        }
        //TODO: Move parameters to different class
        List<ParameterResponse.P> _parameters = new List<ParameterResponse.P>();
        public List<ParameterResponse.P> Parameters
        {
            get => _parameters;
            set
            {
                _parameters = value;
                NotifyPropertyChanged();
            }
        }
        //TODO: Move modes from list to different fields
        List<ModesResponse.P> _modes = new List<ModesResponse.P>();
        public List<ModesResponse.P> Modes
        {
            get => _modes;
            set
            {
                _modes = value;
                NotifyPropertyChanged();
            }
        }

        void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
