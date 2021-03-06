﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Api.Data;

namespace Api.Responses
{
    public class CalendarDayResponse
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DayOfWeek Day { set; get; }

        /// <summary>
        /// Tasks.
        /// Limit 5 elements per day
        /// </summary>
        public List<CalendarTask> Tasks { set; get; } = new List<CalendarTask>();
    }
}
