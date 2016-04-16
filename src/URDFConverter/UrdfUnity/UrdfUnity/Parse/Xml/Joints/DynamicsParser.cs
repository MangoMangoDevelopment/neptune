using System.Xml;
using NLog;
using UrdfUnity.Urdf.Models.Joints;
using UrdfUnity.Util;

namespace UrdfUnity.Parse.Xml.Joints
{
    /// <summary>
    /// Parses a URDF &lt;dynamics&gt; element from XML into a Dynamics object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="Urdf.Models.Joints.Dynamics"/>
    public sealed class DynamicsParser : AbstractUrdfXmlParser<Dynamics>
    {
        private static readonly string DAMPING_ATTRIBUTE_NAME = "damping";
        private static readonly string FRICTION_ATTRIBUTE_NAME = "friction";
        private static readonly double DEFAULT_VALUE = 0d;


        protected override Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The name of the URDF XML element that this class parses.
        /// </summary>
        protected override string ElementName { get; } = "dynamics";


        /// <summary>
        /// Parses a URDF &lt;dynamics&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;dynamics&gt; element. MUST NOT BE NULL</param>
        /// <returns>An Dynamics object parsed from the XML</returns>
        public override Dynamics Parse(XmlNode node)
        {
            ValidateXmlNode(node);

            XmlAttribute dampingAttribute = GetAttributeFromNode(node, DAMPING_ATTRIBUTE_NAME);
            XmlAttribute frictionAttribute = GetAttributeFromNode(node, FRICTION_ATTRIBUTE_NAME);
            double damping = DEFAULT_VALUE;
            double friction = DEFAULT_VALUE;

            if (dampingAttribute == null)
            {
                LogMissingOptionalAttribute(DAMPING_ATTRIBUTE_NAME);
            }
            else
            {
                damping = RegexUtils.MatchDouble(dampingAttribute.Value, DEFAULT_VALUE);
            }
            if (frictionAttribute == null)
            {
                LogMissingOptionalAttribute(FRICTION_ATTRIBUTE_NAME);
            }
            else
            {
                friction = RegexUtils.MatchDouble(frictionAttribute.Value, DEFAULT_VALUE);
            }

            return new Dynamics(damping, friction);
        }
    }
}
