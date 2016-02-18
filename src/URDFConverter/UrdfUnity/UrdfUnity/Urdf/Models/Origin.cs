namespace UrdfUnity.Urdf.Models
{
    /// <summary>
    /// Abstract base class representing the URDF element's reference frame, as per its &lt;origin&gt; element.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="XyzAttribute"/>
    /// <seealso cref="RpyAttribute"/>
    public abstract class Origin
    {
        /// <summary>
        /// The origin element's x, y, z offset.
        /// </summary>
        public XyzAttribute Xyz { get; set; }

        /// <summary>
        /// The origin element's fixed axis roll, pitch and yaw.
        /// </summary>
        public RpyAttribute Rpy { get; set; }


        public Origin(XyzAttribute xyz, RpyAttribute rpy)
        {
            this.Xyz = xyz;
            this.Rpy = rpy;
        }
    }
}
