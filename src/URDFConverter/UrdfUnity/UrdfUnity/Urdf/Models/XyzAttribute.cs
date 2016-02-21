namespace UrdfUnity.Urdf.Models
{
    /// <summary>
    /// Represents the x, y, z offset.
    /// </summary>
    /// <seealso cref="AbstractOrigin"/>
    /// <seealso cref="RpyAttribute"/>
    /// <
    public class XyzAttribute
    {
        /// <summary>
        /// The attribute's X-axis offset value.
        /// </summary>
        public double X { get; }

        /// <summary>
        /// The attribute's Y-axis offset value.
        /// </summary>
        public double Y { get; }

        /// <summary>
        /// The attribute's Z-axis offset value.
        /// </summary>
        public double Z { get; }


        /// <summary>
        /// Creates a new instance of XyzAttribute, defaulting to a zero-vector.
        /// </summary>
        public XyzAttribute() : this(0d, 0d, 0d)
        {
        }

        /// <summary>
        /// Creates a new instance of XyzAttribute with the xyz attribute values specified.
        /// </summary>
        /// <param name="x">The attribute's X-value</param>
        /// <param name="y">The attribute's Y-value</param>
        /// <param name="z">The attribute's Z-value</param>
        public XyzAttribute(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
    }
}
