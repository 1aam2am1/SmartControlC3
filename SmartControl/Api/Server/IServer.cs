using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartControl.Api.Server
{
    public interface IServer
    {
        public Task<bool> Auth(IConnectSettings s, ILoginSettings i);
    }
}
