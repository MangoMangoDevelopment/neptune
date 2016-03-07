namespace UrdfUnity.Urdf.Models.LinkElements.GeometryElements
{
    /// <summary>
    /// Represents the sphere shape of a link's geometry.
    /// </summary>
    public class Sphere
    {
        /// <summary>
        /// The radius of the sphere shape.
        /// </summary>
        public double Radius { get; }


        /// <summary>
        /// Creates a new instance of Sphere.
        /// </summary>
        /// <param name="radius">The radius of the sphere</param>
        public Sphere(double radius)
        {
            this.Radius = radius;
        }

        protected bool Equals(Sphere other)
        {
            return Radius.Equals(other.Radius);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Sphere) obj);
        }

        public override int GetHashCode()
        {
            return Radius.GetHashCode();
        }
    }
}
