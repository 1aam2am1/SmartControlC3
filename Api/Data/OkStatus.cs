using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Api.Data
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OkStatus
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
}
