using System;
using System.Xml;
using UrdfUnity.Urdf.Models.LinkElements.VisualElements;

namespace UrdfUnity.Parse.Xml.LinkElements.VisualElements
{
    /// <summary>
    /// Parses a URDF &lt;texture&gt; element from XML into a Texture object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    /// <seealso cref="Urdf.Models.LinkElements.VisualElements.Texture"/>
    class TextureParser : XmlParser<Texture>
    {
        /// <summary>
        /// Parses a URDF &lt;texture&gt; element from XML.
        /// </summary>
        /// <param name="textureNode">The XML node of a &lt;texture&gt; element</param>
        /// <returns>A Texture object parsed from the XML</returns>
        public Texture Parse(XmlNode textureNode)
        {
            // TODO: Implement...!
            throw new NotImplementedException();
        }
    }
}
