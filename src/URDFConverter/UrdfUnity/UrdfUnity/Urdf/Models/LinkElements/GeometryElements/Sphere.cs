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
    }
}
