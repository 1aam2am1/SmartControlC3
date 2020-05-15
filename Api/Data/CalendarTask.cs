using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Data
{
    public struct CalendarTask
    {

        /// <summary>
        /// Is this task enabled
        /// </summary>
        public bool Enabled { set; get; }
        public int Hour { set; get; }
        public int Minute { set; get; }

        /// <summary>
        /// Number of minutes
        /// </summary>
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



        public static bool operator ==(CalendarTask lhs, CalendarTask rhs)
        {
            return lhs.Enabled == rhs.Enabled
                   && lhs.Hour == rhs.Hour
                   && lhs.Minute == rhs.Minute
                   && lhs.Duration == rhs.Duration
                   && lhs.ExhaustPower == rhs.ExhaustPower
                   && lhs.AirflowPower == rhs.AirflowPower
                   && lhs.Heater == rhs.Heater
                   && lhs.AirTemperature == rhs.AirTemperature
                   && lhs.Boost == rhs.Boost;
        }

        public static bool operator !=(CalendarTask lhs, CalendarTask rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                return this == (CalendarTask)obj;
            }
        }

        public override int GetHashCode()
        {
            return ((base.GetHashCode() ^ Hour ^ Minute ^ Duration ^ ExhaustPower ^ AirflowPower ^ AirTemperature) << 3)
                + (Enabled ? 1 : 0) + (Heater ? 2 : 0) + (Boost ? 4 : 0);
        }
    }
}
