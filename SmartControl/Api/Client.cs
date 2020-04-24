using SmartControl.Api.Data;
using SmartControl.Api.Server;
using System;
using System.Collections.Generic;
using System.Text;
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

        /// <summary>
        /// Connect to server with given settings
        /// </summary>
        /// <param name="s"></param>
        /// <param name="i"></param>
        /// <param name="onConnection">1 when OK 0 if error</param>
        async public void Connect(ConnectSettings s, Credentials i, Action<bool> onConnection)
        {
            var task = await server.Value.Auth(s, i);
            //wait for it to end without blocking the main thread

            onConnection?.Invoke(task);
        }
    }
}
