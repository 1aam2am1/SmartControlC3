using System;
using System.Collections.Generic;
using System.Text;

namespace SmartControl.Api
{
    public class DataManager : IConnectSettings
    {

        string _Url;
        public string Url
        {
            get => _Url;
            set => _Url = value;
        }

    }
}
