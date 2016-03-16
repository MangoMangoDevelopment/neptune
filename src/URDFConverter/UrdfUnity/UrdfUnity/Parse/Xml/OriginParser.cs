using System.Xml;
using UrdfUnity.Urdf.Models;
using UrdfUnity.Util;

namespace UrdfUnity.Parse.Xml
{
    /// <summary>
    /// Parses a URDF &lt;origin&gt; element from XML into a Origin object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="Urdf.Models.Origin"/>
    public class OriginParser : XmlParser<Origin>
    {
        private static readonly string XYZ_ATTRIBUTE_NAME = "xyz";
        private static readonly string RPY_ATTRIBUTE_NAME = "rpy";


        /// <summary>
        /// Parses a URDF &lt;origin&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;origin&gt; element. MUST NOT BE NULL</param>
        /// <returns>An Origin object parsed from the XML</returns>
        public Origin Parse(XmlNode node)
        {
            Preconditions.IsNotNull(node, "node");

            XmlAttribute xyzAttribute = XmlParsingUtils.GetAttributeFromNode(node, XYZ_ATTRIBUTE_NAME);
            XmlAttribute rpyAttribute = XmlParsingUtils.GetAttributeFromNode(node, RPY_ATTRIBUTE_NAME);

            Origin.Builder originBuilder = new Origin.Builder();

            if (xyzAttribute != null)
            {
                if (!RegexUtils.IsMatchNDoubles(xyzAttribute.Value, 3))
                {
                    // TODO: Log malformed URDF <origin> xyz attribute encountered
                }
                else
                {
                    double[] values = RegexUtils.MatchDoubles(xyzAttribute.Value);
                    originBuilder.SetXyz(new XyzAttribute(values[0], values[1], values[2]));
                }
            }

            if (rpyAttribute != null)
            {
                if (!RegexUtils.IsMatchNDoubles(rpyAttribute.Value, 3))
                {
                    // TODO: Log malformed URDF <origin> rpy attribute encountered
                }
                else
                {
                    double[] values = RegexUtils.MatchDoubles(rpyAttribute.Value);
                    originBuilder.SetRpy(new RpyAttribute(values[0], values[1], values[2]));
                }
            }

            return originBuilder.Build();
        }
    }
}
