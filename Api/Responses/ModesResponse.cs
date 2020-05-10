using Api.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Responses
{
    public class ModesResponse
    {
        /// <summary>
        /// Modes of work
        /// 1. Boost
        /// 2. Airing
        /// 3. Sleep
        /// 4. Holiday
        /// 5. Max Vent
        /// </summary>
        public Dictionary<int, ModesStatus> Modes { set; get; } = new Dictionary<int, ModesStatus>();
    }
}
