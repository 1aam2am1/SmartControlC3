using Api.Data;
using SmartControl.Api.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartControl.Api
{
    public interface IClient
    {
        public DataManager GetDataManager();

        public HistoryData GetHistoricalData(List<int> vs, TimePeriod period);
    }
}
