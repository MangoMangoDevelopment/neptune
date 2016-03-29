using System.Collections.Generic;
using System.Xml;
using UrdfUnity.Util;

namespace UrdfUnity.Parse.Xacro.Models
{
    /// <summary>
    /// Models a Xacro macro, storing its name, parameters and its XML representation via XmlElement.
    /// </summary>
    /// <remarks>
    /// The result of the Xacro macro per specified arguments may be injected throughout a URDF file 
    /// by referencing it as an XML element by its name with the Xacro namespace prefix.
    /// </remarks>
    /// <seealso cref="http://wiki.ros.org/xacro"/>
    /// <seealso cref="XacroMacroParser"/>
    public class XacroMacro
    {
        /// <summary>
        /// The name of the Xacro macro.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The names of parameters that may be passed to the macro.
        /// </summary>
        public List<string> Parameters { get; }

        /// <summary>
        /// The original XML definition of the macro.
        /// </summary>
        public XmlElement Xml { get; }


        /// <summary>
        /// Creates a new instance of XacroMacro.
        /// </summary>
        /// <param name="name">The name of the Xacro macro. MUST NOT BE NULL OR EMPTY</param>
        /// <param name="parameters">The names of parameters that may be passed to the macro. MUST NOT BE NULL</param>
        /// <param name="xml">The original XML definition of the macro. MUST NOT BE NULL</param>
        public XacroMacro(string name, List<string> parameters, XmlElement xml)
        {
            Preconditions.IsNotEmpty(name, "Xacro macro must have a name");
            Preconditions.IsNotNull(parameters, "Xacro macro parameter list must not be null");
            Preconditions.IsNotNull(xml, "Xacro macro XML element must not be null");
            this.Name = name;
            this.Parameters = parameters;
            this.Xml = xml;
        }
    }
}
