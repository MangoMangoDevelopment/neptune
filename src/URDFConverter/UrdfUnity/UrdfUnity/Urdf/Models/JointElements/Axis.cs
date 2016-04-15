using UrdfUnity.Urdf.Models.Attributes;
using UrdfUnity.Util;

namespace UrdfUnity.Urdf.Models.JointElements
{
    /// <summary>
    /// Represents the joint axis specified in the joint frame of reference. 
    /// </summary>
    /// <remarks>
    /// This is the axis of rotation for revolute joints, the axis of translation for 
    /// prismatic joints, and the surface normal for planar joints.  Fixed and floating 
    /// joints do not have an axis. 
    /// </remarks>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    public sealed class Axis
    {
        /// <summary>
        /// The x, y, z components of the axis vector.
        /// </summary>
        /// <value>Required. The vector should be normalized.</value>
        public XyzAttribute Xyz { get; }


        /// <summary>
        /// Creates a new instance of Axis.
        /// </summary>
        /// <param name="xyz">The x, y, z components of the axis vector. MUST NOT BE NULL</param>
        public Axis(XyzAttribute xyz)
        {
            Preconditions.IsNotNull(xyz, "Axis xyz property must not be null");
            this.Xyz = xyz;
        }

        protected bool Equals(Axis other)
        {
            return Xyz.Equals(other.Xyz);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Axis)obj);
        }

        public override int GetHashCode()
        {
            return Xyz.GetHashCode();
        }
    }
}
