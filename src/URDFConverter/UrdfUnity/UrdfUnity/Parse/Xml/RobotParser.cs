using System.Collections.Generic;
using System.Xml;
using UrdfUnity.Parse.Xml.LinkElements.VisualElements;
using UrdfUnity.Urdf.Models;
using UrdfUnity.Urdf.Models.LinkElements.VisualElements;
using UrdfUnity.Util;

namespace UrdfUnity.Parse.Xml
{
    /// <summary>
    /// Parses a URDF &lt;robot&gt; root element from XML into a Robot object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/model"/>
    /// <seealso cref="Urdf.Models.Robot"/>
    public sealed class RobotParser : AbstractUrdfXmlParser<Robot>
    {
        private static readonly string NAME_ATTRIBUTE_NAME = "name";
        private static readonly string LINK_ELEMENT_NAME = "link";
        private static readonly string JOINT_ELEMENT_NAME = "joint";
        private static readonly string MATERIAL_ELEMENT_NAME = "material";


        /// <summary>
        /// The name of the URDF XML element that this class parses.
        /// </summary>
        protected override string ElementName { get; } = "robot";


        private readonly Dictionary<string, Link> links = new Dictionary<string, Link>();
        private readonly Dictionary<string, Joint> joints = new Dictionary<string, Joint>();
        private readonly Dictionary<string, Material> materials = new Dictionary<string, Material>();

        private readonly LinkParser linkParser;
        private readonly JointParser jointParser;
        private readonly MaterialParser materialParser;


        /// <summary>
        /// Creates a new instance of RobotParser.
        /// </summary>
        public RobotParser()
        {
            this.linkParser = new LinkParser(materials);
            this.jointParser = new JointParser(links, joints);
            this.materialParser = new MaterialParser(materials);
        }


        /// <summary>
        /// Parses a URDF &lt;robot&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;robot&gt; element</param>
        /// <returns>A Robot object parsed from the XML</returns>
        public override Robot Parse(XmlNode node)
        {
            ValidateXmlNode(node);

            XmlAttribute nameAttribute = GetAttributeFromNode(node, NAME_ATTRIBUTE_NAME);
            XmlNodeList linkElements = node.SelectNodes(LINK_ELEMENT_NAME);
            XmlNodeList jointElements = node.SelectNodes(JOINT_ELEMENT_NAME);
            XmlNodeList materialElements = node.SelectNodes(MATERIAL_ELEMENT_NAME);

            string name = ParseName(nameAttribute);

            // Parse all top-level materials that may be referenced by links
            ParseMaterials(materialElements);

            // Parse all links that will be referenced by joints
            ParseLinks(linkElements);

            // Parse the joints
            ParseJoints(jointElements);

            // Finally, construct the Robot model
            Robot robot = new Robot(name, this.links, this.joints);

            return robot;
        }

        private string ParseName(XmlAttribute nameAttribute)
        {
            if (nameAttribute == null)
            {
                // TODO: Log missing required <link> name attribute
                return Robot.DEFAULT_NAME;
            }

            return nameAttribute.Value;
        }

        private void ParseMaterials(XmlNodeList materialElements)
        {
            if (materialElements.Count > 0)
            {
                foreach (XmlNode materialNode in materialElements)
                {
                    Material material = this.materialParser.Parse(materialNode);
                    this.materials.Add(material.Name, material);
                }
            }
        }

        private void ParseLinks(XmlNodeList linkElements)
        {
            if (linkElements.Count == 0)
            {
                // TODO: Log URDF file doesn't contain any top-level <link> elements
            }
            else
            {
                foreach (XmlNode linkNode in linkElements)
                {
                    Link link = this.linkParser.Parse(linkNode);
                    this.links.Add(link.Name, link);
                }
            }
        }

        private void ParseJoints(XmlNodeList jointElements)
        {
            if (jointElements.Count == 0)
            {
                // TODO: Log URDF file doesn't contain any top-level <joint> elements
            }
            else
            {
                foreach (XmlNode jointNode in jointElements)
                {
                    Joint joint = this.jointParser.Parse(jointNode);
                    this.joints.Add(joint.Name, joint);
                }
            }
        }
    }
}
