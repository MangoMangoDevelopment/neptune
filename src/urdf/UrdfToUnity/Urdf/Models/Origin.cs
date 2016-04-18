using UrdfToUnity.Urdf.Models.Attributes;
using UrdfToUnity.Util;

namespace UrdfToUnity.Urdf.Models
{
    /// <summary>
    /// Base class representing the URDF element's reference frame, as per its &lt;origin&gt; element.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="XyzAttribute"/>
    /// <seealso cref="RpyAttribute"/>
    public sealed class Origin
    {
        /// <summary>
        /// Default origin used if an origin is not specified, defaulting to a zero vector.
        /// </summary>
        public static readonly Origin DEFAULT_ORIGIN = new Origin();


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
                Preconditions.IsNotNull(xyz, "Origin xyz property cannot be set to null");
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
                Preconditions.IsNotNull(rpy, "Origin rpy property cannot be set to null");
                this.rpy = rpy;
                return this;
            }
        }


        /// <summary>
        /// Returns the URDF XML string representation of this model object.
        /// </summary>
        /// <returns>The URDF XML string representation of this model object</returns>
        public override string ToString()
        {
            if (this.Equals(Origin.DEFAULT_ORIGIN))
            {
                return string.Empty;
            }

            XmlStringBuilder sb = new XmlStringBuilder(UrdfSchema.ORIGIN_ELEMENT_NAME);

            if (!this.Xyz.Equals(new XyzAttribute()))
            {
                sb.AddAttribute(UrdfSchema.XYZ_ATTRIBUTE_NAME, this.Xyz);
            }

            if (!this.Rpy.Equals(new RpyAttribute()))
            {
                sb.AddAttribute(UrdfSchema.RPY_ATTRIBUTE_NAME, this.Rpy);
            }

            return sb.ToString();
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
