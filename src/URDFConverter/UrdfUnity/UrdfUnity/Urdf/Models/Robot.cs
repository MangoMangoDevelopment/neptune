using System.Collections.Generic;
using UrdfUnity.Util;

namespace UrdfUnity.Urdf.Models
{
    /// <summary>
    /// Represents a robot model that consists of a set of link and joint elements.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/model"/>
    public class Robot
    {
        /// <summary>
        /// The name of the robot model.
        /// </summary>
        /// <value>Required.</value>
        public string Name { get; }

        /// <summary>
        /// The robot's links that are connected by its joints.
        /// </summary>
        /// <value>Required.</value>
        public List<Link> Links { get; }

        /// <summary>
        /// The robot's joints that connect its links together.
        /// </summary>
        /// <value>Required.</value>
        public List<Joint> Joints { get; }


        /// <summary>
        /// Creates a new instance of Robot with empty lists of Links and Joints.
        /// </summary>
        public Robot(string name) : this(name, new List<Link>(), new List<Joint>())
        {
            // Invoke overloaded constructor.
        }

        /// <summary>
        /// Creates a new instance of Robot with the specified links and joints.
        /// </summary>
        /// <param name="links">A list of the robot model's Links</param>
        /// <param name="joints">A list of the robot model's Joints</param>
        public Robot(string name, List<Link> links, List<Joint> joints)
        {
            Preconditions.IsNotEmpty(name, "name");
            Preconditions.IsNotNull(links, "links");
            Preconditions.IsNotNull(joints, "joints");
            this.Name = name;
            this.Links = links;
            this.Joints = joints;
        }
    }
}
