using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using UrdfToUnity.Urdf.Models.Attributes;
using UrdfToUnity.Urdf.Models.Links;
using UrdfToUnity.Urdf.Models.Links.Geometries;
using UrdfToUnity.Util;

namespace UrdfToUnity.Urdf.Models
{
    /// <summary>
    /// Represents a robot model that consists of a set of link and joint elements.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/model"/>
    /// <seealso cref="Link"/>
    /// <seealso cref="Joint"/>
    public sealed class Robot : BaseRobot
    {
        /// <summary>
        /// The default name used when a Robot needs to be instantiated without a name.
        /// </summary>
        public static readonly string DEFAULT_NAME = "missing_name";

        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();


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
            Preconditions.IsNotEmpty(name, "Robot name property cannot be set to null or empty");
            Preconditions.IsNotNull(links, "Robot links property cannot be set to null");
            Preconditions.IsNotNull(joints, "Robot joints property cannot be set to null");
            this.Name = name;
            this.Links = links;
            this.Joints = joints;
        }

        /// <summary>
        /// Adds a new sensor to the Robot model, creating a joint and link object for the component being added.
        /// </summary>
        /// <param name="component">The component object being added. MUST NOT BE NULL</param>
        /// <param name="parent">The name of the parent link that this component is linked to. MUST NOT BE NULL OR EMPTY</param>
        /// <param name="xyz">The XYZ offset of this component from its parent link. MUST NOT BE NULL</param>
        /// <param name="rpy">The RPY offset of this component from its parent link. MUST NOT BE NULL</param>
        /// <returns><c>true</c> if the component was successfully added, otherwise <c>false</c></returns>
        public string AddComponent(Component component, string parent, XyzAttribute xyz, RpyAttribute rpy)
        {
            Preconditions.IsNotNull(component, "Cannot add a null component to the Robot");
            Preconditions.IsNotEmpty(parent, $"Cannot add component '{component.Name}' to the Robot model with missing parent name");
            Preconditions.IsNotNull(xyz, $"Cannot add component '{component.Name}' to '{Name}' Robot model with null XYZ offset");
            Preconditions.IsNotNull(rpy, $"Cannot add component '{component.Name}' to '{Name}' Robot model with null RPY offset");

            if (!this.Links.ContainsKey(parent))
            {
                LOGGER.Warn($"Adding component '{component.Name}' to '{Name}' Robot model failed because '{Name}' doesn't contain link called '{parent}'");
                return null;
            }

            string linkName = GenerateUniqueKey(component.Name, new List<string>(this.Links.Keys));
            string jointName = GenerateUniqueKey($"{component.Name}_joint", new List<string>(this.Joints.Keys));

            Visual visual = new Visual.Builder(new Geometry(new Mesh.Builder(component.FileName).Build())).Build();
            Link link = new Link.Builder(linkName).SetVisual(visual).Build();
            Joint joint = new Joint.Builder(jointName, Joint.JointType.Fixed, this.Links[parent], link).Build();

            this.Links.Add(link.Name, link);
            this.Joints.Add(joint.Name, joint);

            return linkName;
        }

        /// <summary>
        /// Adds a new sensor to the Robot model, creating a joint object for the component being added.
        /// </summary>
        /// <param name="component">The Robot model object being added. MUST NOT BE NULL</param>
        /// <param name="parent">The name of the parent link on this Robot that this component is linked to. MUST NOT BE NULL OR EMPTY</param>
        /// <param name="child">The name of the child link on the component Robot that this component is linked by. MUST NOT BE NULL OR EMPTY</param>
        /// <param name="xyz">The XYZ offset of this component from its parent link. MUST NOT BE NULL</param>
        /// <param name="rpy">The RPY offset of this component from its parent link. MUST NOT BE NULL</param>
        /// <returns>The name of the connected Link object if the component was successfully added, otherwise <c>null</c></returns>
        public string AddComponent(Robot component, string parent, string child, XyzAttribute xyz, RpyAttribute rpy)
        {
            Preconditions.IsNotNull(component, "Cannot add a null component to the Robot");
            Preconditions.IsNotEmpty(parent, $"Cannot add component '{component.Name}' to the Robot model with missing parent name");
            Preconditions.IsNotEmpty(child, $"Cannot add component '{component.Name}' to the Robot model with missing child name");
            Preconditions.IsNotNull(xyz, $"Cannot add component '{component.Name}' to '{Name}' Robot model with null XYZ offset");
            Preconditions.IsNotNull(rpy, $"Cannot add component '{component.Name}' to '{Name}' Robot model with null RPY offset");

            if (!this.Links.ContainsKey(parent))
            {
                LOGGER.Warn($"Adding component '{component.Name}' to '{Name}' Robot model failed because '{Name}' doesn't contain link called '{parent}'");
                return null;
            }

            if (!component.Links.ContainsKey(child))
            {
                LOGGER.Warn($"Adding component '{component.Name}' to '{Name}' Robot model failed because '{component.Name}' doesn't contain link called '{child}'");
                return null;
            }

            string jointName = GenerateUniqueKey($"{component.Name}_joint", new List<string>(this.Joints.Keys));
            Joint newJoint = new Joint.Builder(jointName, Joint.JointType.Fixed, this.Links[parent], component.Links[child]).Build();
            
            this.Joints.Add(newJoint.Name, newJoint);

            foreach (Link link in component.Links.Values)
            {
                this.Links.Add(link.Name, link);
            }
            foreach (Joint joint in component.Joints.Values)
            {
                this.Joints.Add(joint.Name, joint);
            }

            return child;
        }

        /// <summary>
        /// Helper method to generate a unique name used as a key for a link or joint in the case that a 
        /// component is being added and there's already a link or joint with its name. A number will be 
        /// appended to the name if it is not unique.
        /// </summary>
        /// <param name="name">The name being checked for uniqueness</param>
        /// <param name="existingKeys">A list of keys being checked against for uniqueness</param>
        /// <returns>The provided name if unique, otherwise a unique name of the format "name_#" with an number appended</returns>
        private string GenerateUniqueKey(string name, List<string> existingKeys)
        {
            if (!existingKeys.Contains(name))
            {
                return name;
            }

            string newName = String.Empty;
            int i = 1;

            do
            {
                newName = $"{name}_{i++}";
            }
            while (existingKeys.Contains(newName));

            return newName;
        }

        /// <summary>
        /// Returns the URDF XML string representation of this model object.
        /// </summary>
        /// <returns>The URDF XML string representation of this model object</returns>
        public override string ToString()
        {
            XmlStringBuilder sb = new XmlStringBuilder(UrdfSchema.ROBOT_ELEMENT_NAME)
                .AddAttribute(UrdfSchema.NAME_ATTRIBUTE_NAME, this.Name);

            foreach (Link link in this.Links.Values)
            {
                sb.AddSubElement(link.ToString());
            }

            foreach (Joint joint in this.Joints.Values)
            {
                sb.AddSubElement(joint.ToString());
            }

            return sb.ToString();
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
