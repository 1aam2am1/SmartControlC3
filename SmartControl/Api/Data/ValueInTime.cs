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
    }
}
