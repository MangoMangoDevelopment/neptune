using System;
using System.Xml;
using UrdfUnity.Urdf.Models.JointElements;
using UrdfUnity.Util;

namespace UrdfUnity.Parse.Xml.JointElements
{
    /// <summary>
    /// Parses a URDF &lt;calibration&gt; element from XML into a Calibration object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="Urdf.Models.JointElements.Calibration"/>
    public class CalibrationParser : XmlParser<Calibration>
    {
        private static readonly string RISING_ATTRIBUTE_NAME = "rising";
        private static readonly string FALLING_ATTRIBUTE_NAME = "falling";
        private static readonly double DEFAULT_VALUE = 0d;

        /// <summary>
        /// Parses a URDF &lt;calibration&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;calibration&gt; element</param>
        /// <returns>An Calibration object parsed from the XML</returns>
        public Calibration Parse(XmlNode node)
        {
            Preconditions.IsNotNull(node, "node");

            XmlAttribute risingAttribute = (node.Attributes != null ? (XmlAttribute)node.Attributes.GetNamedItem(RISING_ATTRIBUTE_NAME) : null);
            XmlAttribute fallingAttribute = (node.Attributes != null ? (XmlAttribute)node.Attributes.GetNamedItem(FALLING_ATTRIBUTE_NAME) : null);
            double rising = DEFAULT_VALUE;
            double falling = DEFAULT_VALUE;

            if (risingAttribute == null)
            {
                // TODO: Log missing optional <calibration> rising attribute
            }
            else
            {
                rising = RegexUtils.MatchDouble(risingAttribute.Value, DEFAULT_VALUE);
            }
            if (fallingAttribute == null)
            {
                // TODO: Log missing optional <calibration> falling attribute
            }
            else
            {
                falling = RegexUtils.MatchDouble(fallingAttribute.Value, DEFAULT_VALUE);
            }

            return new Calibration(rising, falling);
        }
    }
}
