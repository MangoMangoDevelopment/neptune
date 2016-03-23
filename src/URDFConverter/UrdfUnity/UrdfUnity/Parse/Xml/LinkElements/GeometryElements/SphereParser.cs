using System.Xml;
using NLog;
using UrdfUnity.Urdf.Models.LinkElements.GeometryElements;
using UrdfUnity.Util;

namespace UrdfUnity.Parse.Xml.LinkElements.GeometryElements
{
    /// <summary>
    /// Parses a URDF &lt;sphere&gt; element from XML into a Sphere object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    /// <seealso cref="Urdf.Models.LinkElements.GeometryElements.Sphere"/>
    public sealed class SphereParser : AbstractUrdfXmlParser<Sphere>
    {
        private static readonly string RADIUS_ATTRIBUTE_NAME = "radius";
        private static readonly double DEFAULT_VALUE = 0d;


        protected override Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The name of the URDF XML element that this class parses.
        /// </summary>
        protected override string ElementName { get; } = "sphere";


        /// <summary>
        /// Parses a URDF &lt;sphere&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;sphere&gt; element</param>
        /// <returns>A Sphere object parsed from the XML</returns>
        public override Sphere Parse(XmlNode node)
        {
            ValidateXmlNode(node);

            XmlAttribute radiusAttribute = GetAttributeFromNode(node, RADIUS_ATTRIBUTE_NAME);
            double radius = DEFAULT_VALUE;

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

            return new Sphere(radius);
        }
    }
}
