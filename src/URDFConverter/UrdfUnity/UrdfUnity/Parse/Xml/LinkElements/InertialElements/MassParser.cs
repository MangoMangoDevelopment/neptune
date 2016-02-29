using System;
using System.Xml;
using UrdfUnity.Urdf.Models.LinkElements.InertialElements;

namespace UrdfUnity.Parse.Xml.LinkElements.InertialElements
{
    /// <summary>
    /// Parses a URDF &lt;mass&gt; element from XML into a Mass object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="Urdf.Models.LinkElements.InertialElements.Mass"/>
    class MassParser : XmlParser<Mass>
    {
        /// <summary>
        /// Parses a URDF &lt;mass&gt; element from XML.
        /// </summary>
        /// <param name="massNode">The XML node of a &lt;mass&gt; element</param>
        /// <returns>A Mass object parsed from the XML</returns>
        public Mass Parse(XmlNode massNode)
        {
            // TODO: Implement...!
            throw new NotImplementedException();
        }
    }
}
