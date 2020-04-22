using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SmartControl.Api.Server.Responses
{
    public class StatusResponse
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum WorkStatus
        {
            Starting,
            Work,
            Erros,
            ByPass,
            Defrost,
            Reheating
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum ModStatus
        {
            Ventilation,
            Boost,
            Airing,
            OpenWindow,
            Holiday,
            Sleep,
            GWC,
            Calendar
        }

        public WorkStatus Status { set; get; }
        public ModStatus Work { set; get; }
        public bool Heater { set; get; }
        public bool ByPass { set; get; }

        /// <summary>
        /// Errors and parts of calendar
        /// </summary>
        public int Errors { set; get; }
        public bool BateryLow { set; get; }

        /// <summary>
        /// If changed some data then true
        /// </summary>
        public bool ChangedParameters { set; get; }
        public bool ChangedCalendar { set; get; }
        public bool ChangedMod { set; get; }
    }
}
