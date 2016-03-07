using UrdfUnity.Util;

namespace UrdfUnity.Urdf.Models.LinkElements
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
    public class Collision
    {
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
        /// Creates a new instance of Collision with the specified geometry.
        /// </summary>
        /// <param name="geometry">The shape of the collision element. MUST NOT BE NULL</param>
        public Collision(Geometry geometry) : this(new Origin(), geometry)
        {
            // Invoke overloaded constructor.
        }

        /// <summary>
        /// Creates a new instance of Collision with the specified origin and geometry.
        /// </summary>
        /// <param name="origin">The reference frame of the collision element. MUST NOT BE NULL</param>
        /// <param name="geometry">The shape of the collision element. MUST NOT BE NULL</param>
        public Collision(Origin origin, Geometry geometry)
        {
            Preconditions.IsNotNull(origin);
            Preconditions.IsNotNull(geometry);
            this.Origin = origin;
            this.Geometry = geometry;
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
