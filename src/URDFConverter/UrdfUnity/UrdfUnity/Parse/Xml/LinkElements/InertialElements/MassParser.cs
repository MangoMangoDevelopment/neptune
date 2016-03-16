using System.Xml;
using UrdfUnity.Urdf.Models.LinkElements.InertialElements;
using UrdfUnity.Util;

namespace UrdfUnity.Parse.Xml.LinkElements.InertialElements
{
    /// <summary>
    /// Parses a URDF &lt;mass&gt; element from XML into a Mass object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="Urdf.Models.LinkElements.InertialElements.Mass"/>
    public class MassParser : XmlParser<Mass>
    {
        /// <summary>
        /// The default value used if the mass element is missing the required value.
        /// </summary>
        public static readonly double DEFAULT_MASS = 0d;

        private static readonly string VALUE_ATTRIBUTE_NAME = "value";


        /// <summary>
        /// Parses a URDF &lt;mass&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;mass&gt; element. MUST NOT BE NULL</param>
        /// <returns>A Mass object with the value parsed from the XML, or the default value of 0 if no mass value parsed</returns>
        public Mass Parse(XmlNode node)
        {
            Preconditions.IsNotNull(node, "node");

            XmlAttribute valueAttribute = (node.Attributes != null) ? (XmlAttribute)node.Attributes.GetNamedItem(VALUE_ATTRIBUTE_NAME) : null;
            return new Mass(RegexUtils.MatchDouble(valueAttribute.Value, DEFAULT_MASS));
        }
    }
}
