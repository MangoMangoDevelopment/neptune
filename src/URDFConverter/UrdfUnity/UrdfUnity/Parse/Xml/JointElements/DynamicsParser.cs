﻿using System.Xml;
using UrdfUnity.Urdf.Models.JointElements;
using UrdfUnity.Util;

namespace UrdfUnity.Parse.Xml.JointElements
{
    /// <summary>
    /// Parses a URDF &lt;dynamics&gt; element from XML into a Dynamics object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="Urdf.Models.JointElements.Dynamics"/>
    public sealed class DynamicsParser : AbstractUrdfXmlParser<Dynamics>
    {
        private static readonly string DAMPING_ATTRIBUTE_NAME = "damping";
        private static readonly string FRICTION_ATTRIBUTE_NAME = "friction";
        private static readonly double DEFAULT_VALUE = 0d;


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
                // TODO: Log missing optional <dynamics> rising attribute
            }
            else
            {
                damping = RegexUtils.MatchDouble(dampingAttribute.Value, DEFAULT_VALUE);
            }
            if (frictionAttribute == null)
            {
                // TODO: Log missing optional <dynamics> falling attribute
            }
            else
            {
                friction = RegexUtils.MatchDouble(frictionAttribute.Value, DEFAULT_VALUE);
            }

            return new Dynamics(damping, friction);
        }
    }
}
