using SmartControl.Api.Server;
using SmartControl.Api.Server.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SmartControl.Api
{
    interface IDataSettings : INotifyPropertyChanged
    {
        /// <summary>
        /// Status of device
        /// </summary>
        public StatusResponse Status { set; get; }

        /// <summary>
        /// Version of device
        /// </summary>
        public MyVersion Version { set; get; }

        /// <summary>
        /// Get Calendar status
        /// </summary>
        public CalendarResponse Calendar { set; get; }

        /// <summary>
        /// Get List of Tasks on each day
        /// 7 days (0-6)
        /// </summary>
        public CalendarDays CalendarTasks { set; get; }

        /// <summary>
        /// List of Parameters
        /// </summary>
        public List<ParameterResponse.P> Parameters { set; get; }

        /// <summary>
        /// Modes of work
        /// 1. Boost
        /// 2. Airing
        /// 3. Sleep
        /// 4. Holiday
        /// 5. Max Vent
        /// </summary>
        public List<ModesResponse.P> Modes { set; get; }
    }
}
