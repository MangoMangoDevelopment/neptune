using System.Xml;
using NLog;
using UrdfToUnity.Urdf;
using UrdfToUnity.Urdf.Models.Joints;
using UrdfToUnity.Util;

namespace UrdfToUnity.Parse.Xml.Joints
{
    /// <summary>
    /// Parses a URDF &lt;dynamics&gt; element from XML into a Dynamics object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="Urdf.Models.Joints.Dynamics"/>
    public sealed class DynamicsParser : AbstractUrdfXmlParser<Dynamics>
    {
        private static readonly double DEFAULT_VALUE = 0d;


        protected override Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The name of the URDF XML element that this class parses.
        /// </summary>
        protected override string ElementName { get; } = UrdfSchema.DYNAMICS_ELEMENT_NAME;


        /// <summary>
        /// Parses a URDF &lt;dynamics&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;dynamics&gt; element. MUST NOT BE NULL</param>
        /// <returns>An Dynamics object parsed from the XML</returns>
        public override Dynamics Parse(XmlNode node)
        {
            ValidateXmlNode(node);

            XmlAttribute dampingAttribute = GetAttributeFromNode(node, UrdfSchema.DAMPING_ATTRIBUTE_NAME);
            XmlAttribute frictionAttribute = GetAttributeFromNode(node, UrdfSchema.FRICTION_ATTRIBUTE_NAME);
            double damping = DEFAULT_VALUE;
            double friction = DEFAULT_VALUE;

            if (dampingAttribute == null)
            {
                LogMissingOptionalAttribute(UrdfSchema.DAMPING_ATTRIBUTE_NAME);
            }
            else
            {
                damping = RegexUtils.MatchDouble(dampingAttribute.Value, DEFAULT_VALUE);
            }
            if (frictionAttribute == null)
            {
                LogMissingOptionalAttribute(UrdfSchema.FRICTION_ATTRIBUTE_NAME);
            }
            else
            {
                friction = RegexUtils.MatchDouble(frictionAttribute.Value, DEFAULT_VALUE);
            }

            return new Dynamics(damping, friction);
        }
    }
}
