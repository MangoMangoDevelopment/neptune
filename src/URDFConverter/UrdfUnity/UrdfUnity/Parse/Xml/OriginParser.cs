using System;
using System.Text.RegularExpressions;
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
        /// <summary>
        /// Regex used for parsing an &lt;origin&gt; element's xyz and rpy attributes.  These
        /// attributes are formatted as a string with three space-delimited real numbers.
        /// </summary>
        private static readonly Regex ATTRIBUTE_REGEX = new Regex(String.Format(@"^{0}\s+{0}\s+{0}$", RegexUtils.REAL_NUMBER_PATTERN));


        /// <summary>
        /// Parses a URDF &lt;origin&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;origin&gt; element</param>
        /// <returns>An Origin object parsed from the XML</returns>
        public Origin Parse(XmlNode node)
        {
            XmlAttributeCollection attributes = node.Attributes;
            XmlAttribute xyzAttribute = (XmlAttribute)attributes.GetNamedItem("xyz");
            XmlAttribute rpyAttribute = (XmlAttribute)attributes.GetNamedItem("rpy");

            Origin.Builder originBuilder = new Origin.Builder();

            if (xyzAttribute != null)
            {
                if (!ATTRIBUTE_REGEX.IsMatch(xyzAttribute.Value))
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
                if (!ATTRIBUTE_REGEX.IsMatch(rpyAttribute.Value))
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
