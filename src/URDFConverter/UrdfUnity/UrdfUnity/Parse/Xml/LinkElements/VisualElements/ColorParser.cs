using System;
using System.Xml;
using UrdfUnity.Urdf.Models.LinkElements.VisualElements;

namespace UrdfUnity.Parse.Xml.LinkElements.VisualElements
{
    /// <summary>
    /// Parses a URDF &lt;color&gt; element from XML into a Color object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    /// <seealso cref="Urdf.Models.LinkElements.VisualElements.Color"/>
    class ColorParser : XmlParser<Color>
    {
        /// <summary>
        /// Parses a URDF &lt;color&gt; element from XML.
        /// </summary>
        /// <param name="colorNode">The XML node of a &lt;color&gt; element</param>
        /// <returns>A Color object parsed from the XML</returns>
        public Color Parse(XmlNode colorNode)
        {
            // TODO: Implement...!
            throw new NotImplementedException();
        }
    }
}
