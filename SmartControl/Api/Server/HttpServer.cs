using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using SmartControl.Api.Server.Responses;

namespace SmartControl.Api.Server
{
    public class HttpServer : IServer
    {
        private HttpClientHandler handler;
        private HttpClient http;


        private readonly string authSite = "auth";

        public HttpServer()
        {
            handler = new HttpClientHandler();
            handler.Credentials = new NetworkCredential();
            handler.PreAuthenticate = true;

            http = new HttpClient(handler);
        }

        public Task<bool> Auth(IConnectSettings s, ILoginSettings i)
        {
            //handler.Credentials = new NetworkCredential(i.Credentials.UserName, i.Credentials.Password);
            (handler.Credentials as NetworkCredential).UserName = i.Credentials.UserName;
            (handler.Credentials as NetworkCredential).Password = i.Credentials.Password;

            try
            {
                var u = new UriBuilder(s.Url);
                if (s.Port > 1024)
                {
                    u.Port = s.Port;
                }

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


                    if (json.user == i.Credentials.UserName && json.authenticated)
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
    }
}
