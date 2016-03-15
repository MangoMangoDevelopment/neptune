using System;
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
    public class LimitParser : XmlParser<Limit>
    {
        private static readonly string LOWER_ATTRIBUTE_NAME = "lower";
        private static readonly string UPPER_ATTRIBUTE_NAME = "upper";
        private static readonly string EFFORT_ATTRIBUTE_NAME = "effort";
        private static readonly string VELOCITY_ATTRIBUTE_NAME = "velocity";
        private static readonly double DEFAULT_VALUE = 0d;


        /// <summary>
        /// Parses a URDF &lt;limit&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;limit&gt; element</param>
        /// <returns>An Limit object parsed from the XML</returns>
        public Limit Parse(XmlNode node)
        {
            Preconditions.IsNotNull(node, "node");

            XmlAttribute lowerAttribute = (node.Attributes != null ? (XmlAttribute)node.Attributes.GetNamedItem(LOWER_ATTRIBUTE_NAME) : null);
            XmlAttribute upperAttribute = (node.Attributes != null ? (XmlAttribute)node.Attributes.GetNamedItem(UPPER_ATTRIBUTE_NAME) : null);
            XmlAttribute effortAttribute = (node.Attributes != null ? (XmlAttribute)node.Attributes.GetNamedItem(EFFORT_ATTRIBUTE_NAME) : null);
            XmlAttribute velocityAttribute = (node.Attributes != null ? (XmlAttribute)node.Attributes.GetNamedItem(VELOCITY_ATTRIBUTE_NAME) : null);
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

            return new Limit(lower, upper, effort, velocity);
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
