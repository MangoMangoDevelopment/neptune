using System.Collections.Generic;
using System.Xml;
using NLog;
using UrdfUnity.Parse.Xml.Joints;
using UrdfUnity.Urdf;
using UrdfUnity.Urdf.Models;

namespace UrdfUnity.Parse.Xml
{
    /// <summary>
    /// Parses a URDF &lt;joint&gt; element from XML into a Joint object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="Urdf.Models.Joint"/>
    public sealed class JointParser : AbstractUrdfXmlParser<Joint>
    {
        protected override Logger Logger { get; } = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// The name of the URDF XML element that this class parses.
        /// </summary>
        protected override string ElementName { get; } = UrdfSchema.JOINT_ELEMENT_NAME;


        private readonly OriginParser originParser = new OriginParser();
        private readonly ParentParser parentParser = new ParentParser();
        private readonly ChildParser childParser = new ChildParser();
        private readonly AxisParser axisParser = new AxisParser();
        private readonly CalibrationParser calibrationParser = new CalibrationParser();
        private readonly DynamicsParser dynamicsParser = new DynamicsParser();
        private readonly LimitParser limitParser = new LimitParser();
        private readonly MimicParser mimicParser;
        private readonly SafetyControllerParser safetyControllerParser = new SafetyControllerParser();

        private readonly Dictionary<string, Link> linkDictionary;
        private readonly Dictionary<string, Joint> jointDictionary;


        /// <summary>
        /// Creates a new instance of JointParser with the provided dictionary of joints.
        /// </summary>
        /// <param name="linkDictionary">A dictionary of available links if link names as keys</param>
        /// <param name="jointDictionary">A dictionary of available joints with joint names as keys</param>
        public JointParser(Dictionary<string, Link> linkDictionary, Dictionary<string, Joint> jointDictionary)
        {
            this.linkDictionary = linkDictionary;
            this.jointDictionary = jointDictionary;
            this.mimicParser = new MimicParser(this.jointDictionary);
        }


        /// <summary>
        /// Parses a URDF &lt;joint&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;joint&gt; element</param>
        /// <returns>A Joint object parsed from the XML</returns>
        public override Joint Parse(XmlNode node)
        {
            ValidateXmlNode(node);

            XmlAttribute nameAttribute = GetAttributeFromNode(node, UrdfSchema.NAME_ATTRIBUTE_NAME);
            XmlAttribute typeAttribute = GetAttributeFromNode(node, UrdfSchema.JOINT_TYPE_ATTRIBUTE_NAME);
            XmlElement originElement = GetElementFromNode(node, UrdfSchema.ORIGIN_ELEMENT_NAME);
            XmlElement parentElement = GetElementFromNode(node, UrdfSchema.PARENT_ELEMENT_NAME);
            XmlElement childElement = GetElementFromNode(node, UrdfSchema.CHILD_ELEMENT_NAME);
            XmlElement axisElement = GetElementFromNode(node, UrdfSchema.AXIS_ELEMENT_NAME);
            XmlElement calibrationElement = GetElementFromNode(node, UrdfSchema.CALIBRATION_ELEMENT_NAME);
            XmlElement dynamicsElement = GetElementFromNode(node, UrdfSchema.DYNAMICS_ELEMENT_NAME);
            XmlElement limitElement = GetElementFromNode(node, UrdfSchema.LIMIT_ELEMENT_NAME);
            XmlElement mimicElement = GetElementFromNode(node, UrdfSchema.MIMIC_ELEMENT_NAME);
            XmlElement safetyControllerElement = GetElementFromNode(node, UrdfSchema.SAFETY_CONTROLLER_ELEMENT_NAME);

            Joint.Builder builder = ConstructBuilder(nameAttribute, typeAttribute, parentElement, childElement);

            if (originElement != null)
            {
                builder.SetOrigin(this.originParser.Parse(originElement));
            }
            if (axisElement != null)
            {
                builder.SetAxis(this.axisParser.Parse(axisElement));
            }
            if (calibrationElement != null)
            {
                builder.SetCalibration(this.calibrationParser.Parse(calibrationElement));
            }
            if (dynamicsElement != null)
            {
                builder.SetDynamics(this.dynamicsParser.Parse(dynamicsElement));
            }
            if (limitElement != null)
            {
                builder.SetLimit(this.limitParser.Parse(limitElement));
            }
            if (mimicElement != null)
            {
                builder.SetMimic(this.mimicParser.Parse(mimicElement));
            }
            if (safetyControllerElement != null)
            {
                builder.SetSafetyController(this.safetyControllerParser.Parse(safetyControllerElement));
            }

            return builder.Build();
        }

        /// <summary>
        /// Returns the JointType corresponding to the provided string representation.
        /// </summary>
        /// <param name="name">The string representation of the joint type</param>
        /// <returns>The JointType matching the provided string, otherwise <c>JointType.Unknown</c></returns>
        public static Joint.JointType GetJointTypeFromName(string name)
        {
            switch (name)
            {
                case "revolute":
                    return Joint.JointType.Revolute;
                case "continuous":
                    return Joint.JointType.Continuous;
                case "prismatic":
                    return Joint.JointType.Prismatic;
                case "fixed":
                    return Joint.JointType.Fixed;
                case "floating":
                    return Joint.JointType.Floating;
                case "planar":
                    return Joint.JointType.Planar;
                default:
                    return Joint.JointType.Unknown;
            }
        }

        private Joint.Builder ConstructBuilder(XmlAttribute nameAttribute, XmlAttribute typeAttribute,
            XmlElement parentElement, XmlElement childElement)
        {
            string name;
            Joint.JointType type;
            Link parent;
            Link child;

            if (nameAttribute == null)
            {
                LogMissingRequiredAttribute(UrdfSchema.NAME_ATTRIBUTE_NAME);
                name = Joint.DEFAULT_NAME;
            }
            else
            {
                name = nameAttribute.Value;
            }

            if (typeAttribute == null)
            {
                LogMissingRequiredAttribute(UrdfSchema.JOINT_TYPE_ATTRIBUTE_NAME);
                type = Joint.JointType.Unknown;
            }
            else
            {
                type = GetJointTypeFromName(typeAttribute.Value);
            }

            if (parentElement == null)
            {
                LogMissingRequiredElement(UrdfSchema.PARENT_ELEMENT_NAME);
                parent = new Link.Builder(Link.DEFAULT_NAME).Build();
            }
            else
            {
                string parentLinkName = this.parentParser.Parse(parentElement);

                if (!this.linkDictionary.ContainsKey(parentLinkName))
                {
                    Logger.Warn("Unknown link name specified as <joint> parent: {0}", parentLinkName);
                    parent = new Link.Builder(parentLinkName).Build();
                }
                else
                {
                    parent = this.linkDictionary[parentLinkName];
                }
            }

            if (childElement == null)
            {
                LogMissingRequiredElement(UrdfSchema.CHILD_ELEMENT_NAME);
                child = new Link.Builder(Link.DEFAULT_NAME).Build();
            }
            else
            {
                string childLinkName = this.childParser.Parse(childElement);

                if (!this.linkDictionary.ContainsKey(childLinkName))
                {
                    Logger.Warn("Unknown link name specified as <joint> child: {0}", childLinkName);
                    child = new Link.Builder(childLinkName).Build();
                }
                else
                {
                    child = this.linkDictionary[childLinkName];
                }
            }

            return new Joint.Builder(name, type, parent, child);
        }
    }
}
