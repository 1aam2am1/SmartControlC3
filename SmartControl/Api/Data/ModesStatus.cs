using System;
using System.Collections.Generic;
using System.Text;

namespace SmartControl.Api.Data
{
    public struct ModesStatus
    {
        /// <summary>
        /// Is active
        /// </summary>
        public bool Active { set; get; }
        /// <summary>
        /// Value 
        /// </summary>
        public int Value { set; get; }
    }
}
