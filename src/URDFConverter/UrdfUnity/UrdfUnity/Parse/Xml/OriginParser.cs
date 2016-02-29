using System;
using System.Xml;
using UrdfUnity.Urdf.Models;

namespace UrdfUnity.Parse.Xml
{
    /// <summary>
    /// Parses a URDF &lt;origin&gt; element from XML into a Origin object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="Urdf.Models.Origin"/>
    class OriginParser : XmlParser<Origin>
    {
        /// <summary>
        /// Parses a URDF &lt;origin&gt; element from XML.
        /// </summary>
        /// <param name="originNode">The XML node of a &lt;origin&gt; element</param>
        /// <returns>A Origin object parsed from the XML</returns>
        public Origin Parse(XmlNode originNode)
        {
            // TODO: Implement...!
            throw new NotImplementedException();
        }
    }
}
