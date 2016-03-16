using System.Xml;
using UrdfUnity.Urdf.Models.LinkElements.GeometryElements;
using UrdfUnity.Util;

namespace UrdfUnity.Parse.Xml.LinkElements.GeometryElements
{
    /// <summary>
    /// Parses a URDF &lt;cylinder&gt; element from XML into a Cylinder object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    /// <seealso cref="Urdf.Models.LinkElements.GeometryElements.Cylinder"/>
    public class CylinderParser : XmlParser<Cylinder>
    {
        private static readonly string RADIUS_ATTRIBUTE_NAME = "radius";
        private static readonly string LENGTH_ATTRIBUTE_NAME = "length";
        private static readonly double DEFAULT_VALUE = 0d;


        /// <summary>
        /// Parses a URDF &lt;cylinder&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;cylinder&gt; element. MUST NOT BE NULL</param>
        /// <returns>A Cylinder object parsed from the XML</returns>
        public Cylinder Parse(XmlNode node)
        {
            Preconditions.IsNotNull(node, "node");

            XmlAttribute radiusAttribute = XmlParsingUtils.GetAttributeFromNode(node, RADIUS_ATTRIBUTE_NAME);
            XmlAttribute lengthAttribute = XmlParsingUtils.GetAttributeFromNode(node, LENGTH_ATTRIBUTE_NAME);

            double radius = DEFAULT_VALUE;
            double length = DEFAULT_VALUE;

            if (radiusAttribute == null)
            {
                // TODO: Log malformed URDF <cylinder> element encountered
            }
            else
            {
                radius = RegexUtils.MatchDouble(radiusAttribute.Value, DEFAULT_VALUE);
            }

            if (lengthAttribute == null)
            {
                // TODO: Log malformed URDF <cylinder> element encountered
            }
            else
            {
                length = RegexUtils.MatchDouble(lengthAttribute.Value, DEFAULT_VALUE);
            }

            return new Cylinder(radius, length);
        }
    }
}
