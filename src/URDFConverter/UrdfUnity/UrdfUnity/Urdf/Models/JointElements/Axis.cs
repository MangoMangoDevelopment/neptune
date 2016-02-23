using UrdfUnity.Util;

namespace UrdfUnity.Urdf.Models.JointElements
{
    /// <summary>
    /// Represents the joint axis specified in the joint frame. 
    /// </summary>
    /// <remarks>
    /// This is the axis of rotation for revolute joints, the axis of translation for 
    /// prismatic joints, and the surface normal for planar joints.  Fixed and floating 
    /// joints do not have an axis. 
    /// </remarks>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    public class Axis
    {
        /// <summary>
        /// The x, y, z components of the axis vector.
        /// </summary>
        /// <value>Required. The vector should be normalized.</value>
        public XyzAttribute Xyz { get; }


        /// <summary>
        /// Creates a new instance of Axis.
        /// </summary>
        /// <param name="xyz">The x, y, z components of the axis vecor</param>
        public Axis(XyzAttribute xyz)
        {
            Preconditions.IsNotNull(xyz);
            this.Xyz = xyz;
        }
    }
}
