using SmartControl.Api.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartControl.Api.SQL
{
    public class SqlValueInTime
    {
        public DateTime SqlValueInTimeId { set; get; }

        public int Value { set; get; }

        public int SqlParameterId { get; set; }

        public static implicit operator SqlValueInTime(ValueInTime v)
        {
            return new SqlValueInTime
            {
                SqlValueInTimeId = DateTime.UnixEpoch.AddSeconds(v.Time),
                Value = v.Value
            };
        }
    }
}
