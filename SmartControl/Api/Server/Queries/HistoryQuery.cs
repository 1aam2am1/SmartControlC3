using SmartControl.Api.Server.MyJsonConverter;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SmartControl.Api.Server.Queries
{
    public class HistoryQuery
    {
        /// <summary>
        /// List of parameters to get history to.
        /// </summary>
        public ParameterQuery Parameter { set; get; }

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
