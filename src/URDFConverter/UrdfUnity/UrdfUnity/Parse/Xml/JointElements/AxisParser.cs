using System.Xml;
using UrdfUnity.Urdf.Models;
using UrdfUnity.Urdf.Models.JointElements;
using UrdfUnity.Util;

namespace UrdfUnity.Parse.Xml.JointElements
{
    /// <summary>
    /// Parses a URDF &lt;axis&gt; element from XML into a Axis object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="Urdf.Models.JointElements.Axis"/>
    public class AxisParser : XmlParser<Axis>
    {
        private static readonly string XYZ_ATTRIBUTE_NAME = "xyz";
        private static readonly double DEFAULT_X_VALUE = 1;
        private static readonly double DEFAULT_Y_VALUE = 0;
        private static readonly double DEFAULT_Z_VALUE = 0;


        /// <summary>
        /// Parses a URDF &lt;axis&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;axis&gt; element</param>
        /// <returns>An Axis object parsed from the XML</returns>
        public Axis Parse(XmlNode node)
        {
            Preconditions.IsNotNull(node, "node");

            XmlAttribute xyzAttribute = (node.Attributes != null ? (XmlAttribute)node.Attributes.GetNamedItem(XYZ_ATTRIBUTE_NAME) : null);
            XyzAttribute xyz = new XyzAttribute(DEFAULT_X_VALUE, DEFAULT_Y_VALUE, DEFAULT_Z_VALUE);

            if (xyzAttribute == null)
            {
                // TODO: Log malformed URDF <box> element encountered
            }
            else
            {
                if (!RegexUtils.IsMatchNDoubles(xyzAttribute.Value, 3))
                {
                    // TODO: Log malformed URDF <box> size attribute encountered
                }
                else
                {
                    double[] values = RegexUtils.MatchDoubles(xyzAttribute.Value);
                    xyz = new XyzAttribute(values[0], values[1], values[2]);
                }
            }

            return new Axis(xyz);
        }
    }
}
