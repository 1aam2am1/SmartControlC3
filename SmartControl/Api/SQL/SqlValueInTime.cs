using Api.Data;
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
                SqlValueInTimeId = DateTimeOffset.FromUnixTimeSeconds(v.Time).UtcDateTime,
                Value = v.Value
            };
        }

        public static implicit operator ValueInTime(SqlValueInTime v)
        {
            DateTimeOffset localTime = DateTime.SpecifyKind(v.SqlValueInTimeId, DateTimeKind.Utc);

            return new ValueInTime
            {
                Time = localTime.ToUnixTimeSeconds(),
                Value = v.Value
            };
        }
    }
}
