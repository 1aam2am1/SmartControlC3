using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SmartControl.Api.Server.Responses
{
    public class PasswdResponse
    {
        [JsonPropertyName("authenticated")]
        public bool Authenticated { set; get; }

        [JsonPropertyName("user")]
        public string User { set; get; } = "";
    }
}
