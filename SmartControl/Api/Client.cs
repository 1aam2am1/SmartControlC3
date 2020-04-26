using SmartControl.Api.Data;
using SmartControl.Api.Server;
using SmartControl.Api.Server.Queries;
using SmartControl.Api.Server.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartControl.Api
{
    public class Client
    {
        /// <summary>
        /// What server to use to communicate with device
        /// </summary>
        //public Lazy<IServer> server = new Lazy<IServer>(new HttpServer());
        public Lazy<IServer> server = new Lazy<IServer>(new FileServer());

        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly object locker = new object();

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
                cancellationTokenSource.Cancel();
                var task = await server.Value.Auth(s, i);
                //wait for it to end without blocking the main thread

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

        private async Task PingAction(int action)
        {
            try
            {
                switch (action)
                {
                    case -1:
                        StopDataSynchronization();
                        await Task.Delay(1000);
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

        public async Task ResolveStatusChange()
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

            Task.WaitAll(waitfor.ToArray());
        }

        public async Task ResolveParametersChange()
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

        public async Task ResolveCalendarChange()
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

            Task.WaitAll(waitfor.ToArray());
        }

        public async Task ResolveTaskChange(DayOfWeek day)
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

        public async Task ResolveModChange()
        {
            var result = await server.Value.GetModes();

            lock (locker)
            {
                data.UpdateModes(result.Modes);
            }
        }

        #endregion
    }
}
