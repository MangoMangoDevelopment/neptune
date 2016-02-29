using System;
using System.Xml;
using UrdfUnity.Urdf.Models.JointElements;

namespace UrdfUnity.Parse.Xml.JointElements
{
    /// <summary>
    /// Parses a URDF &lt;calibration&gt; element from XML into a Calibration object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="Urdf.Models.JointElements.Calibration"/>
    class CalibrationParser : XmlParser<Calibration>
    {
        /// <summary>
        /// Parses a URDF &lt;calibration&gt; element from XML.
        /// </summary>
        /// <param name="calibrationNode">The XML node of a &lt;calibration&gt; element</param>
        /// <returns>An Calibration object parsed from the XML</returns>
        public Calibration Parse(XmlNode calibrationNode)
        {
            // TODO: Implement...!
            throw new NotImplementedException();
        }
    }
}
