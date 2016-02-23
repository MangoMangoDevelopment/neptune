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
        public Origin Origin { get; }

        /// <summary>
        /// The shape of the collision element.
        /// </summary>
        public Geometry Geometry { get; }


        /// <summary>
        /// Creates a new instance of Collision with the specified geometry.
        /// </summary>
        /// <param name="geometry">The shape of the collision element</param>
        public Collision(Geometry geometry) : this(new Origin(), geometry)
        {
            // Invoke overloaded constructor.
        }

        /// <summary>
        /// Creates a new instance of Collision with the specified origin and geometry.
        /// </summary>
        /// <param name="origin">The reference frame of the collision element</param>
        /// <param name="geometry">The shape of the collision element</param>
        public Collision(Origin origin, Geometry geometry)
        {
            Preconditions.IsNotNull(origin);
            Preconditions.IsNotNull(geometry);
            this.Origin = origin;
            this.Geometry = geometry;
        }
    }
}
