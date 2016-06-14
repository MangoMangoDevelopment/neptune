using System.Xml;
//using NLog;
using UrdfToUnity.Urdf;
using UrdfToUnity.Urdf.Models.Joints;
using UrdfToUnity.Util;

namespace UrdfToUnity.Parse.Xml.Joints
{
    /// <summary>
    /// Parses a URDF &lt;calibration&gt; element from XML into a Calibration object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="Urdf.Models.Joints.Calibration"/>
    public sealed class CalibrationParser : AbstractUrdfXmlParser<Calibration>
    {
        private static readonly double DEFAULT_VALUE = 0d;


        //protected override Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The name of the URDF XML element that this class parses.
        /// </summary>
        protected override string ElementName { get; } = UrdfSchema.CALIBRATION_ELEMENT_NAME;


        /// <summary>
        /// Parses a URDF &lt;calibration&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;calibration&gt; element. MUST NOT BE NULL</param>
        /// <returns>An Calibration object parsed from the XML</returns>
        public override Calibration Parse(XmlNode node)
        {
            ValidateXmlNode(node);

            XmlAttribute risingAttribute = GetAttributeFromNode(node, UrdfSchema.RISING_ATTRIBUTE_NAME);
            XmlAttribute fallingAttribute = GetAttributeFromNode(node, UrdfSchema.FALLING_ATTRIBUTE_NAME);
            double rising = DEFAULT_VALUE;
            double falling = DEFAULT_VALUE;

            if (risingAttribute == null)
            {
                LogMissingOptionalAttribute(UrdfSchema.RISING_ATTRIBUTE_NAME);
            }
            else
            {
                rising = RegexUtils.MatchDouble(risingAttribute.Value, DEFAULT_VALUE);
            }

            if (fallingAttribute == null)
            {
                LogMissingOptionalAttribute(UrdfSchema.FALLING_ATTRIBUTE_NAME);
            }
            else
            {
                falling = RegexUtils.MatchDouble(fallingAttribute.Value, DEFAULT_VALUE);
            }

            return new Calibration(rising, falling);
        }
    }
}
