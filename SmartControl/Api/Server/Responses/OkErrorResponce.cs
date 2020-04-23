using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SmartControl.Api.Server.Responses
{
    public class OkErrorResponce
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum R
        {
            /// <summary>
            /// Saved
            /// </summary>
            Ok,
            /// <summary>
            /// Some error
            /// </summary>
            Error,
            /// <summary>
            /// No Permission to get this information
            /// </summary>
            NoPermission,
        }

        public R Result { set; get; }
    }
}
