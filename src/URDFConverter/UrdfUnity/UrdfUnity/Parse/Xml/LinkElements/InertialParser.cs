using System;
using System.Xml;
using UrdfUnity.Urdf.Models.LinkElements;

namespace UrdfUnity.Parse.Xml.LinkElements
{
    /// <summary>
    /// Parses a URDF &lt;inertial&gt; element from XML into a Inertial object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="Urdf.Models.LinkElements.Inertial"/>
    class InertialParser : XmlParser<Inertial>
    {
        /// <summary>
        /// Parses a URDF &lt;inertial&gt; element from XML.
        /// </summary>
        /// <param name="inertialNode">The XML node of a &lt;inertial&gt; element</param>
        /// <returns>A Inertial object parsed from the XML</returns>
        public Inertial Parse(XmlNode inertialNode)
        {
            // TODO: Implement...!
            throw new NotImplementedException();
        }
    }
}
