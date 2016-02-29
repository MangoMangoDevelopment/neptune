using System;
using System.Xml;
using UrdfUnity.Urdf.Models.LinkElements.GeometryElements;

namespace UrdfUnity.Parse.Xml.LinkElements.GeometryElements
{
    /// <summary>
    /// Parses a URDF &lt;sphere&gt; element from XML into a Sphere object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    /// <seealso cref="Urdf.Models.LinkElements.GeometryElements.Sphere"/>
    class SphereParser : XmlParser<Sphere>
    {
        /// <summary>
        /// Parses a URDF &lt;sphere&gt; element from XML.
        /// </summary>
        /// <param name="sphereNode">The XML node of a &lt;sphere&gt; element</param>
        /// <returns>A Sphere object parsed from the XML</returns>
        public Sphere Parse(XmlNode sphereNode)
        {
            // TODO: Implement...!
            throw new NotImplementedException();
        }
    }
}
