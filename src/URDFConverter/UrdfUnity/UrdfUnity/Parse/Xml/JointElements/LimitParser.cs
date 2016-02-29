using System;
using System.Xml;
using UrdfUnity.Urdf.Models.JointElements;

namespace UrdfUnity.Parse.Xml.JointElements
{
    /// <summary>
    /// Parses a URDF &lt;limit&gt; element from XML into a Limit object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="Urdf.Models.JointElements.Limit"/>
    class LimitParser : XmlParser<Limit>
    {
        /// <summary>
        /// Parses a URDF &lt;limit&gt; element from XML.
        /// </summary>
        /// <param name="limitNode">The XML node of a &lt;limit&gt; element</param>
        /// <returns>An Limit object parsed from the XML</returns>
        public Limit Parse(XmlNode limitNode)
        {
            // TODO: Implement...!
            throw new NotImplementedException();
        }
    }
}
