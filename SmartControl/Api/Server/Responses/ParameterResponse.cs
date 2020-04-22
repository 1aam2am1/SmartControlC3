using System;
using System.Collections.Generic;
using System.Text;

namespace SmartControl.Api.Server.Responses
{
    public class ParameterResponse
    {
        public class P
        {
            public int I { set; get; }
            public int V { set; get; }
        }

        public List<P> Parameters { set; get; }
    }
}
