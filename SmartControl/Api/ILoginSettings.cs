using System;
using System.Collections.Generic;
using System.Text;

namespace SmartControl.Api
{
    public interface ILoginSettings
    {
        public Credentials Credentials { get; set; }
    }
}
