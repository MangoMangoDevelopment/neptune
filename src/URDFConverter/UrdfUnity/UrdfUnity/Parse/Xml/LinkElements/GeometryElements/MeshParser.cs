using System;
using System.Xml;
using UrdfUnity.Urdf.Models.LinkElements.GeometryElements;

namespace UrdfUnity.Parse.Xml.LinkElements.GeometryElements
{
    /// <summary>
    /// Parses a URDF &lt;mesh&gt; element from XML into a Mesh object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    /// <seealso cref="Urdf.Models.LinkElements.GeometryElements.Mesh"/>
    class MeshParser : XmlParser<Mesh>
    {
        /// <summary>
        /// Parses a URDF &lt;mesh&gt; element from XML.
        /// </summary>
        /// <param name="meshNode">The XML node of a &lt;mesh&gt; element</param>
        /// <returns>A Mesh object parsed from the XML</returns>
        public Mesh Parse(XmlNode meshNode)
        {
            // TODO: Implement...!
            throw new NotImplementedException();
        }
    }
}
