namespace UrdfUnity.Urdf.Models.LinkElements.GeometryElements
{
    /// <summary>
    /// Represents the cylinder shape of a link's geometry.
    /// </summary>
    public class Cylinder
    {
        /// <summary>
        /// The radius of the cylinder shape.
        /// </summary>
        public double Radius { get; }

        /// <summary>
        /// The length of the cylinder shape.
        /// </summary>
        public double Length { get; }


        /// <summary>
        /// Creates a new instance of Cylinder.
        /// </summary>
        /// <param name="radius">The radius of the cylinder</param>
        /// <param name="length">The length of the cylinder</param>
        public Cylinder(double radius, double length)
        {
            this.Radius = radius;
            this.Length = length;
        }

        protected bool Equals(Cylinder other)
        {
            return Radius.Equals(other.Radius) && Length.Equals(other.Length);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Cylinder) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Radius.GetHashCode()*397) ^ Length.GetHashCode();
            }
        }
    }
}
