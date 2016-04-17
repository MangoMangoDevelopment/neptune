namespace UrdfUnity.Urdf.Models.Attributes
{
    /// <summary>
    /// Represents the scale attribute of a trimesh element.
    /// </summary>
    /// <seealso cref="Mesh"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    public sealed class ScaleAttribute
    {
        /// <summary>
        /// The scale of the mesh object's x-dimension.
        /// </summary>
        public double X { get; }

        /// <summary>
        /// The scale of the mesh object's y-dimension.
        /// </summary>
        public double Y { get; }

        /// <summary>
        /// The scale of the mesh object's z-dimension.
        /// </summary>
        public double Z { get; }


        /// <summary>
        /// Creates a new instance of ScaleAttribute.
        /// </summary>
        /// <param name="x">The x-dimension scale</param>
        /// <param name="y">The y-dimension scale</param>
        /// <param name="z">The z-dimension scale</param>
        public ScaleAttribute(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// Returns the string representation of this object as an XML attribute value.
        /// </summary>
        /// <returns>"X Y Z"</returns>
        public override string ToString()
        {
            return $"{X} {Y} {Z}";
        }

        protected bool Equals(ScaleAttribute other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ScaleAttribute)obj);
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
