namespace UrdfToUnity.Urdf.Models.Attributes
{
    /// <summary>
    /// Represents the three side lengths of a box shape.
    /// </summary>
    /// <seealso cref="Box"/>
    public sealed class SizeAttribute
    {
        /// <summary>
        /// The size of the length side the box.
        /// </summary>
        public double Length { get; }

        /// <summary>
        /// The size of the width side of the box.
        /// </summary>
        public double Width { get; }

        /// <summary>
        /// The size of the height side of the box.
        /// </summary>
        public double Height { get; }


        /// <summary>
        /// Creates a new instance of SizeAttribute.
        /// </summary>
        /// <param name="length">The attribute's length size</param>
        /// <param name="width">The attribute's width size</param>
        /// <param name="height">The attribute's height size</param>
        public SizeAttribute(double length, double width, double height)
        {
            this.Length = length;
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// Returns the string representation of this object as an XML attribute value.
        /// </summary>
        /// <returns>"L W H"</returns>
        public override string ToString()
        {
            return $"{Length} {Width} {Height}";
        }

        protected bool Equals(SizeAttribute other)
        {
            return Length.Equals(other.Length) && Width.Equals(other.Width) && Height.Equals(other.Height);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SizeAttribute)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Length.GetHashCode();
                hashCode = (hashCode * 397) ^ Width.GetHashCode();
                hashCode = (hashCode * 397) ^ Height.GetHashCode();
                return hashCode;
            }
        }
    }
}
