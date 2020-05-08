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
using System.Threading;
using System.Reflection;
using System.IO;

namespace SmartControl.Api.Server
{
    public class HttpServer : IServer
    {
        private HttpClientHandler handler;
        private HttpClient http = null;


        private readonly static string authSite = "auth";
        private readonly static string versionSite = "version";

        private readonly static string statusSite = "status";
        private readonly static string statusPingSite = statusSite + "/ping";

        private readonly static string parametersSite = "parameters";
        private readonly static string parametersSaveSite = parametersSite + "/save";

        private readonly static string calendarSite = "calendar";
        private readonly static string calendarSaveSite = calendarSite + "/save";
        private readonly static string calendarDaySaveSite = calendarSite + "/day/save";

        private readonly static string modesSite = "modes";
        private readonly static string modesSaveSite = modesSite + "/save";

        private readonly static string historySite = "history";


        public HttpServer()
        {

        }

        public async Task<bool> Auth(ConnectSettings s, Credentials i)
        {
            handler = new HttpClientHandler
            {
                Credentials = new NetworkCredential(i.UserName, i.Password),
                PreAuthenticate = true
            };


            http?.Dispose();
            http = new HttpClient(handler, true)
            {
                Timeout = Timeout.InfiniteTimeSpan
            };

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
                return false;
            }

            try
            {
                var cts = new CancellationTokenSource();
                cts.CancelAfter(TimeSpan.FromSeconds(30));

                var response = await http.GetAsync(authSite, cts.Token);

                response.EnsureSuccessStatusCode();

                var json = JsonSerializer.Deserialize<PasswdResponse>(await response.Content.ReadAsStringAsync());

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
        }

        public Task<VersionResponse> Version()
        {
            var Version = new VersionQuery
            {
                ApiVersion = new MyVersion { Major = 1, Minor = 0, Build = 0 },
                AppVersion = Assembly.GetExecutingAssembly().GetName().Version
            };

            return SendJsonQuery<VersionResponse, VersionQuery>(Version, versionSite);
        }

        public Task<StatusResponse> GetFullStatus()
        {
            return SendGetQuery<StatusResponse>(statusSite);
        }


        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        public void StartAsyncPing(Func<int, Task> v)
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource = new CancellationTokenSource();

            var token = cancellationTokenSource.Token;

            Task.Run(async () =>
            {
                try
                {
                    var response = await http.GetAsync(statusPingSite, HttpCompletionOption.ResponseHeadersRead, token);

                    if (token.IsCancellationRequested) { throw new RuntimeException(); }

                    response.EnsureSuccessStatusCode();

                    using var body = await response.Content.ReadAsStreamAsync();
                    using var reader = new StreamReader(body);
                    while (!reader.EndOfStream)
                    {
                        string message = reader.ReadLine();
                        if (message == null) { continue; }

                        var json = JsonSerializer.Deserialize<StatusPingLowResponse>(message);

                        await v.Invoke(json.NoChange);
                    }
                }
                catch
                {

                }
                finally
                {
                    if (!token.IsCancellationRequested)
                    {
                        await v.Invoke(-1);
                    }
                }
            }, token);
        }

        public void ClosePingClient()
        {
            cancellationTokenSource.Cancel();
        }

        public Task<ParameterResponse> GetParameters(ParameterQuery parameter)
        {
            return SendJsonQuery<ParameterResponse, ParameterQuery>(parameter, parametersSite);
        }

        public Task<CalendarResponse> GetCalendarData()
        {
            return SendGetQuery<CalendarResponse>(calendarSite);
        }

        public Task<CalendarDayResponse> GetTask(CalendarDayQuery day)
        {
            return SendJsonQuery<CalendarDayResponse, CalendarDayQuery>(day, calendarSite);
        }

        public Task<ModesResponse> GetModes()
        {
            return SendGetQuery<ModesResponse>(modesSite);
        }

        public Task<HistoryResponse> GetHistoricalData(HistoryQuery history)
        {
            return SendJsonQuery<HistoryResponse, HistoryQuery>(history, historySite);
        }

        public Task<OkErrorResponce> SaveParameters(ParameterSave parameter)
        {
            return SendJsonQuery<OkErrorResponce, ParameterSave>(parameter, parametersSaveSite);
        }

        public Task<OkErrorResponce> SaveCalendar(CalendarSave calendar)
        {
            return SendJsonQuery<OkErrorResponce, CalendarSave>(calendar, calendarSaveSite);
        }

        public Task<OkErrorResponce> SaveDayTask(CalendarDaySave calendar)
        {
            return SendJsonQuery<OkErrorResponce, CalendarDaySave>(calendar, calendarDaySaveSite);
        }

        public Task<OkErrorResponce> SaveModes(ModesSave modes)
        {
            return SendJsonQuery<OkErrorResponce, ModesSave>(modes, modesSaveSite);
        }

        /// <summary>
        /// Send JSON Post message
        /// </summary>
        /// <typeparam name="TResult">Return type</typeparam>
        /// <typeparam name="T">Query type</typeparam>
        /// <param name="message">Query to send</param>
        /// <param name="site">Where send message</param>
        /// <returns>Required response or throw</returns>
        private async Task<TResult> SendJsonQuery<TResult, T>(T message, string site)
        {
            HttpResponseMessage response = null;
            try
            {
                var cts = new CancellationTokenSource();
                cts.CancelAfter(TimeSpan.FromSeconds(30));

                var content = new StringContent(JsonSerializer.Serialize(message), Encoding.UTF8, "application/json");
                response = await http.PostAsync(site, content, cts.Token);

                response.EnsureSuccessStatusCode();

                var json = JsonSerializer.Deserialize<TResult>(await response.Content.ReadAsStringAsync());

                return json;
            }
            catch (HttpRequestException)
            {
                throw new RuntimeException(await response?.Content.ReadAsStringAsync());
            }
            catch
            {
                throw new RuntimeException();
            }
        }

        /// <summary>
        /// Send Get request
        /// </summary>
        /// <typeparam name="TResult">Return type</typeparam>
        /// <param name="site">Where send message</param>
        /// <returns></returns>
        private async Task<TResult> SendGetQuery<TResult>(string site)
        {
            HttpResponseMessage response = null;
            try
            {
                var cts = new CancellationTokenSource();
                cts.CancelAfter(TimeSpan.FromSeconds(30));

                response = await http.GetAsync(site, cts.Token);

                response.EnsureSuccessStatusCode();

                var json = JsonSerializer.Deserialize<TResult>(await response.Content.ReadAsStringAsync());

                return json;
            }
            catch (HttpRequestException)
            {
                throw new RuntimeException(await response?.Content.ReadAsStringAsync());
            }
            catch
            {
                throw new RuntimeException();
            }
        }
    }
}
