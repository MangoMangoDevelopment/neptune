using System.Xml;
//using NLog;
using UrdfToUnity.Urdf;
using UrdfToUnity.Urdf.Models.Joints;
using UrdfToUnity.Util;

namespace UrdfToUnity.Parse.Xml.Joints
{
    /// <summary>
    /// Parses a URDF &lt;limit&gt; element from XML into a Limit object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="Urdf.Models.Joints.Limit"/>
    public sealed class LimitParser : AbstractUrdfXmlParser<Limit>
    {
        private static readonly double DEFAULT_VALUE = 0d;


        //protected override Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The name of the URDF XML element that this class parses.
        /// </summary>
        protected override string ElementName { get; } = UrdfSchema.LIMIT_ELEMENT_NAME;


        /// <summary>
        /// Parses a URDF &lt;limit&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;limit&gt; element. MUST NOT BE NULL</param>
        /// <returns>An Limit object parsed from the XML</returns>
        public override Limit Parse(XmlNode node)
        {
            ValidateXmlNode(node);

            XmlAttribute lowerAttribute = GetAttributeFromNode(node, UrdfSchema.LOWER_ATTRIBUTE_NAME);
            XmlAttribute upperAttribute = GetAttributeFromNode(node, UrdfSchema.UPPER_ATTRIBUTE_NAME);
            XmlAttribute effortAttribute = GetAttributeFromNode(node, UrdfSchema.EFFORT_ATTRIBUTE_NAME);
            XmlAttribute velocityAttribute = GetAttributeFromNode(node, UrdfSchema.VELOCITY_ATTRIBUTE_NAME);
            double lower = ParseAttribute(lowerAttribute);
            double upper = ParseAttribute(upperAttribute);
            double effort = ParseAttribute(effortAttribute);
            double velocity = ParseAttribute(velocityAttribute);

            if (effortAttribute == null)
            {
                LogMissingRequiredAttribute(UrdfSchema.EFFORT_ATTRIBUTE_NAME);
            }
            if (velocityAttribute == null)
            {
                LogMissingRequiredAttribute(UrdfSchema.VELOCITY_ATTRIBUTE_NAME);
            }

            return new Limit(effort, velocity, lower, upper);
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
