﻿using SmartControl.Api.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartControl.Api.Server.Queries
{
    public class VersionQuery
    {
        public MyVersion ApiVersion { set; get; }
        public MyVersion AppVersion { set; get; }
    }
}
