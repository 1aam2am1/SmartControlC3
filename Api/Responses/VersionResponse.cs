using Api.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Responses
{
    public class VersionResponse
    {
        public MyVersion ApiVersion { set; get; }
        public MyVersion ServerVersion { set; get; }
        public MyVersion DeviceVersion { set; get; }
    }
}
