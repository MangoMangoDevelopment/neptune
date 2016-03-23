using System.Xml;
using NLog;
using UrdfUnity.Urdf.Models.JointElements;
using UrdfUnity.Util;

namespace UrdfUnity.Parse.Xml.JointElements
{
    /// <summary>
    /// Parses a URDF &lt;calibration&gt; element from XML into a Calibration object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="Urdf.Models.JointElements.Calibration"/>
    public sealed class CalibrationParser : AbstractUrdfXmlParser<Calibration>
    {
        private static readonly string RISING_ATTRIBUTE_NAME = "rising";
        private static readonly string FALLING_ATTRIBUTE_NAME = "falling";
        private static readonly double DEFAULT_VALUE = 0d;


        protected override Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The name of the URDF XML element that this class parses.
        /// </summary>
        protected override string ElementName { get; } = "calibration";


        /// <summary>
        /// Parses a URDF &lt;calibration&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;calibration&gt; element. MUST NOT BE NULL</param>
        /// <returns>An Calibration object parsed from the XML</returns>
        public override Calibration Parse(XmlNode node)
        {
            ValidateXmlNode(node);

            XmlAttribute risingAttribute = GetAttributeFromNode(node, RISING_ATTRIBUTE_NAME);
            XmlAttribute fallingAttribute = GetAttributeFromNode(node, FALLING_ATTRIBUTE_NAME);
            double rising = DEFAULT_VALUE;
            double falling = DEFAULT_VALUE;

            if (risingAttribute == null)
            {
                LogMissingOptionalAttribute(RISING_ATTRIBUTE_NAME);
            }
            else
            {
                rising = RegexUtils.MatchDouble(risingAttribute.Value, DEFAULT_VALUE);
            }

            if (fallingAttribute == null)
            {
                LogMissingOptionalAttribute(FALLING_ATTRIBUTE_NAME);
            }
            else
            {
                falling = RegexUtils.MatchDouble(fallingAttribute.Value, DEFAULT_VALUE);
            }

            return new Calibration(rising, falling);
        }
    }
}
