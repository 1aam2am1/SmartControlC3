using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using SmartControl.Api.Server.Responses;
using SmartControl.Api.Server.Queries;
using SmartControl.Api.Server.SaveQueries;
using SmartControl.Api.Data;

namespace SmartControl.Api.Server
{
    public class HttpServer : IServer
    {
        private HttpClientHandler handler;
        private HttpClient http;


        private readonly static string authSite = "auth";
        private readonly static string version = "version";
        private readonly static string status = "status";
        private readonly static string statusPing = status + "/ping";
        private readonly static string parameters = "parameters";
        private readonly static string calendar = "calendar";
        private readonly static string modes = "modes";
        private readonly static string history = "history";


        public HttpServer()
        {
            handler = new HttpClientHandler();
            handler.Credentials = new NetworkCredential();
            handler.PreAuthenticate = true;

            http = new HttpClient(handler);
        }

        public Task<bool> Auth(ConnectSettings s, Credentials i)
        {
            //handler.Credentials = new NetworkCredential(i.Credentials.UserName, i.Credentials.Password);
            (handler.Credentials as NetworkCredential).UserName = i.UserName;
            (handler.Credentials as NetworkCredential).Password = i.Password;

            try
            {
                var u = new UriBuilder(s.Url);
                if (s.Port > 1024)
                {
                    u.Port = s.Port;
                }
                /*
                if(u.Scheme == "http")
                {
                    u.Scheme = "https";
                }
                */

                http.BaseAddress = u.Uri;
                //http.BaseAddress = new Uri("https://httpbin.org/");
            }
            catch
            {
                return Task.Run(() => false);
            }

            return Task.Run(async () =>
            {
                try
                {
                    var response = await http.GetAsync(authSite);

                    response.EnsureSuccessStatusCode();

                    var json = JsonSerializer.Deserialize<PasswdResponse>(await response.Content.ReadAsStringAsync());
                    var s = await response.Content.ReadAsStringAsync();


                    if (json.User == i.UserName && json.Authenticated)
                    {
                        return true;
                    }
                    return false;
                }
                catch
                {
                    return false;
                }
            });
        }

        public Task<VersionResponse> Version()
        {
            throw new NotImplementedException();
        }

        public Task<StatusResponse> GetFullStatus()
        {
            throw new NotImplementedException();
        }

        public void StartAsyncPing(Action<int> v)
        {
            throw new NotImplementedException();
        }

        public void ClosePingClient()
        {
            throw new NotImplementedException();
        }

        public Task<ParameterResponse> GetParameters(ParameterQuery parameter)
        {
            throw new NotImplementedException();
        }

        public Task<CalendarResponse> GetCalendarData()
        {
            throw new NotImplementedException();
        }

        public Task<CalendarDayResponse> GetTask(CalendarDayQuery day)
        {
            throw new NotImplementedException();
        }

        public Task<ModesResponse> GetModes()
        {
            throw new NotImplementedException();
        }

        public Task<HistoryResponse> GetHistoricalData(HistoryQuery history)
        {
            throw new NotImplementedException();
        }

        public Task<OkErrorResponce> SaveParameters(ParameterSave parameter)
        {
            throw new NotImplementedException();
        }

        public Task<OkErrorResponce> SaveCalendar(CalendarSave calendar)
        {
            throw new NotImplementedException();
        }

        public Task<OkErrorResponce> SaveDayTask(CalendarDaySave calendar)
        {
            throw new NotImplementedException();
        }

        public Task<OkErrorResponce> SaveModes(ModesSave modes)
        {
            throw new NotImplementedException();
        }
    }
}
