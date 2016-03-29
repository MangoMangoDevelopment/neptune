using UrdfUnity.Util;

namespace UrdfUnity.Parse.Xacro.Models
{
    /// <summary>
    /// Models a Xacro property, storing its name and string value.
    /// </summary>
    /// <remarks>
    /// The value stored by the Xacro property may be injected throughout a URDF file by referencing 
    /// the property name in URDF attribute values.
    /// </remarks>
    /// <seealso cref="http://wiki.ros.org/xacro"/>
    /// <seealso cref="XacroPropertyParser"/>
    public class XacroProperty
    {
        /// <summary>
        /// The name of the Xacro property.
        /// </summary>
        public string Name { get;  }

        /// <summary>
        /// The string value of the Xacro property.
        /// </summary>
        public string Value { get; }


        /// <summary>
        /// Creates a new instance of XacroProperty.
        /// </summary>
        /// <param name="name">The name of the Xacro property. MUST NOT BE NULL OR EMPTY</param>
        /// <param name="value">The string value of the Xacro property. MUST NOT BE NULL OR EMPTY</param>
        public XacroProperty(string name, string value)
        {
            Preconditions.IsNotEmpty(name, "Xacro property must have a name");
            Preconditions.IsNotEmpty(value, "Xacro property must have a value");
            this.Name = name;
            this.Value = value;
        }
    }
}
