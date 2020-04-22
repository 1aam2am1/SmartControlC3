using SmartControl.Api.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartControl.Api
{
    public class Client
    {
        //public Lazy<IServer> server = new Lazy<IServer>(new HttpServer());
        public Lazy<IServer> server = new Lazy<IServer>(new FileServer());
        async public void Connect(IConnectSettings s, ILoginSettings i, Action<bool> onConnection)
        {
            var task = await server.Value.Auth(s, i);
            //wait for it to end without blocking the main thread

            onConnection?.Invoke(task);
        }
    }
}
