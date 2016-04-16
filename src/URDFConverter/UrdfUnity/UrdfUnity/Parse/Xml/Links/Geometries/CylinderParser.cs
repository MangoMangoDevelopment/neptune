using System.Xml;
using NLog;
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
        private static readonly string RADIUS_ATTRIBUTE_NAME = "radius";
        private static readonly string LENGTH_ATTRIBUTE_NAME = "length";
        private static readonly double DEFAULT_VALUE = 0d;


        protected override Logger Logger { get; } = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// The name of the URDF XML element that this class parses.
        /// </summary>
        protected override string ElementName { get; } = "cylinder";


        /// <summary>
        /// Parses a URDF &lt;cylinder&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;cylinder&gt; element. MUST NOT BE NULL</param>
        /// <returns>A Cylinder object parsed from the XML</returns>
        public override Cylinder Parse(XmlNode node)
        {
            ValidateXmlNode(node);

            XmlAttribute radiusAttribute = GetAttributeFromNode(node, RADIUS_ATTRIBUTE_NAME);
            XmlAttribute lengthAttribute = GetAttributeFromNode(node, LENGTH_ATTRIBUTE_NAME);

            double radius = DEFAULT_VALUE;
            double length = DEFAULT_VALUE;

            if (radiusAttribute == null)
            {
                LogMissingRequiredAttribute(RADIUS_ATTRIBUTE_NAME);
            }
            else
            {
                if (!RegexUtils.IsMatchNDoubles(radiusAttribute.Value, 1))
                {
                    LogMalformedAttribute(RADIUS_ATTRIBUTE_NAME);
                }
                else
                {
                    radius = RegexUtils.MatchDouble(radiusAttribute.Value, DEFAULT_VALUE);
                }
            }

            if (lengthAttribute == null)
            {
                LogMissingRequiredAttribute(LENGTH_ATTRIBUTE_NAME);
            }
            else
            {
                if (!RegexUtils.IsMatchNDoubles(lengthAttribute.Value, 1))
                {
                    LogMalformedAttribute(LENGTH_ATTRIBUTE_NAME);
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
