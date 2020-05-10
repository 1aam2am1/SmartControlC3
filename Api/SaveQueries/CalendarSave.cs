using Api.MyJsonConverter;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Api.SaveQueries
{
    public class CalendarSave
    {
        public bool Enabled { set; get; }
        public int ActiveDays { set; get; }

        /// <summary>
        /// Date to send to device.
        /// If Year is lesser than 1700 then date isn't saved.
        /// </summary>
        [JsonConverter(typeof(MyDateConverter))]
        public DateTime Date { set; get; }
    }
}
