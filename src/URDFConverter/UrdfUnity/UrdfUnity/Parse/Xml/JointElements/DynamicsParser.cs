using System;
using System.Xml;
using UrdfUnity.Urdf.Models.JointElements;

namespace UrdfUnity.Parse.Xml.JointElements
{
    /// <summary>
    /// Parses a URDF &lt;dynamics&gt; element from XML into a Dynamics object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="Urdf.Models.JointElements.Dynamics"/>
    class DynamicsParser : XmlParser<Dynamics>
    {
        /// <summary>
        /// Parses a URDF &lt;dynamics&gt; element from XML.
        /// </summary>
        /// <param name="dynamicsNode">The XML node of a &lt;dynamics&gt; element</param>
        /// <returns>An Dynamics object parsed from the XML</returns>
        public Dynamics Parse(XmlNode dynamicsNode)
        {
            // TODO: Implement...!
            throw new NotImplementedException();
        }
    }
}
