using SmartControl.Api.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartControl.Api.Server.Responses
{
    public class HistoryResponse
    {
        public Dictionary<int, List<ValueInTime>> Parameters { set; get; } = new Dictionary<int, List<ValueInTime>>();
    }
}
