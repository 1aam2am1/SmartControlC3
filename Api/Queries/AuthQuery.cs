using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Api.Queries
{
    public class AuthQuery
    {
        [JsonPropertyName("user")]
        public string User { set; get; } = "";

        [JsonPropertyName("password")]
        public string Password { set; get; } = "";
    }
}
