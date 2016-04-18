using System.Xml;
using NLog;
using UrdfToUnity.Urdf;
using UrdfToUnity.Urdf.Models;
using UrdfToUnity.Urdf.Models.Attributes;
using UrdfToUnity.Util;

namespace UrdfToUnity.Parse.Xml
{
    /// <summary>
    /// Parses a URDF &lt;origin&gt; element from XML into a Origin object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="Urdf.Models.Origin"/>
    public sealed class OriginParser : AbstractUrdfXmlParser<Origin>
    {
        protected override Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The name of the URDF XML element that this class parses.
        /// </summary>
        protected override string ElementName { get; } = UrdfSchema.ORIGIN_ELEMENT_NAME;


        /// <summary>
        /// Parses a URDF &lt;origin&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;origin&gt; element. MUST NOT BE NULL</param>
        /// <returns>An Origin object parsed from the XML</returns>
        public override Origin Parse(XmlNode node)
        {
            ValidateXmlNode(node);

            XmlAttribute xyzAttribute = GetAttributeFromNode(node, UrdfSchema.XYZ_ATTRIBUTE_NAME);
            XmlAttribute rpyAttribute = GetAttributeFromNode(node, UrdfSchema.RPY_ATTRIBUTE_NAME);

            Origin.Builder originBuilder = new Origin.Builder();

            if (xyzAttribute != null)
            {
                if (!RegexUtils.IsMatchNDoubles(xyzAttribute.Value, 3))
                {
                    LogMalformedAttribute(UrdfSchema.XYZ_ATTRIBUTE_NAME);
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
                    LogMalformedAttribute(UrdfSchema.RPY_ATTRIBUTE_NAME);
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
