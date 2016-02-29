using System;
using System.Xml;
using UrdfUnity.Urdf.Models.JointElements;

namespace UrdfUnity.Parse.Xml.JointElements
{
    /// <summary>
    /// Parses a URDF &lt;safety_controller&gt; element from XML into a SafetyController object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="Urdf.Models.JointElements.SafetyController"/>
    class SafetyControllerParser : XmlParser<SafetyController>
    {
        /// <summary>
        /// Parses a URDF &lt;safety_controller&gt; element from XML.
        /// </summary>
        /// <param name="safetyControllerNode">The XML node of a &lt;safety_controller&gt; element</param>
        /// <returns>An SafetyController object parsed from the XML</returns>
        public SafetyController Parse(XmlNode safetyControllerNode)
        {
            // TODO: Implement...!
            throw new NotImplementedException();
        }
    }
}
