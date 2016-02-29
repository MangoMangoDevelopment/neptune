using System;
using System.Xml;
using UrdfUnity.Urdf.Models.LinkElements.GeometryElements;

namespace UrdfUnity.Parse.Xml.LinkElements.GeometryElements
{
    /// <summary>
    /// Parses a URDF &lt;box&gt; element from XML into a Box object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    /// <seealso cref="Urdf.Models.LinkElements.GeometryElements.Box"/>
    class BoxParser : XmlParser<Box>
    {
        /// <summary>
        /// Parses a URDF &lt;box&gt; element from XML.
        /// </summary>
        /// <param name="boxNode">The XML node of a &lt;box&gt; element</param>
        /// <returns>A Box object parsed from the XML</returns>
        public Box Parse(XmlNode boxNode)
        {
            // TODO: Implement...!
            throw new NotImplementedException();
        }
    }
}
