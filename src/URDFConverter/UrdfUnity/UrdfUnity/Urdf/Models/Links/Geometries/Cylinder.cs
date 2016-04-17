using UrdfUnity.Util;

namespace UrdfUnity.Urdf.Models.Links.Geometries
{
    /// <summary>
    /// Represents the cylinder shape of a link's geometry.
    /// </summary>
    public sealed class Cylinder
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
            Preconditions.IsTrue(radius > 0, "The radius of a cylinder must be greater than 0");
            Preconditions.IsTrue(length > 0, "The length of a cylinder must be greater than 0");
            this.Radius = radius;
            this.Length = length;
        }

        /// <summary>
        /// Returns the URDF XML string representation of this model object.
        /// </summary>
        /// <returns>The URDF XML string representation of this model object</returns>
        public override string ToString()
        {
            return new XmlStringBuilder(UrdfSchema.CYLINDER_ELEMENT_NAME)
                .AddAttribute(UrdfSchema.RADIUS_ATTRIBUTE_NAME, this.Radius)
                .AddAttribute(UrdfSchema.LENGTH_ATTRIBUTE_NAME, this.Length)
                .ToString();
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
            return Equals((Cylinder)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Radius.GetHashCode() * 397) ^ Length.GetHashCode();
            }
        }
    }
}
