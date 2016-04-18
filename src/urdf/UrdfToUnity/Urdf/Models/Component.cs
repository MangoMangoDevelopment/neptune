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
        public string FileName { get; }


        /// <summary>
        /// Creates a new instance of Component.
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

        protected bool Equals(Component other)
        {
            return Name.Equals(other.Name) && FileName.Equals(other.FileName);
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
                return (Name.GetHashCode() * 397) ^ FileName.GetHashCode();
            }
        }
    }
}
