using UrdfUnity.Urdf.Models.Links.Geometries;
using UrdfUnity.Util;

namespace UrdfUnity.Urdf.Models.Links
{
    /// <summary>
    /// Represents the shape of the of a link's visual object or collision properties.
    /// </summary>
    /// <remarks>
    /// The shape of a visual object can be one of a box, cylinder, sphere or mesh.
    /// </remarks>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    public sealed class Geometry
    {
        /// <summary>
        /// Enumerates the possible shapes that the geometry may be.
        /// </summary>
        public enum Shapes { Box, Cylinder, Sphere, Mesh };


        /// <summary>
        /// The type of shape that the geometry element contains.
        /// </summary>
        public Shapes Shape { get; }

        /// <summary>
        /// The box shape's attributes if the geometry of this element is a box.
        /// </summary>
        /// <value>A <c>Box</c> object if <c>this.Shape == Shapes.Box</c>, otherwise <c>null</c></value>
        public Box Box { get; }

        /// <summary>
        /// The cylinder shape's attributes if the geometry of this element is a cylinder.
        /// </summary>
        /// <value>A <c>Cylinder</c> object if <c>this.Shape == Shapes.Cylinder</c>, otherwise <c>null</c></value>
        public Cylinder Cylinder { get; }

        /// <summary>
        /// The sphere shape's attributes if the geometry of this element is a sphere.
        /// </summary>
        /// <value>A <c>Sphere</c> object if <c>this.Shape == Shapes.Sphere</c>, otherwise <c>null</c></value>
        public Sphere Sphere { get; }

        /// <summary>
        /// The mesh shape's attributes if the geometry of this element is a trimesh.
        /// </summary>
        /// <value>A <c>Mesh</c> object if <c>this.Shape == Shapes.Mesh</c>, otherwise <c>null</c></value>
        public Mesh Mesh { get; }


        /// <summary>
        /// Creates a new instance of Geometry that is a box.
        /// </summary>
        /// <param name="box">The box shape's attributes. MUST NOT BE NULL</param>
        public Geometry(Box box)
        {
            Preconditions.IsNotNull(box, "Geometry box property must not be set to null");
            this.Shape = Shapes.Box;
            this.Box = box;
        }

        /// <summary>
        /// Creates a new instance of Geometry that is a cylinder.
        /// </summary>
        /// <param name="box">The cylinder shape's attributes</param>
        public Geometry(Cylinder cylinder)
        {
            Preconditions.IsNotNull(cylinder, "Geometry cylinder property must not be set to null");
            this.Shape = Shapes.Cylinder;
            this.Cylinder = cylinder;
        }

        /// <summary>
        /// Creates a new instance of Geometry that is a sphere.
        /// </summary>
        /// <param name="box">The sphere shape's attributes</param>
        public Geometry(Sphere sphere)
        {
            Preconditions.IsNotNull(sphere, "Geometry sphere property must not be set to null");
            this.Shape = Shapes.Sphere;
            this.Sphere = sphere;
        }

        /// <summary>
        /// Creates a new instance of Geometry that is a mesh.
        /// </summary>
        /// <param name="box">The mesh shape's attributes</param>
        public Geometry(Mesh mesh)
        {
            Preconditions.IsNotNull(mesh, "Geometry mesh property must not be set to null");
            this.Shape = Shapes.Mesh;
            this.Mesh = mesh;
        }

        protected bool Equals(Geometry other)
        {
            return Shape == other.Shape && Equals(Box, other.Box) && Equals(Cylinder, other.Cylinder)
                && Equals(Sphere, other.Sphere) && Equals(Mesh, other.Mesh);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Geometry)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int)Shape;
                hashCode = (hashCode * 397) ^ (Box != null ? Box.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Cylinder != null ? Cylinder.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Sphere != null ? Sphere.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Mesh != null ? Mesh.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
