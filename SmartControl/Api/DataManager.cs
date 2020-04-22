using System;
using System.Collections.Generic;
using System.Text;

namespace SmartControl.Api
{
    public class DataManager : IConnectSettings, ILoginSettings
    {

        string _Url;
        public string Url
        {
            get => _Url;
            set => _Url = value;
        }

        int _Port;
        public int Port
        {
            get => _Port;
            set => _Port = value;
        }

        Credentials _credentials = new Credentials();
        public Credentials Credentials
        {
            get => _credentials;
            set => _credentials = value;
        }
    }
}
