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

        public void SaveParametersQueue(int p, int v);

        public void SaveCalendarQueue(bool Enabled, int ActiveDays, DateTime Date = new DateTime());

        public void SaveCalendarTaskQueue(DayOfWeek day, List<CalendarTask> tasks);

        public void SaveModesQueue(int i, ModesStatus status);
    }
}
