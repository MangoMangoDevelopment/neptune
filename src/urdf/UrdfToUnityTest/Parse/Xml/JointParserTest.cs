using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfToUnity.Parse.Xml;
using UrdfToUnity.Urdf.Models;
using UrdfToUnity.Urdf.Models.Attributes;
using UrdfToUnity.Urdf.Models.Joints;

namespace UrdfToUnityTest.Parse.Xml
{
    [TestClass]
    public class JointParserTest
    {
        private static readonly Dictionary<string, Link> linkDictionary = new Dictionary<string, Link>();
        private static readonly Dictionary<string, Joint> jointDictionary = new Dictionary<string, Joint>();

        private static readonly string PARENT_JOINT_NAME = "parent";
        private static readonly string CHILD_JOINT_NAME = "child";
        private static readonly string MIMIC_JOINT_NAME = "mimic";

        private readonly JointParser parser = new JointParser(linkDictionary, jointDictionary);
        private readonly XmlDocument xmlDoc = new XmlDocument();


        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            Link parent = new Link.Builder(PARENT_JOINT_NAME).Build();
            Link child = new Link.Builder(CHILD_JOINT_NAME).Build();
            Joint mimic = new Joint.Builder(MIMIC_JOINT_NAME, Joint.JointType.Continuous, parent, child).Build();

            linkDictionary.Add(parent.Name, parent);
            linkDictionary.Add(child.Name, child);
            jointDictionary.Add(mimic.Name, mimic);
        }

        [TestMethod]
        public void ParseJoint()
        {
            string name = "joint";
            string type = "continuous";
            Origin origin = new Origin.Builder().SetXyz(new XyzAttribute(1, 2, 3)).SetRpy(new RpyAttribute(1, 2, 3)).Build();
            Axis axis = new Axis(new XyzAttribute(1, 2, 3));
            Calibration calibration = new Calibration(1, 2);
            Dynamics dynamics = new Dynamics(1, 2);
            Limit limit = new Limit(1, 2);
            Mimic mimic = new Mimic(jointDictionary[MIMIC_JOINT_NAME], 2, 3);
            SafetyController safetyController = new SafetyController(1);

            StringBuilder xmlBuilder = new StringBuilder();

            xmlBuilder.Append(String.Format("<joint name='{0}' type='{1}'>", name, type));
            xmlBuilder.Append(String.Format("<origin rpy='{0} {1} {2}' xyz='{3} {4} {5}'/>", origin.Xyz.X, origin.Xyz.Y, origin.Xyz.Z, origin.Rpy.R, origin.Rpy.P, origin.Rpy.Y));
            xmlBuilder.Append(String.Format("<parent link='{0}'/>", PARENT_JOINT_NAME));
            xmlBuilder.Append(String.Format("<child link='{0}'/>", CHILD_JOINT_NAME));
            xmlBuilder.Append(String.Format("<axis xyz='{0} {1} {2}'/>", axis.Xyz.X, axis.Xyz.Y, axis.Xyz.Z));
            xmlBuilder.Append(String.Format("<calibration rising='{0}' falling='{1}'/>", calibration.Rising, calibration.Falling));
            xmlBuilder.Append(String.Format("<dynamics damping='{0}' friction='{1}'/>", dynamics.Damping, dynamics.Friction));
            xmlBuilder.Append(String.Format("<limit lower='{0}' upper='{1}' effort='{2}' velocity='{3}'/>", limit.Lower, limit.Upper, limit.Effort, limit.Velocity));
            xmlBuilder.Append(String.Format("<mimic joint='{0}' multiplier='{1}' offset='{2}'/>", MIMIC_JOINT_NAME, mimic.Multiplier, mimic.Offset));
            xmlBuilder.Append(String.Format("<safety_controller soft_lower_limit='{0}' soft_upper_limit='{1}' k_position='{2}' k_velocity='{3}'/>", safetyController.SoftLowerLimit, safetyController.SoftUpperLimit, safetyController.KPosition, safetyController.KVelocity));
            xmlBuilder.Append("</joint>");

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xmlBuilder.ToString())));
            Joint joint = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(name, joint.Name);
            Assert.AreEqual(JointParser.GetJointTypeFromName(type), joint.Type);
            Assert.AreEqual(origin, joint.Origin);
            Assert.AreEqual(PARENT_JOINT_NAME, joint.Parent.Name);
            Assert.AreEqual(CHILD_JOINT_NAME, joint.Child.Name);
            Assert.AreEqual(axis, joint.Axis);
            Assert.AreEqual(calibration, joint.Calibration);
            Assert.AreEqual(dynamics, joint.Dynamics);
            Assert.AreEqual(limit, joint.Limit);
            Assert.AreEqual(mimic, joint.Mimic);
            Assert.AreEqual(safetyController, joint.SafetyController);
        }

        [TestMethod]
        public void GetJointTypeFromName()
        {
            Assert.AreEqual(Joint.JointType.Continuous, JointParser.GetJointTypeFromName("continuous"));
            Assert.AreEqual(Joint.JointType.Fixed, JointParser.GetJointTypeFromName("fixed"));
            Assert.AreEqual(Joint.JointType.Floating, JointParser.GetJointTypeFromName("floating"));
            Assert.AreEqual(Joint.JointType.Planar, JointParser.GetJointTypeFromName("planar"));
            Assert.AreEqual(Joint.JointType.Prismatic, JointParser.GetJointTypeFromName("prismatic"));
            Assert.AreEqual(Joint.JointType.Revolute, JointParser.GetJointTypeFromName("revolute"));
            Assert.AreEqual(Joint.JointType.Unknown, JointParser.GetJointTypeFromName("not a valid type"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseJointNullNode()
        {
            this.parser.Parse(null);
        }

        [TestMethod]
        public void ParseJointMalformed()
        {
            string xml = "<joint></joint>";

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Joint joint = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(Joint.DEFAULT_NAME, joint.Name);
            Assert.AreEqual(Joint.JointType.Unknown, joint.Type);
            Assert.AreEqual(Link.DEFAULT_NAME, joint.Parent.Name);
            Assert.AreEqual(Link.DEFAULT_NAME, joint.Child.Name);
            Assert.AreEqual(new Origin(), joint.Origin);
            Assert.AreEqual(new Axis(new XyzAttribute(1, 0, 0)), joint.Axis);
            Assert.IsNull(joint.Calibration);
            Assert.IsNull(joint.Dynamics);
            Assert.IsNull(joint.Limit);
            Assert.IsNull(joint.Mimic);
            Assert.IsNull(joint.SafetyController);
        }
    }
}
