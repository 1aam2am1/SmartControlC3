using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Data
{
    public struct TimePeriod
    {
        /// <summary>
        /// Period begin
        /// </summary>
        public DateTime Begining { set; get; }

        /// <summary>
        /// Period end
        /// </summary>
        public DateTime End { set; get; }
    }
}
