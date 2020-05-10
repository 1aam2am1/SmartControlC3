using Api.MyJsonConverter;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Api.Queries
{
    public class HistoryQuery
    {
        /// <summary>
        /// List of parameters to get history to.
        /// </summary>
        public List<int> Parameter { set; get; } = new List<int>();

        /// <summary>
        /// From with date begin query
        /// </summary>
        [JsonConverter(typeof(MyDateConverter))]
        public DateTime Begining { set; get; }

        /// <summary>
        /// To with date send last data
        /// </summary>
        [JsonConverter(typeof(MyDateConverter))]
        public DateTime End { set; get; }
    }
}
