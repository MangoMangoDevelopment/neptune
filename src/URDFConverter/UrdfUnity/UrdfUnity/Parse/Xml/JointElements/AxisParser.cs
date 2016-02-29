using System;
using System.Xml;
using UrdfUnity.Urdf.Models.JointElements;

namespace UrdfUnity.Parse.Xml.JointElements
{
    /// <summary>
    /// Parses a URDF &lt;axis&gt; element from XML into a Axis object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="Urdf.Models.JointElements.Axis"/>
    class AxisParser : XmlParser<Axis>
    {
        /// <summary>
        /// Parses a URDF &lt;axis&gt; element from XML.
        /// </summary>
        /// <param name="axisNode">The XML node of a &lt;axis&gt; element</param>
        /// <returns>An Axis object parsed from the XML</returns>
        public Axis Parse(XmlNode axisNode)
        {
            // TODO: Implement...!
            throw new NotImplementedException();
        }
    }
}
