namespace UrdfUnity.Urdf.Models
{
    /// <summary>
    /// Abstract base class representing the URDF element's reference frame, as per its &lt;origin&gt; element.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="XyzAttribute"/>
    /// <seealso cref="RpyAttribute"/>
    public abstract class AbstractOrigin
    {
        /// <summary>
        /// The origin element's x, y, z offset.
        /// </summary>
        /// <value>Optional: Defaults to zero vector.</value>
        public XyzAttribute Xyz { get; }

        /// <summary>
        /// The origin element's fixed axis roll, pitch and yaw.
        /// </summary>
        /// <value>Optional: Defaults to identity.</value>
        public RpyAttribute Rpy { get; }


        /// <summary>
        /// Creates a new instance of AbstractOrigin with the specified xyz attribute and default rpy attribute value.
        /// </summary>
        /// <param name="xyz">The origin element's x, y, z offset.</param>
        public AbstractOrigin(XyzAttribute xyz) : this(xyz, new RpyAttribute())
        {
            // Invoke the overloaded constructor.
        }

        /// <summary>
        /// Creates a new instance of AbstractOrigin with the specified rpy attribute and default xyz attribute value.
        /// </summary>
        /// <param name="rpy">The origin element's fixed axis roll, pitch and yaw.</param>
        public AbstractOrigin(RpyAttribute rpy) : this(new XyzAttribute(), rpy)
        {
            // Invoke the overloaded constructor.
        }

        /// <summary>
        /// Creates a new instance of AbstractOrigin with the specified xyz and rpy attribute values.
        /// </summary>
        /// <param name="xyz">The origin element's x, y, z offset.</param>
        /// <param name="rpy">The origin element's fixed axis roll, pitch and yaw.</param>
        public AbstractOrigin(XyzAttribute xyz, RpyAttribute rpy)
        {
            this.Xyz = xyz;
            this.Rpy = rpy;
        }
    }
}
