namespace UrdfUnity.Urdf.Models.LinkElements.InertialElements
{
    /// <summary>
    /// Derived class representing the URDF link's inertial reference frame, as per its &lt;origin&gt; element.
    /// </summary>
    /// <remarks>
    /// The pose of the inertial reference frame is relative to the link reference frame
    /// and needs to be at the link's center of gravity.
    /// </remarks>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/inertial"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="XyzAttribute"/>
    /// <seealso cref="RpyAttribute"/>
    public class InertialOrigin : AbstractOrigin
    {
        /// <summary>
        /// Creates a new instance of InertialOrigin with the default xyz and rpy attribute values.
        /// </summary>
        public InertialOrigin() : base()
        {
            // Invoke base constructor.
        }
        
        /// <summary>
        /// Creates a new instance of InertialOrigin with the xyz attribute values specified.
        /// </summary>
        /// <param name="xyz">Position offset of the center of mass of the link with respect to the link origin in the link local reference frame</param>
        public InertialOrigin(XyzAttribute xyz) : base(xyz)
        {
            // Invoke base constructor.
        }

        /// <summary>
        /// Creates a new instance of InertialOrigin with the rpy attribute values specified.
        /// </summary>
        /// <param name="rpy">Roll, pitch and yaw orientation offsets of the inertial frame in local link frame in radians</param>
        public InertialOrigin(RpyAttribute rpy) : base(rpy)
        {
            // Invoke base constructor.
        }

        /// <summary>
        /// Creates a new instance of InertialOrigin with the xyz and rpy attribute values specified.
        /// </summary>
        /// <param name="xyz">Position offset of the center of mass of the link with respect to the link origin in the link local reference frame</param>
        /// <param name="rpy">Roll, pitch and yaw orientation offsets of the inertial frame in local link frame in radians</param>
        public InertialOrigin(XyzAttribute xyz, RpyAttribute rpy) : base (xyz, rpy)
        {
            // Invoke base constructor.
        }
    }
}
