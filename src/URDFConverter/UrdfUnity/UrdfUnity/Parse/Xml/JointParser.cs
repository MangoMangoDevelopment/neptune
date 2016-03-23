using System.Collections.Generic;
using System.Xml;
using UrdfUnity.Parse.Xml.JointElements;
using UrdfUnity.Urdf.Models;
using UrdfUnity.Util;

namespace UrdfUnity.Parse.Xml
{
    /// <summary>
    /// Parses a URDF &lt;joint&gt; element from XML into a Joint object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="Urdf.Models.Joint"/>
    public sealed class JointParser : AbstractUrdfXmlParser<Joint>
    {
        private static readonly string NAME_ATTRIBUTE_NAME = "name";
        private static readonly string TYPE_ATTRIBUTE_NAME = "type";
        private static readonly string ORIGIN_ELEMENT_NAME = "origin";
        private static readonly string PARENT_ELEMENT_NAME = "parent";
        private static readonly string CHILD_ELEMENT_NAME = "child";
        private static readonly string AXIS_ELEMENT_NAME = "axis";
        private static readonly string CALIBRATION_ELEMENT_NAME = "calibration";
        private static readonly string DYNAMICS_ELEMENT_NAME = "dynamics";
        private static readonly string LIMIT_ELEMENT_NAME = "limit";
        private static readonly string MIMIC_ELEMENT_NAME = "mimic";
        private static readonly string SAFETY_CONTROLLER_ELEMENT_NAME = "safety_controller";


        /// <summary>
        /// The name of the URDF XML element that this class parses.
        /// </summary>
        protected override string ElementName { get; } = "joint";


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

            XmlAttribute nameAttribute = GetAttributeFromNode(node, NAME_ATTRIBUTE_NAME);
            XmlAttribute typeAttribute = GetAttributeFromNode(node, TYPE_ATTRIBUTE_NAME);
            XmlElement originElement = GetElementFromNode(node, ORIGIN_ELEMENT_NAME);
            XmlElement parentElement = GetElementFromNode(node, PARENT_ELEMENT_NAME);
            XmlElement childElement = GetElementFromNode(node, CHILD_ELEMENT_NAME);
            XmlElement axisElement = GetElementFromNode(node, AXIS_ELEMENT_NAME);
            XmlElement calibrationElement = GetElementFromNode(node, CALIBRATION_ELEMENT_NAME);
            XmlElement dynamicsElement = GetElementFromNode(node, DYNAMICS_ELEMENT_NAME);
            XmlElement limitElement = GetElementFromNode(node, LIMIT_ELEMENT_NAME);
            XmlElement mimicElement = GetElementFromNode(node, MIMIC_ELEMENT_NAME);
            XmlElement safetyControllerElement = GetElementFromNode(node, SAFETY_CONTROLLER_ELEMENT_NAME);

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
                // TODO: Log missing required <joint> name attribute
                name = Joint.DEFAULT_NAME;
            }
            else
            {
                name = nameAttribute.Value;
            }

            if (typeAttribute == null)
            {
                // TODO: Log missing required <joint> type attribute
                type = Joint.JointType.Unknown;
            }
            else
            {
                type = GetJointTypeFromName(typeAttribute.Value);
            }

            if (parentElement == null)
            {
                // TODO: Log missing required <joint> parent sub-element
                parent = new Link.Builder(Link.DEFAULT_NAME).Build();
            }
            else
            {
                string parentLinkName = this.parentParser.Parse(parentElement);

                if (!this.linkDictionary.ContainsKey(parentLinkName))
                {
                    // TODO: Log unknown link specified by <joint> parent sub-element
                    parent = new Link.Builder(parentLinkName).Build();
                }
                else
                {
                    parent = this.linkDictionary[parentLinkName];
                }
            }

            if (childElement == null)
            {
                // TODO: Log missing required <joint> child sub-element
                child = new Link.Builder(Link.DEFAULT_NAME).Build();
            }
            else
            {
                string childLinkName = this.childParser.Parse(childElement);

                if (!this.linkDictionary.ContainsKey(childLinkName))
                {
                    // TODO: Log unknown link specified by <joint> child sub-element
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
