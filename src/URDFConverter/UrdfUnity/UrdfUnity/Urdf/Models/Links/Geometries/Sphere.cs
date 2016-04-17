using UrdfUnity.Util;

namespace UrdfUnity.Urdf.Models.Links.Geometries
{
    /// <summary>
    /// Represents the sphere shape of a link's geometry.
    /// </summary>
    public sealed class Sphere
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
            Preconditions.IsTrue(radius > 0, "The radius of a sphere must be greater than 0");
            this.Radius = radius;
        }

        /// <summary>
        /// Returns the URDF XML string representation of this model object.
        /// </summary>
        /// <returns>The URDF XML string representation of this model object</returns>
        public override string ToString()
        {
            return new XmlStringBuilder(UrdfSchema.SPHERE_ELEMENT_NAME)
                .AddAttribute(UrdfSchema.RADIUS_ATTRIBUTE_NAME, this.Radius)
                .ToString();
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
            return Equals((Sphere)obj);
        }

        public override int GetHashCode()
        {
            return Radius.GetHashCode();
        }
    }
}
