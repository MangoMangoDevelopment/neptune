using System;
using System.Collections.Generic;
using System.Xml;
using UrdfUnity.Urdf.Models;
using UrdfUnity.Urdf.Models.JointElements;
using UrdfUnity.Util;

namespace UrdfUnity.Parse.Xml.JointElements
{
    /// <summary>
    /// Parses a URDF &lt;mimic&gt; element from XML into a Mimic object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="Urdf.Models.JointElements.Mimic"/>
    public class MimicParser : XmlParser<Mimic>
    {
        private static readonly string JOINT_ATTRIBUTE_NAME = "joint";
        private static readonly string MULTIPLIER_ATTRIBUTE_NAME = "multiplier";
        private static readonly string OFFSET_ATTRIBUTE_NAME = "offset";
        private static readonly double DEFAULT_MULTIPLIER = 1;
        private static readonly double DEFAULT_OFFSET = 0;
        private static readonly Joint DEFAULT_JOINT = new Joint.Builder(Joint.DEFAULT_NAME, Joint.JointType.Unknown,
            new Link.Builder(Link.DEFAULT_NAME).Build(), new Link.Builder(Link.DEFAULT_NAME).Build()).Build();


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
        public Mimic Parse(XmlNode node)
        {
            Preconditions.IsNotNull(node, "node");

            XmlAttribute jointAttribute = (node.Attributes != null ? (XmlAttribute)node.Attributes.GetNamedItem(JOINT_ATTRIBUTE_NAME) : null);
            XmlAttribute multiplierAttribute = (node.Attributes != null ? (XmlAttribute)node.Attributes.GetNamedItem(MULTIPLIER_ATTRIBUTE_NAME) : null);
            XmlAttribute offsetAttribute = (node.Attributes != null ? (XmlAttribute)node.Attributes.GetNamedItem(OFFSET_ATTRIBUTE_NAME) : null);
            Joint joint = DEFAULT_JOINT;
            double multiplier = (multiplierAttribute != null) ? RegexUtils.MatchDouble(multiplierAttribute.Value, DEFAULT_MULTIPLIER) : DEFAULT_MULTIPLIER;
            double offset = (offsetAttribute != null) ? RegexUtils.MatchDouble(offsetAttribute.Value, DEFAULT_OFFSET) : DEFAULT_OFFSET;

            if (jointAttribute == null || jointAttribute.Value == null || jointAttribute.Value.Equals(String.Empty))
            {
                // TODO: Log missing required <mimic> joint attribute
            }
            else
            {
                if (!this.jointDictionary.ContainsKey(jointAttribute.Value))
                {
                    // TODO: Log unknown joint specified by <mimic> joint attribute
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
