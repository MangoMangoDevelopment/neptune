using System;
using System.Xml;
using UrdfUnity.Urdf.Models.LinkElements;

namespace UrdfUnity.Parse.Xml.LinkElements
{
    /// <summary>
    /// Parses a URDF &lt;geometry&gt; element from XML into a Geometry object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    /// <seealso cref="Urdf.Models.LinkElements.Geometry"/>
    class GeometryParser : XmlParser<Geometry>
    {
        /// <summary>
        /// Parses a URDF &lt;geometry&gt; element from XML.
        /// </summary>
        /// <param name="geometryNode">The XML node of a &lt;geometry&gt; element</param>
        /// <returns>A Geometry object parsed from the XML</returns>
        public Geometry Parse(XmlNode geometryNode)
        {
            // TODO: Implement...!
            throw new NotImplementedException();
        }
    }
}
