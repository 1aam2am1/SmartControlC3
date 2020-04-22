using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SmartControl.Api.Server
{
    public class FileServer : IServer
    {
        StreamWriter file;
        public FileServer()
        {
            file = new StreamWriter("FileServer.txt");
        }

        public Task<bool> Auth(IConnectSettings s, ILoginSettings i)
        {
            return Task.Run(() =>
            {
                file.WriteLine("Auth function: {0} {1} {2}", s.Url, i.Credentials.UserName, i.Credentials.Password);
                file.Flush();

                if (i.Credentials.UserName == "user" && i.Credentials.Password == "password")
                {
                    return true;
                }
                return false;
            });
        }
    }
}
