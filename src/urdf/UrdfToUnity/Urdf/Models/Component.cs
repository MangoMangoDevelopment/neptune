using UrdfToUnity.Urdf.Models.Links.Geometries;
using UrdfToUnity.Util;

namespace UrdfToUnity.Urdf.Models
{
    /// <summary>
    /// Represents an additional component, typically a robotic sensor, that can be added to a
    /// Robot model object.
    /// </summary>
    /// <remarks>
    /// The component's file name property points to the mesh file used to visually render the component.
    /// The Component model object is used internally and does not have a URDF counterpart.
    /// </remarks>
    /// <seealso cref="BaseRobot"/>
    public class Component
    {
        /// <summary>
        /// The name of the component.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The file name of the component's mesh used to visually render the object.
        /// </summary>
        /// <value><c>null</c> if this Component was constructed with a Box</value>
        public string FileName { get; }

        /// <summary>
        /// The box object representing the component's length, width and height used to visually render the object.
        /// </summary>
        /// <value><c>null</c> if this Component was constructed with a file name</value>
        public Box Box { get; }


        /// <summary>
        /// Creates a new instance of Component, where the component is a mesh.
        /// </summary>
        /// <param name="name">The name of the component</param>
        /// <param name="fileName">The mesh file for the component</param>
        public Component(string name, string fileName)
        {
            Preconditions.IsNotEmpty(name, "Component name property cannot be set to null or empty");
            Preconditions.IsNotEmpty(fileName, "Component fileNath property cannot be set to null or empty");
            this.Name = name;
            this.FileName = fileName;
        }

        /// <summary>
        /// Creates a new instance of Component, where the component is a box.
        /// </summary>
        /// <param name="name">The name of the component</param>
        /// <param name="box">The box object for the component</param>
        public Component(string name, Box box)
        {
            Preconditions.IsNotEmpty(name, "Component name property cannot be set to null or empty");
            Preconditions.IsNotNull(box, "Component box property cannot be set to null or empty");
            this.Name = name;
            this.Box = box;
        }

        protected bool Equals(Component other)
        {
            return Name.Equals(other.Name) && Equals(FileName, other.FileName) && Equals(Box, other.Box);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Component)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name.GetHashCode();
                hashCode = (hashCode * 397) ^ (FileName != null ? FileName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Box != null ? Box.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
