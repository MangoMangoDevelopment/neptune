namespace UrdfUnity.Urdf.Models
{
    /// <summary>
    /// Represents the fixed axis roll, pitch and yaw angles in radians.
    /// </summary>
    /// <seealso cref="Origin"/>
    /// <seealso cref="XyzAttribute"/>
    public class RpyAttribute
    {
        /// <summary>
        /// The attribute's roll value.
        /// </summary>
        public double R { get; set; }

        /// <summary>
        /// The attribute's pitch value.
        /// </summary>
        public double P { get; set; }

        /// <summary>
        /// The attribute's yaw value.
        /// </summary>
        public double Y { get; set; }


        /// <summary>
        /// Creates a new instance of RpyAttribute, defaulting to identity.
        /// </summary>
        public RpyAttribute() : this(0, 0, 0)
        {
        }

        /// <summary>
        /// Creates a new instance of RpyAttribute, per the RPY values specified.
        /// </summary>
        /// <param name="r">The attribute's roll value</param>
        /// <param name="p">The attribute's pitch value</param>
        /// <param name="y">The attribute's yaw value</param>
        public RpyAttribute(int r, int p, int y)
        {
            this.R = r;
            this.P = p;
            this.Y = y;
        }
    }
}
