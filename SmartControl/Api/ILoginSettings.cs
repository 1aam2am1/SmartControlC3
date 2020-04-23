using System;
using System.Collections.Generic;
using System.Text;

namespace SmartControl.Api
{
    public interface ILoginSettings
    {
        /// <summary>
        /// Credentials to use when connecting to server.
        /// </summary>
        public Credentials Credentials { get; set; }
    }
}
