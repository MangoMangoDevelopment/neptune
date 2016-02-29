using System;
using System.Xml;
using UrdfUnity.Urdf.Models;

namespace UrdfUnity.Parse.Xml
{
    /// <summary>
    /// Parses a URDF &lt;joint&gt; element from XML into a Joint object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="Urdf.Models.Joint"/>
    class JointParser : XmlParser<Joint>
    {
        /// <summary>
        /// Parses a URDF &lt;joint&gt; element from XML.
        /// </summary>
        /// <param name="jointNode">The XML node of a &lt;joint&gt; element</param>
        /// <returns>A Joint object parsed from the XML</returns>
        public Joint Parse(XmlNode jointNode)
        {
            // TODO: Implement...!
            throw new NotImplementedException();
        }
    }
}
