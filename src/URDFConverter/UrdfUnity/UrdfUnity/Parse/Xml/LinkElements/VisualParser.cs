using System;
using System.Xml;
using UrdfUnity.Urdf.Models.LinkElements;

namespace UrdfUnity.Parse.Xml.LinkElements
{
    /// <summary>
    /// Parses a URDF &lt;visual&gt; element from XML into a Visual object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    /// <seealso cref="Urdf.Models.LinkElements.Visual"/>
    class VisualParser : XmlParser<Visual>
    {
        /// <summary>
        /// Parses a URDF &lt;visual&gt; element from XML.
        /// </summary>
        /// <param name="visualNode">The XML node of a &lt;visual&gt; element</param>
        /// <returns>A Visual object parsed from the XML</returns>
        public Visual Parse(XmlNode visualNode)
        {
            // TODO: Implement...!
            throw new NotImplementedException();
        }
    }
}
