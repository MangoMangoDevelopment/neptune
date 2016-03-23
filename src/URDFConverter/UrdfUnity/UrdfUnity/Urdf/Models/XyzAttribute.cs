namespace UrdfUnity.Urdf.Models
{
    /// <summary>
    /// Represents the x, y, z offset.
    /// </summary>
    /// <seealso cref="Origin"/>
    /// <seealso cref="RpyAttribute"/>
    /// <
    public sealed class XyzAttribute
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

        protected bool Equals(XyzAttribute other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((XyzAttribute)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                return hashCode;
            }
        }
    }
}
