using SmartControl.Api.Server.MyJsonConverter;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SmartControl.Api.Server.Responses
{
    public class CalendarResponse
    {
        public bool Enabled { set; get; }
        public int CalState { set; get; }

        /// <summary>
        /// 1&lt;&lt;0 Monday
        /// 1&lt;&lt;6 Sunday
        /// </summary>
        public int ActiveDays { set; get; }

        [JsonConverter(typeof(MyDateConverter))]
        public DateTime Date { set; get; }
    }
}
