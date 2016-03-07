using UrdfUnity.Urdf.Models.LinkElements.VisualElements;
using UrdfUnity.Util;

namespace UrdfUnity.Urdf.Models.LinkElements
{
    /// <summary>
    /// Represents the visual properties of a link, specifying its shape for visualization purposes.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    public class Visual
    {
        /// <summary>
        /// The reference frame of the visual element with respect to the reference frame of the link.
        /// </summary>
        /// <value>Optional. Defaults to identity.</value>
        public Origin Origin { get; }

        /// <summary>
        /// The shape of the visual element.
        /// </summary>
        /// <value>Required.</value>
        public Geometry Geometry { get; }

        /// <summary>
        /// The material of the visual element.
        /// </summary>
        /// <value>Optional. MAY BE NULL.</value>
        public Material Material { get; }


        /// <summary>
        /// Creates a new instance of Visual with the specified geometry.
        /// </summary>
        /// <param name="geometry">The shape of the visual element</param>
        public Visual(Geometry geometry) : this(new Origin(), geometry)
        {
            // Invoke overloaded constructor.
        }

        /// <summary>
        /// Creates a new instance of Visual with the specified origin and geometry.
        /// </summary>
        /// <param name="origin">The reference frame of the visual element</param>
        /// <param name="geometry">The shape of the visual element</param>
        public Visual(Origin origin, Geometry geometry) : this(origin, geometry, null)
        {
            // Invoke overloaded constructor.
        }

        /// <summary>
        /// Creates a new instance of Visual with the specified geometry and material.
        /// </summary>
        /// <param name="geometry">The shape of the visual element</param>
        /// <param name="material">The material of the visual element</param>
        public Visual(Geometry geometry, Material material) : this(new Origin(), geometry, material)
        {
            // Invoke overloaded constructor.
        }

        /// <summary>
        /// Creates a new instance of Visual with the specified origin, geometry and material.
        /// </summary>
        /// <param name="origin">The reference frame of the visual element</param>
        /// <param name="geometry">The shape of the visual element</param>
        /// <param name="material">The material of the visual element</param>
        public Visual(Origin origin, Geometry geometry, Material material)
        {
            Preconditions.IsNotNull(origin);
            Preconditions.IsNotNull(geometry);
            this.Origin = origin;
            this.Geometry = geometry;
            this.Material = material;
        }

        protected bool Equals(Visual other)
        {
            return Origin.Equals(other.Origin) && Geometry.Equals(other.Geometry) 
                && (Material != null ? Material.Equals(other.Material) : other.Material == null);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Visual) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Origin.GetHashCode();
                hashCode = (hashCode*397) ^ Geometry.GetHashCode();
                hashCode = (hashCode*397) ^ (Material != null ? Material.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
