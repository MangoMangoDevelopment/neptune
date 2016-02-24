using System.Collections.Generic;
using UrdfUnity.Util;

namespace UrdfUnity.Urdf.Models
{
    /// <summary>
    /// Represents a robot model that consists of a set of link and joint elements.
    /// </summary>
    public class Robot
    {
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
        public Robot() : this(new List<Link>(), new List<Joint>())
        {
            // Invoke overloaded constructor.
        }

        /// <summary>
        /// Creates a new instance of Robot with the specified links and joints.
        /// </summary>
        /// <param name="links">A list of the robot model's Links</param>
        /// <param name="joints">A list of the robot model's Joints</param>
        public Robot(List<Link> links, List<Joint> joints)
        {
            Preconditions.IsNotNull(links);
            Preconditions.IsNotNull(joints);
            this.Links = links;
            this.Joints = joints;
        }
    }
}
