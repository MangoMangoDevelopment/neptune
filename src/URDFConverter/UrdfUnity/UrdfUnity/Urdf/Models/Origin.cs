using UrdfUnity.Util;

namespace UrdfUnity.Urdf.Models
{
    /// <summary>
    /// Base class representing the URDF element's reference frame, as per its &lt;origin&gt; element.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="XyzAttribute"/>
    /// <seealso cref="RpyAttribute"/>
    public class Origin
    {
        /// <summary>
        /// The origin element's x, y, z offset.
        /// </summary>
        /// <value>Optional. Defaults to zero vector.</value>
        public XyzAttribute Xyz { get; }

        /// <summary>
        /// The origin element's fixed axis roll, pitch and yaw.
        /// </summary>
        /// <value>Optional. Defaults to identity.</value>
        public RpyAttribute Rpy { get; }


        /// <summary>
        /// Creates a new instance of Origin with default xyz and rpy attribute values.
        /// </summary>
        public Origin()
        {
            this.Xyz = new XyzAttribute();
            this.Rpy = new RpyAttribute();
        }

        /// <summary>
        /// Creates a new instance of Origin with the specified xyz and rpy attribute values.
        /// An Origin.Builder must be used to instantiate an Origin with optional properties as specified.
        /// </summary>
        /// <param name="xyz">The origin element's x, y, z offset.</param>
        /// <param name="rpy">The origin element's fixed axis roll, pitch and yaw.</param>
        private Origin(XyzAttribute xyz, RpyAttribute rpy)
        {
            this.Xyz = xyz;
            this.Rpy = rpy;
        }


        /// <summary>
        /// Helper class to build a new instance of Origin with the optional properties if specified
        /// and default property values otherwise.
        /// </summary>
        public class Builder
        {
            private XyzAttribute xyz = new XyzAttribute();
            private RpyAttribute rpy = new RpyAttribute();


            /// <summary>
            /// Creates a new instance of Origin with the specified properties and default properties if not specified.
            /// </summary>
            /// <returns>A Origin object with the properties set</returns>
            public Origin Build()
            {
                return new Origin(this.xyz, this.rpy);
            }

            /// <summary>
            /// Sets the Origin's XyzAttribute.
            /// </summary>
            /// <param name="xyz">The origin element's x, y, z offset. MUST NOT BE NULL</param>
            /// <returns>This Origin.Builder instance</returns>
            public Builder SetXyz(XyzAttribute xyz)
            {
                Preconditions.IsNotNull(xyz, "xyz");
                this.xyz = xyz;
                return this;
            }

            /// <summary>
            /// Sets the Origin's RpyAttribute.
            /// </summary>
            /// <param name="rpy">The origin element's fixed axis roll, pitch and yaw. MUST NOT BE NULL</param>
            /// <returns>This Origin.Builder instance</returns>
            public Builder SetRpy(RpyAttribute rpy)
            {
                Preconditions.IsNotNull(rpy, "rpy");
                this.rpy = rpy;
                return this;
            }
        }

        protected bool Equals(Origin other)
        {
            return Xyz.Equals(other.Xyz) && Rpy.Equals(other.Rpy);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Origin)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Xyz.GetHashCode() * 397) ^ Rpy.GetHashCode();
            }
        }
    }
}
