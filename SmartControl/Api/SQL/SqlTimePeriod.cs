﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SmartControl.Api.SQL
{
    public class SqlTimePeriod
    {
        /// <summary>
        /// Id of parameter that this period concerns
        /// </summary>
        [ForeignKey("SqlParameter")]
        public int SqlParameterId { get; set; }

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
