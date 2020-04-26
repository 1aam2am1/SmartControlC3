using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SmartControl.Api.SQL
{
    public class SqlParameter
    {
        public int SqlParameterId { get; set; }

        public List<SqlValueInTime> Values { get; } = new List<SqlValueInTime>();
    }
}
