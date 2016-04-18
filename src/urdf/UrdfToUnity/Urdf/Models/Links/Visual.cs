using UrdfToUnity.Urdf.Models.Links.Visuals;
using UrdfToUnity.Util;

namespace UrdfToUnity.Urdf.Models.Links
{
    /// <summary>
    /// Represents the visual properties of a link, specifying its shape for visualization purposes.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    public sealed class Visual
    {
        /// <summary>
        /// The name identifier of the visual's geometry.
        /// </summary>
        /// <value>Optional. MAY BE NULL.</value>
        public string Name { get; }

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
        /// Creates a new instance of Visual with the specified origin, geometry and material.
        /// A Visual.Builder must be used to create a visual to enforce required and default properties.
        /// </summary>
        /// <param name="name">The identifier name of the visual element's geometry</param>
        /// <param name="origin">The reference frame of the visual element. MUST NOT BE NULL</param>
        /// <param name="geometry">The shape of the visual element. MUST NOT BE NULL</param>
        /// <param name="material">The material of the visual element</param>
        private Visual(string name, Origin origin, Geometry geometry, Material material)
        {
            Preconditions.IsNotNull(origin, "Visual origin property must not be null");
            Preconditions.IsNotNull(geometry, "Visual geometry property must not be null");
            this.Name = name;
            this.Origin = origin;
            this.Geometry = geometry;
            this.Material = material;
        }


        /// <summary>
        /// Helper class to build a new instance of visual with its defined properties.
        /// </summary>
        public class Builder
        {
            private string name = null;
            private Origin origin = Origin.DEFAULT_ORIGIN;
            private Geometry geometry;
            private Material material = null;

            /// <summary>
            /// Creates a new instance of Builder.
            /// </summary>
            /// <param name="geometry">The shape of the visual element</param>
            public Builder(Geometry geometry)
            {
                Preconditions.IsNotNull(geometry, "Visual geometry property cannot be set to null");
                this.geometry = geometry;
            }

            /// <summary>
            /// Creates a new instance of Visual with the specified properties.
            /// </summary>
            /// <returns>A Visual object with the properties set</returns>
            public Visual Build()
            {
                return new Visual(this.name, this.origin, this.geometry, this.material);
            }

            public Builder SetName(string name)
            {
                Preconditions.IsNotEmpty(name, "Visual name property cannot be set to null or empty");
                this.name = name;
                return this;
            }

            public Builder SetOrigin(Origin origin)
            {
                Preconditions.IsNotNull(origin, "Visual origin property cannot be set to null");
                this.origin = origin;
                return this;
            }

            public Builder SetMaterial(Material material)
            {
                Preconditions.IsNotNull(material, "Visual material property cannot be set to null");
                this.material = material;
                return this;
            }
        }


        /// <summary>
        /// Returns the URDF XML string representation of this model object.
        /// </summary>
        /// <returns>The URDF XML string representation of this model object</returns>
        public override string ToString()
        {
            XmlStringBuilder sb = new XmlStringBuilder(UrdfSchema.VISUAL_ELEMENT_NAME)
                .AddSubElement(this.Geometry.ToString());

            if (this.Name != null)
            {
                sb.AddAttribute(UrdfSchema.NAME_ATTRIBUTE_NAME, this.Name);
            }

            if (!this.Origin.Equals(Origin.DEFAULT_ORIGIN))
            {
                sb.AddSubElement(this.Origin.ToString());
            }

            if (this.Material != null)
            {
                sb.AddSubElement(this.Material.ToString());
            }

            return sb.ToString();
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
            return Equals((Visual)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Origin.GetHashCode();
                hashCode = (hashCode * 397) ^ Geometry.GetHashCode();
                hashCode = (hashCode * 397) ^ (Material != null ? Material.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
