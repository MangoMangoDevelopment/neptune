namespace UrdfUnity.Urdf.Models
{
    /// <summary>
    /// Represents the fixed axis roll, pitch and yaw angles in radians.
    /// </summary>
    /// <seealso cref="Origin"/>
    /// <seealso cref="XyzAttribute"/>
    public sealed class RpyAttribute
    {
        /// <summary>
        /// The attribute's roll value.
        /// </summary>
        public double R { get; }

        /// <summary>
        /// The attribute's pitch value.
        /// </summary>
        public double P { get; }

        /// <summary>
        /// The attribute's yaw value.
        /// </summary>
        public double Y { get; }


        /// <summary>
        /// Creates a new instance of RpyAttribute, defaulting to identity.
        /// </summary>
        public RpyAttribute() : this(0d, 0d, 0d)
        {
        }

        /// <summary>
        /// Creates a new instance of RpyAttribute with the rpy attribute values specified.
        /// </summary>
        /// <param name="r">The attribute's roll value</param>
        /// <param name="p">The attribute's pitch value</param>
        /// <param name="y">The attribute's yaw value</param>
        public RpyAttribute(double r, double p, double y)
        {
            this.R = r;
            this.P = p;
            this.Y = y;
        }

        protected bool Equals(RpyAttribute other)
        {
            return R.Equals(other.R) && P.Equals(other.P) && Y.Equals(other.Y);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((RpyAttribute)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = R.GetHashCode();
                hashCode = (hashCode * 397) ^ P.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                return hashCode;
            }
        }
    }
}
