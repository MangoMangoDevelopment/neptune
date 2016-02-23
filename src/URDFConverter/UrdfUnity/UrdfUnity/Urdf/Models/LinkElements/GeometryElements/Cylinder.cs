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
    }
}
