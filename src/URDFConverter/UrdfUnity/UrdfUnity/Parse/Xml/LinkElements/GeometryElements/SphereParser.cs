using System.Xml;
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
    public class SphereParser : XmlParser<Sphere>
    {
        private static readonly string RADIUS_ATTRIBUTE_NAME = "radius";
        private static readonly double DEFAULT_VALUE = 0d;


        /// <summary>
        /// Parses a URDF &lt;sphere&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;sphere&gt; element</param>
        /// <returns>A Sphere object parsed from the XML</returns>
        public Sphere Parse(XmlNode node)
        {
            Preconditions.IsNotNull(node, "node");

            XmlAttribute radiusAttribute = XmlParsingUtils.GetAttributeFromNode(node, RADIUS_ATTRIBUTE_NAME);
            double radius = DEFAULT_VALUE;

            if (radiusAttribute == null)
            {
                // TODO: Log malformed URDF <sphere> element encountered
            }
            else
            {
                radius = RegexUtils.MatchDouble(radiusAttribute.Value, DEFAULT_VALUE);
            }

            return new Sphere(radius);
        }
    }
}
