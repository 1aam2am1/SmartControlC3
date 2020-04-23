using System;
using System.Collections.Generic;
using System.Text;

namespace SmartControl.Api.Server.Responses
{
    public class HistoryResponse
    {
        public class P
        {
            public class Q
            {
                /// <summary>
                /// Time in seconds from 1.01.1970 UTC
                /// </summary>
                public long T { set; get; }
                /// <summary>
                /// Historical Value of parameter
                /// </summary>
                public int V { set; get; }
            }

            public int Parameter { set; get; }
            public List<Q> V { set; get; }
        }

        public List<P> Parameters { set; get; }
    }
}
