using System;
using System.Collections.Generic;
using System.Text;

namespace SmartControl.Api.Server.Responses
{
    public class PasswdResponse
    {
        public bool authenticated { set; get; }
        public string user { set; get; }
    }
}
