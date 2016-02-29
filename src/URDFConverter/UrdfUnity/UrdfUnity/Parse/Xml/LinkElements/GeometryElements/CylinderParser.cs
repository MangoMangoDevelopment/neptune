using System;
using System.Xml;
using UrdfUnity.Urdf.Models.LinkElements.GeometryElements;

namespace UrdfUnity.Parse.Xml.LinkElements.GeometryElements
{
    /// <summary>
    /// Parses a URDF &lt;cylinder&gt; element from XML into a Cylinder object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    /// <seealso cref="Urdf.Models.LinkElements.GeometryElements.Cylinder"/>
    class CylinderParser : XmlParser<Cylinder>
    {
        /// <summary>
        /// Parses a URDF &lt;cylinder&gt; element from XML.
        /// </summary>
        /// <param name="cylinderNode">The XML node of a &lt;cylinder&gt; element</param>
        /// <returns>A Cylinder object parsed from the XML</returns>
        public Cylinder Parse(XmlNode cylinderNode)
        {
            // TODO: Implement...!
            throw new NotImplementedException();
        }
    }
}
