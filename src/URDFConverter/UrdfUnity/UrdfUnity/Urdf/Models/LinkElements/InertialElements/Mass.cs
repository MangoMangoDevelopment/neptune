﻿using UrdfUnity.Util;

namespace UrdfUnity.Urdf.Models.LinkElements.InertialElements
{
    /// <summary>
    /// Represents the mass of the link in kilograms.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/inertial"/>
    public class Mass
    {
        /// <summary>
        /// The mass value of the link in kilograms.
        /// </summary>
        /// <value>Required. MUST NOT BE NEGATIVE</value>
        public double Value { get; }


        /// <summary>
        /// Creates a new instance of Mass with the value specified.
        /// </summary>
        /// <param name="value">The mass of the link in kilograms. MUST NOT BE NEGATIVE</param>
        public Mass(double value)
        {
            Preconditions.IsTrue(value >= 0, "value");
            this.Value = value;
        }

        protected bool Equals(Mass other)
        {
            return Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Mass) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
