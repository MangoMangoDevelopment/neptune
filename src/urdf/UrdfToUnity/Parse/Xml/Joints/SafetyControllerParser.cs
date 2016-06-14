using System.Xml;
//using NLog;
using UrdfToUnity.Urdf;
using UrdfToUnity.Urdf.Models.Joints;
using UrdfToUnity.Util;

namespace UrdfToUnity.Parse.Xml.Joints
{
    /// <summary>
    /// Parses a URDF &lt;safety_controller&gt; element from XML into a SafetyController object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="Urdf.Models.Joints.SafetyController"/>
    public sealed class SafetyControllerParser : AbstractUrdfXmlParser<SafetyController>
    {
        private static readonly double DEFAULT_VALUE = 0d;


        //protected override Logger Logger { get; } = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// The name of the URDF XML element that this class parses.
        /// </summary>
        protected override string ElementName { get; } = UrdfSchema.SAFETY_CONTROLLER_ELEMENT_NAME;


        /// <summary>
        /// Parses a URDF &lt;safety_controller&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;safety_controller&gt; element</param>
        /// <returns>An SafetyController object parsed from the XML</returns>
        public override SafetyController Parse(XmlNode node)
        {
            ValidateXmlNode(node);

            XmlAttribute lowerLimitAttribute = GetAttributeFromNode(node, UrdfSchema.LOWER_LIMIT_ATTRIBUTE_NAME);
            XmlAttribute upperLimitAttribute = GetAttributeFromNode(node, UrdfSchema.UPPER_LIMIT_ATTRIBUTE_NAME);
            XmlAttribute positionAttribute = GetAttributeFromNode(node, UrdfSchema.K_POSITION_ATTRIBUTE_NAME);
            XmlAttribute velocityAttribute = GetAttributeFromNode(node, UrdfSchema.K_VELOCITY_ATTRIBUTE_NAME);
            double lowerLimit = ParseAttribute(lowerLimitAttribute);
            double upperLimit = ParseAttribute(upperLimitAttribute);
            double position = ParseAttribute(positionAttribute);
            double velocity = ParseAttribute(velocityAttribute);

            if (velocityAttribute == null)
            {
                LogMissingRequiredAttribute(UrdfSchema.K_VELOCITY_ATTRIBUTE_NAME);
            }

            return new SafetyController(velocity, position, lowerLimit, upperLimit);
        }

        private double ParseAttribute(XmlAttribute attribute)
        {
            double value = DEFAULT_VALUE;

            if (attribute != null)
            {
                value = RegexUtils.MatchDouble(attribute.Value, DEFAULT_VALUE);
            }

            return value;
        }
    }
}
