using System;
using System.Collections.Generic;
using System.Text;

namespace SmartControl.Api.Server.Responses
{
    public class StatusPingLowResponse
    {
        /// <summary>
        /// Is there change in device
        /// 0 No change
        /// 1 Change
        /// </summary>
        public int NoChange { set; get; }
    }
}
