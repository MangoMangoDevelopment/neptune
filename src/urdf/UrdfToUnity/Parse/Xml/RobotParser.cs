using System.Collections.Generic;
using System.Xml;
//using NLog;
using UrdfToUnity.Parse.Xml.Links.Visuals;
using UrdfToUnity.Urdf;
using UrdfToUnity.Urdf.Models;
using UrdfToUnity.Urdf.Models.Links.Visuals;

namespace UrdfToUnity.Parse.Xml
{
    /// <summary>
    /// Parses a URDF &lt;robot&gt; root element from XML into a Robot object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/model"/>
    /// <seealso cref="Urdf.Models.Robot"/>
    public sealed class RobotParser : AbstractUrdfXmlParser<Robot>
    {
        //protected override Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The name of the URDF XML element that this class parses.
        /// </summary>
        protected override string ElementName { get; } = UrdfSchema.ROBOT_ELEMENT_NAME;


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

            XmlAttribute nameAttribute = GetAttributeFromNode(node, UrdfSchema.NAME_ATTRIBUTE_NAME);
            XmlNodeList Links = node.SelectNodes(UrdfSchema.LINK_ELEMENT_NAME);
            XmlNodeList Joints = node.SelectNodes(UrdfSchema.JOINT_ELEMENT_NAME);
            XmlNodeList materialElements = node.SelectNodes(UrdfSchema.MATERIAL_ELEMENT_NAME);

            string name = ParseName(nameAttribute);

            // Parse all top-level materials that may be referenced by links
            ParseMaterials(materialElements);

            // Parse all links that will be referenced by joints
            ParseLinks(Links);

            // Parse the joints
            ParseJoints(Joints);

            // Finally, construct the Robot model
            Robot robot = new Robot(name, this.links, this.joints);

            return robot;
        }

        private string ParseName(XmlAttribute nameAttribute)
        {
            if (nameAttribute == null)
            {
                LogMissingRequiredAttribute(UrdfSchema.NAME_ATTRIBUTE_NAME);
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

        private void ParseLinks(XmlNodeList Links)
        {
            if (Links.Count == 0)
            {
                //Logger.Warn("URDF file is missing required top-level link elements");
            }
            else
            {
                foreach (XmlNode linkNode in Links)
                {
                    Link link = this.linkParser.Parse(linkNode);
                    this.links.Add(link.Name, link);
                }
            }
        }

        private void ParseJoints(XmlNodeList Joints)
        {
            if (Joints.Count == 0)
            {
                //Logger.Warn("URDF file is missing required top-level joint elements");
            }
            else
            {
                foreach (XmlNode jointNode in Joints)
                {
                    Joint joint = this.jointParser.Parse(jointNode);
                    this.joints.Add(joint.Name, joint);
                }
            }
        }
    }
}
