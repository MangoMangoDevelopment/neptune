namespace UrdfUnity.Urdf.Models
{
    /// <summary>
    /// Represents the x, y, z offset.
    /// </summary>
    /// <seealso cref="Origin"/>
    /// <seealso cref="RpyAttribute"/>
    /// <
    public class XyzAttribute
    {
        /// <summary>
        /// The attribute's X-axis offset value.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// The attribute's Y-axis offset value.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// The attribute's Z-axis offset value.
        /// </summary>
        public double Z { get; set; }


        /// <summary>
        /// Creates a new instance of XyzAttribute, defaulting to a zero-vector.
        /// </summary>
        public XyzAttribute() : this(0, 0, 0)
        {
        }

        /// <summary>
        /// Creates a new instance of XyzAttribute, per the XYZ values specified.
        /// </summary>
        /// <param name="x">The attribute's X-value</param>
        /// <param name="y">The attribute's Y-value</param>
        /// <param name="z">The attribute's Z-value</param>
        public XyzAttribute(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
    }
}
