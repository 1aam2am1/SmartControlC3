using SmartControl.Api.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SmartControl.Api.Server.Responses
{
    public class OkErrorResponce
    {
        public OkStatus Result { set; get; }
    }
}
