using System.Xml;
using UrdfUnity.Urdf.Models.JointElements;
using UrdfUnity.Util;

namespace UrdfUnity.Parse.Xml.JointElements
{
    /// <summary>
    /// Parses a URDF &lt;limit&gt; element from XML into a Limit object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="Urdf.Models.JointElements.Limit"/>
    public sealed class LimitParser : AbstractUrdfXmlParser<Limit>
    {
        private static readonly string LOWER_ATTRIBUTE_NAME = "lower";
        private static readonly string UPPER_ATTRIBUTE_NAME = "upper";
        private static readonly string EFFORT_ATTRIBUTE_NAME = "effort";
        private static readonly string VELOCITY_ATTRIBUTE_NAME = "velocity";
        private static readonly double DEFAULT_VALUE = 0d;


        /// <summary>
        /// The name of the URDF XML element that this class parses.
        /// </summary>
        protected override string ElementName { get; } = "limit";


        /// <summary>
        /// Parses a URDF &lt;limit&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;limit&gt; element. MUST NOT BE NULL</param>
        /// <returns>An Limit object parsed from the XML</returns>
        public override Limit Parse(XmlNode node)
        {
            ValidateXmlNode(node);

            XmlAttribute lowerAttribute = GetAttributeFromNode(node, LOWER_ATTRIBUTE_NAME);
            XmlAttribute upperAttribute = GetAttributeFromNode(node, UPPER_ATTRIBUTE_NAME);
            XmlAttribute effortAttribute = GetAttributeFromNode(node, EFFORT_ATTRIBUTE_NAME);
            XmlAttribute velocityAttribute = GetAttributeFromNode(node, VELOCITY_ATTRIBUTE_NAME);
            double lower = ParseAttribute(lowerAttribute);
            double upper = ParseAttribute(upperAttribute);
            double effort = ParseAttribute(effortAttribute);
            double velocity = ParseAttribute(velocityAttribute);

            if (effortAttribute == null)
            {
                // TODO: Log missing required <limit> effort attribute
            }
            if (velocityAttribute == null)
            {
                // TODO: Log missing required <limit> velocity attribute
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
