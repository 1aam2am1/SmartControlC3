using System;
using System.Collections.Generic;
using System.Text;

namespace SmartControl.Api.Server.Responses
{
    public class ModesResponse
    {
        public class P
        {
            /// <summary>
            /// Is active
            /// </summary>
            public bool I { set; get; }
            /// <summary>
            /// Value 
            /// </summary>
            public int V { set; get; }
        }

        /// <summary>
        /// Modes of work
        /// 1. Boost
        /// 2. Airing
        /// 3. Sleep
        /// 4. Holiday
        /// 5. Max Vent
        /// </summary>
        public List<P> Modes { set; get; }
    }
}
