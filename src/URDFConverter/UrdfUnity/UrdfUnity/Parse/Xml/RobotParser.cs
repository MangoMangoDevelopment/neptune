using System;
using System.Xml;
using UrdfUnity.Urdf.Models;

namespace UrdfUnity.Parse.Xml
{
    /// <summary>
    /// Parses a URDF &lt;robot&gt; root element from XML into a Robot object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/model"/>
    /// <seealso cref="Urdf.Models.Robot"/>
    class RobotParser : XmlParser<Robot>
    {
        /// <summary>
        /// Parses a URDF &lt;robot&gt; element from XML.
        /// </summary>
        /// <param name="robotNode">The XML node of a &lt;robot&gt; element</param>
        /// <returns>A Robot object parsed from the XML</returns>
        public Robot Parse(XmlNode robotNode)
        {
            // TODO: Implement...!
            throw new NotImplementedException();
        }
    }
}
