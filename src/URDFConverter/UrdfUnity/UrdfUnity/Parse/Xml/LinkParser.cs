using System;
using System.Xml;
using UrdfUnity.Urdf.Models;

namespace UrdfUnity.Parse.Xml
{
    /// <summary>
    /// Parses a URDF &lt;link&gt; element from XML into a Link object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="Urdf.Models.Link"/>
    class LinkParser : XmlParser<Link>
    {
        /// <summary>
        /// Parses a URDF &lt;link&gt; element from XML.
        /// </summary>
        /// <param name="linkNode">The XML node of a &lt;link&gt; element</param>
        /// <returns>A Link object parsed from the XML</returns>
        public Link Parse(XmlNode linkNode)
        {
            // TODO: Implement...!
            throw new NotImplementedException();
        }
    }
}
