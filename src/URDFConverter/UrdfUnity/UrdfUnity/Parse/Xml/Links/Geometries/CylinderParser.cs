using System.Xml;
using NLog;
using UrdfUnity.Urdf;
using UrdfUnity.Urdf.Models.Links.Geometries;
using UrdfUnity.Util;

namespace UrdfUnity.Parse.Xml.Links.Geometries
{
    /// <summary>
    /// Parses a URDF &lt;cylinder&gt; element from XML into a Cylinder object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    /// <seealso cref="Urdf.Models.Links.Geometries.Cylinder"/>
    public sealed class CylinderParser : AbstractUrdfXmlParser<Cylinder>
    {
        private static readonly double DEFAULT_VALUE = 0d;


        protected override Logger Logger { get; } = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// The name of the URDF XML element that this class parses.
        /// </summary>
        protected override string ElementName { get; } = UrdfSchema.CYLINDER_ELEMENT_NAME;


        /// <summary>
        /// Parses a URDF &lt;cylinder&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;cylinder&gt; element. MUST NOT BE NULL</param>
        /// <returns>A Cylinder object parsed from the XML</returns>
        public override Cylinder Parse(XmlNode node)
        {
            ValidateXmlNode(node);

            XmlAttribute radiusAttribute = GetAttributeFromNode(node, UrdfSchema.RADIUS_ATTRIBUTE_NAME);
            XmlAttribute lengthAttribute = GetAttributeFromNode(node, UrdfSchema.LENGTH_ATTRIBUTE_NAME);

            double radius = DEFAULT_VALUE;
            double length = DEFAULT_VALUE;

            if (radiusAttribute == null)
            {
                LogMissingRequiredAttribute(UrdfSchema.RADIUS_ATTRIBUTE_NAME);
            }
            else
            {
                if (!RegexUtils.IsMatchNDoubles(radiusAttribute.Value, 1))
                {
                    LogMalformedAttribute(UrdfSchema.RADIUS_ATTRIBUTE_NAME);
                }
                else
                {
                    radius = RegexUtils.MatchDouble(radiusAttribute.Value, DEFAULT_VALUE);
                }
            }

            if (lengthAttribute == null)
            {
                LogMissingRequiredAttribute(UrdfSchema.LENGTH_ATTRIBUTE_NAME);
            }
            else
            {
                if (!RegexUtils.IsMatchNDoubles(lengthAttribute.Value, 1))
                {
                    LogMalformedAttribute(UrdfSchema.LENGTH_ATTRIBUTE_NAME);
                }
                else
                {
                    length = RegexUtils.MatchDouble(lengthAttribute.Value, DEFAULT_VALUE);
                }
            }

            return new Cylinder(radius, length);
        }
    }
}
