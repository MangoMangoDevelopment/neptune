using System.Collections.Generic;
using System.Linq;
using UrdfUnity.Util;

namespace UrdfUnity.Urdf.Models
{
    /// <summary>
    /// Represents a robot model that consists of a set of link and joint elements.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/model"/>
    /// <seealso cref="Link"/>
    /// <seealso cref="Joint"/>
    public class Robot
    {
        /// <summary>
        /// The default name used when a Robot needs to be instantiated without a name.
        /// </summary>
        public static readonly string DEFAULT_NAME = "missing_name";


        /// <summary>
        /// The name of the robot model.
        /// </summary>
        /// <value>Required.</value>
        public string Name { get; }

        /// <summary>
        /// The robot's links that are connected by its links stored as key/value pairs with the
        /// link's name as the key and the link object as the value.
        /// </summary>
        /// <value>Required.</value>
        public Dictionary<string, Link> Links { get; }

        /// <summary>
        /// The robot's joints that connect its links together stored as key/value pairs with the
        /// joint's name as the key and the joint object as the value.
        /// </summary>
        /// <value>Required.</value>
        public Dictionary<string, Joint> Joints { get; }


        /// <summary>
        /// Creates a new instance of Robot with empty lists of Links and Joints.
        /// </summary>
        /// <param name="name">The name of the robot model</param>
        public Robot(string name) : this(name, new Dictionary<string, Link>(), new Dictionary<string, Joint>())
        {
            // Invoke overloaded constructor.
        }

        /// <summary>
        /// Creates a new instance of Robot with the specified links and joints.
        /// </summary>
        /// <param name="name">The name of the robot model</param>
        /// <param name="links">A list of the robot model's Links</param>
        /// <param name="joints">A list of the robot model's Joints</param>
        public Robot(string name, Dictionary<string, Link> links, Dictionary<string, Joint> joints)
        {
            Preconditions.IsNotEmpty(name, "name");
            Preconditions.IsNotNull(links, "links");
            Preconditions.IsNotNull(joints, "joints");
            this.Name = name;
            this.Links = links;
            this.Joints = joints;
        }

        protected bool Equals(Robot other)
        {
            return string.Equals(Name, other.Name) && Links.SequenceEqual(other.Links) && Joints.SequenceEqual(other.Joints);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Robot)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                foreach (var link in Links)
                {
                    hashCode = (hashCode * 397) ^ link.GetHashCode();
                }
                foreach (var joint in Joints)
                {
                    hashCode = (hashCode * 397) ^ joint.GetHashCode();
                }
                return hashCode;
            }
        }
    }
}
