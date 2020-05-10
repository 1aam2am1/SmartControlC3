using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Api.Data
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum WorkStatus
    {
        Starting,
        Work,
        Erros,
        ByPass,
        Defrost,
        Reheating
    }
}
