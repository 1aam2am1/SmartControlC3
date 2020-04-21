using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartControl.Api
{
    public class Client
    {

        async public void Connect(IConnectSettings s, ILoginSettings i, Action onConnection)
        {
            Task task = Task.Run(() => System.Threading.Thread.Sleep(10000));
            //wait for it to end without blocking the main thread
            await task;

            onConnection?.Invoke();
        }
    }
}
