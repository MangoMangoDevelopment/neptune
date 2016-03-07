using System;
using System.Text.RegularExpressions;
using System.Xml;
using UrdfUnity.Urdf.Models.LinkElements.GeometryElements;
using UrdfUnity.Util;

namespace UrdfUnity.Parse.Xml.LinkElements.GeometryElements
{
    /// <summary>
    /// Parses a URDF &lt;box&gt; element from XML into a Box object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    /// <seealso cref="Urdf.Models.LinkElements.GeometryElements.Box"/>
    public class BoxParser : XmlParser<Box>
    {
        private static readonly string SIZE_ATTRIBUTE_NAME = "size";
        private static readonly double DEFAULT_VALUE = 0d;


        /// <summary>
        /// Parses a URDF &lt;box&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;box&gt; element. MUST NOT BE NULL</param>
        /// <returns>A Box object parsed from the XML</returns>
        public Box Parse(XmlNode node)
        {
            Preconditions.IsNotNull(node, "node");

            XmlAttribute sizeAttribute = (node.Attributes != null ? (XmlAttribute)node.Attributes.GetNamedItem(SIZE_ATTRIBUTE_NAME) : null);
            SizeAttribute size = new SizeAttribute(DEFAULT_VALUE, DEFAULT_VALUE, DEFAULT_VALUE);

            if (sizeAttribute == null)
            {
                // TODO: Log malformed URDF <box> element encountered
            }
            else
            {
                if (!RegexUtils.ATTRIBUTE_REGEX_THREE_REAL_NUMBERS.IsMatch(sizeAttribute.Value))
                {
                    // TODO: Log malformed URDF <box> size attribute encountered
                }
                else
                {
                    double[] values = RegexUtils.MatchDoubles(sizeAttribute.Value);
                    size = new SizeAttribute(values[0], values[1], values[2]);
                }
            }

            return new Box(size);
        }
    }
}
