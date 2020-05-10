using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Api.Responses;
using Api.Queries;
using Api.SaveQueries;
using System.Threading;
using System.Runtime.CompilerServices;
using Api.Data;

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

        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        public void StartAsyncPing(Func<int, Task> v)
        {
            NotifyFunctionCalled();

            cancellationTokenSource.Cancel();
            cancellationTokenSource = new CancellationTokenSource();

            var token = cancellationTokenSource.Token;

            Task.Run(async () =>
            {
                NotifyFunctionCalled("Task.Run(async () =>");
                while (token.IsCancellationRequested == false)
                {
                    await Task.Delay(10000, token);
                    file.WriteLine("Async Ping Send 1");
                    file.Flush();
                    await v.Invoke(1);
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
            //result.ChangedParameters = new Random().NextDouble() > 0.5;
            result.ChangedParameters = true;
            result.ChangedCalendar = true;//new Random().NextDouble() > 0.5;
            result.ChangedMod = new Random().NextDouble() > 0.5;

            file.WriteLine("Status Parameters: {0}, Calendar: {1}, Mod: {2}", result.ChangedParameters, result.ChangedCalendar, result.ChangedMod);

            return result;
        }

        public async Task<HistoryResponse> GetHistoricalData(HistoryQuery history)
        {
            NotifyFunctionCalled();

            await Task.Delay(100);

            var result = new HistoryResponse();

            DateTimeOffset localTime = DateTime.UtcNow;

            result.Parameters = new Dictionary<int, List<ValueInTime>>
                {
                    {34, new List<ValueInTime> {
                        new ValueInTime{ Time = localTime.ToUnixTimeSeconds() - 120, Value = 10},
                        new ValueInTime{ Time = localTime.ToUnixTimeSeconds(), Value = 12},
                        }
                    },
                    {35, new List<ValueInTime> {
                        new ValueInTime{ Time = localTime.ToUnixTimeSeconds() - 60, Value = 14},
                        new ValueInTime{ Time = localTime.ToUnixTimeSeconds(), Value = 20},
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
                    {0, new ModesStatus{ Active = true, Value = 50} },
                    {1, new ModesStatus{ Active = false, Value = 60} },
                    {2, new ModesStatus{ Active = false, Value = 0} },
                    {3, new ModesStatus{ Active = true, Value = 0} },
                    {4, new ModesStatus{ Active = false, Value = 20} }
                };


            return result;
        }

        public async Task<ParameterResponse> GetParameters(ParameterQuery parameter)
        {
            NotifyFunctionCalled();

            await Task.Delay(100);

            var result = new ParameterResponse();
            var engine = new Random();
            foreach (var v in parameter.Parameter)
            {
                result.Parameters[v] = engine.Next(0, 100);
            }

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
                    },
                    new CalendarTask
                    {
                        Enabled = false,
                        Hour = 4,
                        Minute = 00,
                        Duration = new Random().Next(0, 600),
                        ExhaustPower = 45,
                        AirflowPower = 50,
                        Heater = true,
                        AirTemperature = 56,
                        Boost = new Random().NextDouble() > 0.5,
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
