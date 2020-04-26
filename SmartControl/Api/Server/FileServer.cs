using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SmartControl.Api.Server.Responses;
using SmartControl.Api.Server.Queries;
using SmartControl.Api.Server.SaveQueries;
using System.Threading;
using System.Runtime.CompilerServices;
using SmartControl.Api.Data;

namespace SmartControl.Api.Server
{
    public class FileServer : IServer
    {
        StreamWriter file;
        public FileServer()
        {
            file = new StreamWriter("FileServer.txt");
        }

        public async Task<bool> Auth(ConnectSettings s, Credentials i)
        {
            file.WriteLine("Auth function: {0}:{1} {2} {3}", s.Url, s.Port, i.UserName, i.Password);
            file.Flush();

            await Task.Delay(2000);

            if (i.UserName == "user" && i.Password == "password")
            {
                return true;
            }
            return false;
        }

        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        public void StartAsyncPing(Action<int> v)
        {
            NotifyFunctionCalled();

            cancellationTokenSource.Cancel();

            var token = cancellationTokenSource.Token;

            Task.Run(async () =>
            {
                while (token.IsCancellationRequested == false)
                {
                    await Task.Delay(10000, token);
                    file.WriteLine("Async Ping Send 1");
                    file.Flush();
                    v.Invoke(1);
                }
            }, token);
        }


        public void ClosePingClient()
        {
            NotifyFunctionCalled();

            cancellationTokenSource.Cancel();
        }

        public async Task<CalendarResponse> GetCalendarData()
        {
            NotifyFunctionCalled();

            await Task.Delay(2000);

            var result = new CalendarResponse();
            result.Enabled = true;
            result.CalState = 0b1011;
            result.ActiveDays = 0b1001101;
            result.Date = DateTime.UtcNow;

            return result;

        }

        public async Task<StatusResponse> GetFullStatus()
        {
            NotifyFunctionCalled();

            await Task.Delay(1000);

            var result = new StatusResponse();
            result.Status = WorkStatus.Work;
            result.Work = ModStatus.GWC;
            result.Heater = false;
            result.ByPass = false;
            result.Errors = 0;
            result.BateryLow = false;
            result.ChangedParameters = new Random().NextDouble() > 0.5;
            result.ChangedCalendar = new Random().NextDouble() > 0.5;
            result.ChangedMod = new Random().NextDouble() > 0.5;

            file.WriteLine("Status Parameters: {0}, Calendar: {1}, Mod: {2}", result.ChangedParameters, result.ChangedCalendar, result.ChangedMod);

            return result;
        }

        public async Task<HistoryResponse> GetHistoricalData(HistoryQuery history)
        {
            NotifyFunctionCalled();

            await Task.Delay(100);

            var result = new HistoryResponse();
            TimeSpan interval = DateTime.Now - DateTime.UnixEpoch;

            result.Parameters = new Dictionary<int, List<ValueInTime>>
                {
                    {34, new List<ValueInTime> {
                        new ValueInTime{ Time = (long)Math.Floor(interval.TotalSeconds), Value = 10},
                        new ValueInTime{ Time = (long)Math.Floor(interval.TotalSeconds) - 120, Value = 12},
                        }
                    },
                    {35, new List<ValueInTime> {
                        new ValueInTime{ Time = (long)Math.Floor(interval.TotalSeconds), Value = 14},
                        new ValueInTime{ Time = (long)Math.Floor(interval.TotalSeconds) - 60, Value = 20},
                        }
                    }
                };

            return result;
        }

        public async Task<ModesResponse> GetModes()
        {
            NotifyFunctionCalled();

            await Task.Delay(100);

            var result = new ModesResponse();
            result.Modes = new Dictionary<int, ModesStatus>
                {
                    {1, new ModesStatus{ Active = true, Value = 50} },
                    {2, new ModesStatus{ Active = false, Value = 60} },
                    {3, new ModesStatus{ Active = false, Value = 0} },
                    {4, new ModesStatus{ Active = true, Value = 0} },
                    {5, new ModesStatus{ Active = false, Value = 20} }
                };


            return result;
        }

        public async Task<ParameterResponse> GetParameters(ParameterQuery parameter)
        {
            NotifyFunctionCalled();

            await Task.Delay(100);

            var result = new ParameterResponse();
            result.Parameters = new Dictionary<int, int>
                {
                    {0, 20},
                    {1, 10},
                    {2, 16},
                    {3, 18},
                    {4, 36},
                    {5, 30},
                    {6, 60},
                    {7, 100},
                    {8, 120},
                    {9, 24}
                };

            return result;
        }

        public async Task<CalendarDayResponse> GetTask(CalendarDayQuery day)
        {
            NotifyFunctionCalled();

            await Task.Delay(100);

            var result = new CalendarDayResponse();
            result.Day = day.Day;
            result.Tasks = new List<CalendarTask>
                {
                    new CalendarTask
                    {
                        Enabled = true,
                        Hour = 1,
                        Minute = 52,
                        Duration = 320,
                        ExhaustPower = 20,
                        AirflowPower = 60,
                        Heater = false,
                        AirTemperature = 56,
                        Boost = false,
                    }
                };

            return result;
        }

        public async Task<OkErrorResponce> SaveCalendar(CalendarSave calendar)
        {
            NotifyFunctionCalled();

            await Task.Delay(100);

            var result = new OkErrorResponce();

            result.Result = OkStatus.Ok;

            return result;
        }

        public async Task<OkErrorResponce> SaveDayTask(CalendarDaySave calendar)
        {
            NotifyFunctionCalled();

            await Task.Delay(100);

            var result = new OkErrorResponce();

            result.Result = OkStatus.Error;

            return result;
        }

        public async Task<OkErrorResponce> SaveModes(ModesSave modes)
        {
            NotifyFunctionCalled();

            await Task.Delay(100);

            var result = new OkErrorResponce();

            result.Result = OkStatus.Ok;

            return result;
        }

        public async Task<OkErrorResponce> SaveParameters(ParameterSave parameter)
        {
            NotifyFunctionCalled();

            await Task.Delay(100);

            var result = new OkErrorResponce();

            result.Result = OkStatus.Ok;

            return result;
        }

        public async Task<VersionResponse> Version()
        {
            NotifyFunctionCalled();

            await Task.Delay(100);

            var result = new VersionResponse();

            result.ApiVersion = new MyVersion { Major = 1, Minor = 0, Revision = 0 };
            result.DeviceVersion = new MyVersion { Major = 1, Minor = 0, Revision = 1 };
            result.ServerVersion = new MyVersion { Major = 0, Minor = 0, Revision = 1 };

            return result;
        }


        private void NotifyFunctionCalled([CallerMemberName] string propertyName = "")
        {
            file.WriteLine("{0}() Called", propertyName);
            file.Flush();
        }
    }
}
