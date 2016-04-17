using UrdfUnity.Util;

namespace UrdfUnity.Urdf.Models.Links
{
    /// <summary>
    /// Represents the collision properties of a link.
    /// </summary>
    /// <remarks>
    /// The collision properties can be different from the visual properties of a link. 
    /// Multiple instances of collision elements can exist for the same link. The union of the 
    /// geometry they define forms the collision representation of the link. 
    /// </remarks>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    public sealed class Collision
    {
        /// <summary>
        /// The name identifier of the collision's geometry.
        /// </summary>
        /// <value>Optional. May be null</value>
        public string Name { get; }

        /// <summary>
        /// The reference frame of the collision element, relative to the reference frame of the link.
        /// </summary>
        /// <value>Optional. Defaults to identity</value>
        public Origin Origin { get; }

        /// <summary>
        /// The shape of the collision element.
        /// </summary>
        /// <value>Required.</value>
        public Geometry Geometry { get; }


        /// <summary>
        /// Creates a new instance of Collision with the specified origin and geometry.
        /// A Collision.Builder must be used to create a collision to enforce required and default properties.
        /// </summary>
        /// <param name="origin">The reference frame of the collision element. MUST NOT BE NULL</param>
        /// <param name="geometry">The shape of the collision element. MUST NOT BE NULL</param>
        private Collision(string name, Origin origin, Geometry geometry)
        {
            Preconditions.IsNotNull(origin, "Collision origin property must not be null");
            Preconditions.IsNotNull(geometry, "Collision geometry property must not be null");
            this.Name = name;
            this.Origin = origin;
            this.Geometry = geometry;
        }


        /// <summary>
        /// Helper class to build a new instance of collision with its defined properties.
        /// </summary>
        public class Builder
        {
            private string name = null;
            private Origin origin = Origin.DEFAULT_ORIGIN;
            private Geometry geometry = null;

            /// <summary>
            /// Creates a new instance of Builder.
            /// </summary>
            public Builder() : this(null)
            {
                // Invoke overloaded constructor.
            }

            /// <summary>
            /// Creates a new instance of Builder.
            /// </summary>
            /// <param name="name">The name of the collision element</param>
            public Builder(string name)
            {
                this.name = name;
            }

            /// <summary>
            /// Creates a new instance of Collision with the specified properties.
            /// </summary>
            /// <returns>A Collision object with the properties set</returns>
            public Collision Build()
            {
                return new Collision(name, origin, geometry);
            }

            public Builder SetOrigin(Origin origin)
            {
                Preconditions.IsNotNull(origin, "Collision origin property cannot be set to null");
                this.origin = origin;
                return this;
            }

            public Builder SetGeometry(Geometry geometry)
            {
                Preconditions.IsNotNull(geometry, "Collision geometry property cannot be set to null");
                this.geometry = geometry;
                return this;
            }
        }


        /// <summary>
        /// Returns the URDF XML string representation of this model object.
        /// </summary>
        /// <returns>The URDF XML string representation of this model object</returns>
        public override string ToString()
        {
            XmlStringBuilder sb = new XmlStringBuilder(UrdfSchema.COLLISION_ELEMENT_NAME)
                .AddSubElement(this.Geometry.ToString());

            if (this.Name != null)
            {
                sb.AddAttribute(UrdfSchema.NAME_ATTRIBUTE_NAME, this.Name);
            }

            if (!this.Origin.Equals(Origin.DEFAULT_ORIGIN))
            {
                sb.AddSubElement(this.Origin.ToString());
            }
            
            return sb.ToString();
        }

        protected bool Equals(Collision other)
        {
            return Origin.Equals(other.Origin) && Geometry.Equals(other.Geometry);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Collision) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Origin.GetHashCode()*397) ^ Geometry.GetHashCode();
            }
        }
    }
}
