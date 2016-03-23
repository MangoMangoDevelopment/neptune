using System.Xml;
using NLog;
using UrdfUnity.Urdf.Models.LinkElements.InertialElements;
using UrdfUnity.Util;

namespace UrdfUnity.Parse.Xml.LinkElements.InertialElements
{
    /// <summary>
    /// Parses a URDF &lt;mass&gt; element from XML into a Mass object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="Urdf.Models.LinkElements.InertialElements.Mass"/>
    public class MassParser : AbstractUrdfXmlParser<Mass>
    {
        /// <summary>
        /// The default value used if the mass element is missing the required value.
        /// </summary>
        private static readonly double DEFAULT_MASS = 0d;

        private static readonly string VALUE_ATTRIBUTE_NAME = "value";


        protected override Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The name of the URDF XML element that this class parses.
        /// </summary>
        protected override string ElementName { get; } = "mass";


        /// <summary>
        /// Parses a URDF &lt;mass&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;mass&gt; element. MUST NOT BE NULL</param>
        /// <returns>A Mass object with the value parsed from the XML, or the default value of 0 if no mass value parsed</returns>
        public override Mass Parse(XmlNode node)
        {
            ValidateXmlNode(node);

            XmlAttribute valueAttribute = GetAttributeFromNode(node, VALUE_ATTRIBUTE_NAME);
            return new Mass(RegexUtils.MatchDouble(valueAttribute.Value, DEFAULT_MASS));
        }
    }
}
