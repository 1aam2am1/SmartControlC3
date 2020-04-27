using SmartControl.Api.SQL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SmartControl.Api.Data
{
    public struct ValueInTime
    {
        /// <summary>
        /// Time in seconds from 1.01.1970 UTC
        /// </summary>
        [JsonPropertyName("T")]
        public long Time { set; get; }
        /// <summary>
        /// Historical Value of parameter
        /// </summary>
        [JsonPropertyName("V")]
        public int Value { set; get; }

        public static implicit operator ValueInTime(SqlValueInTime v)
        {
            DateTimeOffset localTime = DateTime.SpecifyKind(v.SqlValueInTimeId, DateTimeKind.Utc);

            return new ValueInTime
            {
                Time = localTime.ToUnixTimeSeconds(),
                Value = v.Value
            };
        }
    }
}
