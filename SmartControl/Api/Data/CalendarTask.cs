using System;
using System.Collections.Generic;
using System.Text;

namespace SmartControl.Api.Data
{
    public class CalendarTask
    {

        /// <summary>
        /// Is this task enabled
        /// </summary>
        public bool Enabled { set; get; }
        public int Hour { set; get; }
        public int Minute { set; get; }
        public int Duration { set; get; }


        public int ExhaustPower { set; get; }
        public int AirflowPower { set; get; }

        /// <summary>
        /// Is Heater on.
        /// AirTemperature is temperature it warms to.
        /// </summary>
        public bool Heater { set; get; }
        public int AirTemperature { set; get; }

        public bool Boost { set; get; }
    }
}
