using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Data
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


        public static bool operator ==(ModesStatus lhs, ModesStatus rhs)
        {
            return lhs.Active == rhs.Active && lhs.Value == rhs.Value;
        }

        public static bool operator !=(ModesStatus lhs, ModesStatus rhs)
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
                return this == (ModesStatus)obj;
            }
        }

        public override int GetHashCode()
        {
            return ((base.GetHashCode() ^ Value) << 1) | (Active ? 1 : 0);
        }
    }
}
