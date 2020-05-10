using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Api.Queries
{
    public class CalendarDayQuery
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DayOfWeek Day { set; get; }
    }
}
