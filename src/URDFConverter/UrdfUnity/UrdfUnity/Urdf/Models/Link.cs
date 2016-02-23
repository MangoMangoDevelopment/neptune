using System.Collections.Generic;
using UrdfUnity.Urdf.Models.LinkElements;
using UrdfUnity.Util;

namespace UrdfUnity.Urdf.Models
{
    /// <summary>
    /// Represents a rigid body with inertia, visual features and collision properties.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    public class Link
    {
        /// <summary>
        /// The name of the link.
        /// </summary>
        /// <value>Required.</value>
        public string Name { get; }

        /// <summary>
        /// The inertial properties of the link.
        /// </summary>
        /// <value>Optional. MAY BE NULL.</value>
        public Inertial Inertial { get; }

        /// <summary>
        /// The visual properties of the link. The union of geometry defined by each list item 
        /// forms the visual representation of the link.
        /// </summary>
        /// <value>Optional. MAY BE NULL.</value>
        public List<Visual> Visual { get; }

        /// <summary>
        /// The collision properties of the link. The union of geometry defined by each list item 
        /// forms the collision representation of the link.
        /// </summary>
        /// <value>Optional. MAY BE NULL.</value>
        public List<Collision> Collision { get; }
        

        /// <summary>
        /// Creates a new instance of Link with the specified properties.
        /// </summary>
        /// <param name="name">The name of the link</param>
        /// <param name="inertial">The inertial properties of the link</param>
        /// <param name="visual">The visual properties of the link</param>
        /// <param name="collision">The collision properties of the link</param>
        public Link(string name, Inertial inertial, List<Visual> visual, List<Collision> collision)
        {
            Preconditions.IsNotEmpty(name);
            this.Name = name;
            this.Inertial = inertial;
            this.Visual = visual;
            this.Collision = collision;
        }
    }
}
