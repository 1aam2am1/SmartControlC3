using Microsoft.EntityFrameworkCore;
using SmartControl.Api.Data;
using SmartControl.Api.Server;
using SmartControl.Api.Server.Queries;
using SmartControl.Api.Server.Responses;
using SmartControl.Api.Server.SaveQueries;
using SmartControl.Api.SQL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartControl.Api
{
    public class Client : IClient
    {
        /// <summary>
        /// What server to use to communicate with device
        /// </summary>
        public Lazy<IServer> server = new Lazy<IServer>(new HttpServer());
        //public Lazy<IServer> server = new Lazy<IServer>(new FileServer());
        readonly Lazy<HistoryContext> history = new Lazy<HistoryContext>(new HistoryContext());
        private readonly object locker = new object();

        //TODO: Loaded data also update server_data and data
        //TODO: Integration to saving Property lock data
        private readonly DataManager server_data = new DataManager();

        /// <summary>
        /// Connect to server with given settings
        /// </summary>
        /// <param name="s"></param>
        /// <param name="i"></param>
        /// <param name="onConnection">1 when OK 0 if error</param>
        async public void Connect(ConnectSettings s, Credentials i, Action<bool> onConnection)
        {
            try
            {
                StopDataSynchronization();
                var task = await server.Value.Auth(s, i);
                //wait for it to end without blocking the main thread
                if (task)
                {
                    await GetVersion();
                }

                onConnection?.Invoke(task);
            }
            catch
            {
                onConnection?.Invoke(false);
            }
        }

        private readonly DataManager data = new DataManager();
        public DataManager GetDataManager()
        {
            return data;
        }

        public HistoryData GetHistoricalData(List<int> vs, TimePeriod period)
        {
            var result = new HistoryData();

            foreach (var i in vs)
            {
                ParameterHistoryData(i, period, result);
            }


            return result;
        }

        #region History

        SemaphoreSlim _semaphoreHistory = new SemaphoreSlim(1);
        private async void ParameterHistoryData(int key, TimePeriod period, HistoryData loaded)
        {
            var periods = await history.Value.TimePeriods.Where(p => p.SqlParameterId == key)
                .Where(p =>
                 (p.Begining >= period.Begining && p.Begining <= period.End) ||
                 (p.End >= period.Begining && p.End <= period.End))
            .OrderBy(p => p.Begining).ToListAsync();


            List<Task> waitfor = new List<Task>();

            TimePeriod timePeriod = period;
            foreach (var p in periods)
            {
                if (timePeriod.Begining <= p.Begining)
                {
                    TimePeriod r = new TimePeriod();

                    r.Begining = timePeriod.Begining;
                    r.End = p.Begining;
                    if (timePeriod.End <= p.End)
                    {
                        timePeriod.Begining = timePeriod.End;
                    }
                    else
                    {
                        timePeriod.Begining = p.End;
                    }

                    waitfor.Add(GetHistoryDataFromServerAndSave(key, r, loaded));
                }
                else
                {
                    if (timePeriod.End <= p.End)
                    {
                        timePeriod.Begining = timePeriod.End;
                    }
                    else
                    {
                        timePeriod.Begining = p.End;
                    }
                }
            }
            if (timePeriod.Begining != timePeriod.End)
            {
                waitfor.Add(GetHistoryDataFromServerAndSave(key, timePeriod, loaded));
            }

            {
                var saved = await history.Value.Values.Where(p => p.SqlParameterId == key)
                    .Where(p =>
                     (p.SqlValueInTimeId >= period.Begining && p.SqlValueInTimeId <= period.End))
                    .OrderBy(p => p.SqlValueInTimeId)
                .Select(p => (ValueInTime)p).ToListAsync();

                lock (locker)
                {
                    loaded.UpdateParameters(key, saved);
                }
            }

            await Task.WhenAll(waitfor.ToArray());

            await _semaphoreHistory.WaitAsync();
            try
            {
                SqlTimePeriod save = new SqlTimePeriod
                {
                    Begining = period.Begining,
                    End = period.End,
                    SqlParameterId = key
                };
                if (periods.Count >= 1 && periods.First().Begining < save.Begining)
                {
                    save.Begining = periods.First().Begining;
                }
                if (periods.Count >= 1 && periods.First().End > save.End)
                {
                    save.End = periods.First().End;
                }

                history.Value.TimePeriods.RemoveRange(periods);
                history.Value.TimePeriods.Add(save);
            }
            finally
            {
                lock (locker)
                {
                    history.Value.SaveChanges();
                }

                _semaphoreHistory.Release(1);
            }
        }


        private async Task GetHistoryDataFromServerAndSave(int key, TimePeriod period, HistoryData loaded)
        {
            var query = new HistoryQuery
            {
                Begining = period.Begining,
                End = period.End,
                Parameter = new List<int> { key },
            };

            var result = await server.Value.GetHistoricalData(query);

            lock (locker)
            {
                loaded.UpdateParameters(result.Parameters);
            }

            foreach (var v in result.Parameters)
            {
                foreach (var k in v.Value)
                {
                    history.Value.Values.Add(k);
                }
            }
        }


        #endregion

        /// <summary>
        /// Function start data synchronization with connected device.
        /// </summary>
        public void StartDataSynchronization()
        {
            server.Value.StartAsyncPing(PingAction);
        }

        public void StopDataSynchronization()
        {
            server.Value.ClosePingClient();
        }

        #region Internal

        private async Task GetVersion()
        {
            var response = await server.Value.Version();

            lock (locker)
            {
                data.ApiVersion = response.ApiVersion;
                data.DeviceVersion = response.DeviceVersion;
                data.ServerVersion = response.ServerVersion;
            }
        }

        private async Task PingAction(int action)
        {
            try
            {
                switch (action)
                {
                    case -1:
                        StopDataSynchronization();
                        await Task.Delay(3000);
                        StartDataSynchronization();
                        break;
                    case 0:
                        //no change
                        break;
                    case 1:
                        //change
                        await ResolveStatusChange();
                        break;
                }
                await ResolveQuery();
            }
            catch (AggregateException e)
            {
                foreach (var v in e.InnerExceptions)
                {
                    if (v.Message.Length == 0)
                    {
                        //connection error re try
                        StopDataSynchronization();
                        await Task.Delay(1000);
                        StartDataSynchronization();
                    }
                    else
                    {
                        //message from server error
                        Console.WriteLine("Exception: {0}", v.Message);
                    }
                }
            }
            catch
            {
                //some internal error
            }
        }

        private async Task ResolveStatusChange()
        {
            var response = await server.Value.GetFullStatus();
            List<Task> waitfor = new List<Task>();

            if (response.ChangedParameters)
            {
                waitfor.Add(ResolveParametersChange());
            }
            if (response.ChangedCalendar)
            {
                waitfor.Add(ResolveCalendarChange());
            }
            if (response.ChangedMod)
            {
                waitfor.Add(ResolveModChange());
            }

            lock (locker)
            {
                data.BateryLow = response.BateryLow;
                data.ByPass = response.ByPass;
                data.Errors = response.Errors;
                data.Heater = response.Heater;
                data.Status = response.Status;
                data.Work = response.Work;
            }

            await Task.WhenAll(waitfor.ToArray());
        }

        private async Task ResolveParametersChange()
        {
            var query = new ParameterQuery();
            query.Parameter.AddRange(Enumerable.Range(0, 17));
            query.Parameter.AddRange(Enumerable.Range(20, 60));
            query.Parameter.Add(255);

            var parameters = await server.Value.GetParameters(query);
            lock (locker)
            {
                data.UpdateParameters(parameters.Parameters);
            }
        }

        private async Task ResolveCalendarChange()
        {
            List<Task> waitfor = new List<Task>();

            var d = server.Value.GetCalendarData();

            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                waitfor.Add(ResolveTaskChange(day));
            }

            var result = await d;

            lock (locker)
            {
                data.ActiveDays = result.ActiveDays;
                data.CalState = result.CalState;
                data.CalDate = result.Date;
                data.CalEnabled = result.Enabled;
            }

            await Task.WhenAll(waitfor.ToArray());
        }

        private async Task ResolveTaskChange(DayOfWeek day)
        {
            var query = new CalendarDayQuery
            {
                Day = day
            };

            var parametres = await server.Value.GetTask(query);

            lock (locker)
            {
                data.UpdateTask(parametres.Day, parametres.Tasks);
            }
        }

        private async Task ResolveModChange()
        {
            var result = await server.Value.GetModes();

            lock (locker)
            {
                data.UpdateModes(result.Modes);
            }
        }

        SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);
        public async Task ResolveQuery()
        {
            Dictionary<int, int> P;
            Dictionary<DayOfWeek, List<CalendarTask>> T;
            CalendarSave C = null;
            Dictionary<int, ModesStatus> M;
            List<Task> waitfor = new List<Task>();

            await _semaphoreSlim.WaitAsync();
            lock (locker)
            {
                P = data.DiffParameters(server_data);
                T = data.DiffTask(server_data);
                if (data.ActiveDays != server_data.ActiveDays ||
                    data.CalEnabled != server_data.CalEnabled ||
                    data.CalDate != server_data.CalDate)
                {
                    C = new CalendarSave
                    {
                        ActiveDays = data.ActiveDays,
                        Enabled = data.CalEnabled
                    };
                    if (data.CalDate != server_data.CalDate)
                    {
                        C.Date = data.CalDate;
                    }
                    else
                    {
                        C.Date = new DateTime();
                    }
                }
                M = data.DiffModes(server_data);
            }
            try
            {
                if (P.Count != 0)
                {
                    waitfor.Add(ResolveParametersQuery(P));
                }
                if (T.Count != 0)
                {
                    waitfor.Add(ResolveTaskQuery(T));
                }
                if (C != null)
                {
                    waitfor.Add(ResolveCalendarQuery(C));
                }
                if (M.Count != 0)
                {
                    waitfor.Add(ResolveMod(M));
                }
            }
            finally
            {
                _semaphoreSlim.Release();
            }

            await Task.WhenAll(waitfor.ToArray());
        }

        private async Task ResolveParametersQuery(Dictionary<int, int> d)
        {
            var query = new ParameterSave
            {
                Parameters = d,
            };

            var response = await server.Value.SaveParameters(query);

            switch (response.Result)
            {
                case OkStatus.Ok:
                    ///TODO: When wrong set should load new data true
                    lock (locker)
                    {
                        data.UpdateParameters(d);
                    }
                    break;
                case OkStatus.Error:
                    //something wrong
                    break;
                case OkStatus.NoPermission:
                    //you dint have permission
                    break;
            }
        }

        private async Task ResolveTaskQuery(Dictionary<DayOfWeek, List<CalendarTask>> t)
        {
            List<Task> waitfor = new List<Task>();

            foreach (var v in t)
            {
                var query = new CalendarDaySave
                {
                    Day = v.Key,
                    Tasks = v.Value
                };
                waitfor.Add(Task.Run(async () =>
                {
                    var response = await server.Value.SaveDayTask(query);

                    switch (response.Result)
                    {
                        case OkStatus.Ok:
                            lock (locker)
                            {
                                data.UpdateTask(query.Day, query.Tasks);
                            }
                            break;
                        case OkStatus.Error:
                            //something wrong
                            break;
                        case OkStatus.NoPermission:
                            //you dint have permission
                            break;
                    }
                }));
            }

            await Task.WhenAll(waitfor.ToArray());
        }

        private async Task ResolveCalendarQuery(CalendarSave c)
        {
            var query = new CalendarSave
            {
                ActiveDays = c.ActiveDays,
                Enabled = c.Enabled,
                Date = c.Date
            };

            var response = await server.Value.SaveCalendar(query);

            switch (response.Result)
            {
                case OkStatus.Ok:
                    lock (locker)
                    {
                        data.ActiveDays = c.ActiveDays;
                        data.CalEnabled = c.Enabled;
                        data.CalDate = c.Date;
                    }
                    break;
                case OkStatus.Error:
                    //something wrong
                    break;
                case OkStatus.NoPermission:
                    //you dint have permission
                    break;
            }
        }

        private async Task ResolveMod(Dictionary<int, ModesStatus> m)
        {
            var query = new ModesSave
            {
                Modes = m
            };

            var response = await server.Value.SaveModes(query);

            switch (response.Result)
            {
                case OkStatus.Ok:
                    lock (locker)
                    {
                        data.UpdateModes(m);
                    }
                    break;
                case OkStatus.Error:
                    //something wrong
                    break;
                case OkStatus.NoPermission:
                    //you dint have permission
                    break;
            }
        }

        #endregion
    }
}
