using System;
using System.Collections.Generic;
using System.Xml;
using NLog;
using UrdfUnity.Urdf;
using UrdfUnity.Urdf.Models;
using UrdfUnity.Urdf.Models.Joints;
using UrdfUnity.Util;

namespace UrdfUnity.Parse.Xml.Joints
{
    /// <summary>
    /// Parses a URDF &lt;mimic&gt; element from XML into a Mimic object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="Urdf.Models.Joints.Mimic"/>
    public sealed class MimicParser : AbstractUrdfXmlParser<Mimic>
    {
        private static readonly double DEFAULT_MULTIPLIER = 1;
        private static readonly double DEFAULT_OFFSET = 0;
        private static readonly Joint DEFAULT_JOINT = new Joint.Builder(Joint.DEFAULT_NAME, Joint.JointType.Unknown,
            new Link.Builder(Link.DEFAULT_NAME).Build(), new Link.Builder(Link.DEFAULT_NAME).Build()).Build();


        protected override Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The name of the URDF XML element that this class parses.
        /// </summary>
        protected override string ElementName { get; } = UrdfSchema.MIMIC_ELEMENT_NAME;

        /// <summary>
        /// The dictionary of joints available to mimic in the top-level URDF robot element.
        /// </summary>
        private readonly Dictionary<string, Joint> jointDictionary;


        /// <summary>
        /// Creates a new instance of MimicParser with the provided dictionary of joints.
        /// </summary>
        /// <param name="jointDictionary">A dictionary of available joints with joint names as keys</param>
        public MimicParser(Dictionary<string, Joint> jointDictionary)
        {
            this.jointDictionary = jointDictionary;
        }


        /// <summary>
        /// Parses a URDF &lt;mimic&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;mimic&gt; element</param>
        /// <returns>An Mimic object parsed from the XML</returns>
        public override Mimic Parse(XmlNode node)
        {
            ValidateXmlNode(node);

            XmlAttribute jointAttribute = GetAttributeFromNode(node, UrdfSchema.JOINT_ATTRIBUTE_NAME);
            XmlAttribute multiplierAttribute = GetAttributeFromNode(node, UrdfSchema.MULTIPLIER_ATTRIBUTE_NAME);
            XmlAttribute offsetAttribute = GetAttributeFromNode(node, UrdfSchema.OFFSET_ATTRIBUTE_NAME);
            Joint joint = DEFAULT_JOINT;
            double multiplier = (multiplierAttribute != null) ? RegexUtils.MatchDouble(multiplierAttribute.Value, DEFAULT_MULTIPLIER) : DEFAULT_MULTIPLIER;
            double offset = (offsetAttribute != null) ? RegexUtils.MatchDouble(offsetAttribute.Value, DEFAULT_OFFSET) : DEFAULT_OFFSET;

            if (jointAttribute == null || String.IsNullOrEmpty(jointAttribute.Value))
            {
                LogMissingRequiredAttribute(UrdfSchema.JOINT_ATTRIBUTE_NAME);
            }
            else
            {
                if (!this.jointDictionary.ContainsKey(jointAttribute.Value))
                {
                    Logger.Info("Unknown joint name specified in <mimic>: {0}", jointAttribute.Value);
                    joint = new Joint.Builder(jointAttribute.Value, DEFAULT_JOINT.Type, DEFAULT_JOINT.Parent, DEFAULT_JOINT.Child).Build();
                }
                else
                {
                    joint = this.jointDictionary[jointAttribute.Value];
                }
            }

            return new Mimic(joint, multiplier, offset);
        }
    }
}
