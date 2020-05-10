using Api.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Queries
{
    public class VersionQuery
    {
        public MyVersion ApiVersion { set; get; }
        public MyVersion AppVersion { set; get; }
    }
}
