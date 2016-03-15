using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Parse.Xml.JointElements;
using UrdfUnity.Urdf.Models;
using UrdfUnity.Urdf.Models.JointElements;

namespace UrdfUnityTest.Parse.Xml.JointElements
{
    [TestClass]
    public class MimicParserTest
    {
        private static readonly Dictionary<string, Joint> jointDictionary = new Dictionary<string, Joint>();

        private readonly MimicParser parser = new MimicParser(jointDictionary);
        private readonly XmlDocument xmlDoc = new XmlDocument();

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            Joint joint1 = new Joint.Builder("joint1", Joint.JointType.Fixed, new Link.Builder("link1").Build(), new Link.Builder("link2").Build()).Build();
            Joint joint2 = new Joint.Builder("joint2", Joint.JointType.Fixed, new Link.Builder("link3").Build(), new Link.Builder("link4").Build()).Build();
            Joint joint3 = new Joint.Builder("joint3", Joint.JointType.Fixed, new Link.Builder("link5").Build(), new Link.Builder("link6").Build()).Build();

            jointDictionary.Add(joint1.Name, joint1);
            jointDictionary.Add(joint2.Name, joint2);
            jointDictionary.Add(joint3.Name, joint3);
        }

        [TestMethod]
        public void ParseMimic()
        {
            string joint = "joint2";
            double multiplier = 1;
            double offset = 2;
            string xml = String.Format("<mimic joint='{0}' multiplier='{1}' offset='{2}'/>", joint, multiplier, offset);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Mimic mimic = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(joint, mimic.Joint.Name);
            Assert.AreEqual(jointDictionary[joint], mimic.Joint);
            Assert.AreEqual(multiplier, mimic.Multiplier);
            Assert.AreEqual(offset, mimic.Offset);
        }

        [TestMethod]
        public void ParseMimicJointOnly()
        {
            string joint = "joint2";
            string xml = String.Format("<mimic joint='{0}'/>", joint);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Mimic mimic = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(joint, mimic.Joint.Name);
            Assert.AreEqual(jointDictionary[joint], mimic.Joint);
            Assert.AreEqual(1, mimic.Multiplier);
            Assert.AreEqual(0, mimic.Offset);
        }

        [TestMethod]
        public void ParseMimicUnknownJoint()
        {
            string joint = "unknown_joint";
            string xml = String.Format("<mimic joint='{0}'/>", joint);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Mimic mimic = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(joint, mimic.Joint.Name);
            Assert.AreEqual(Joint.JointType.Unknown, mimic.Joint.Type);
            Assert.AreEqual(1, mimic.Multiplier);
            Assert.AreEqual(0, mimic.Offset);
        }

        [TestMethod]
        public void ParseMimicMalformed()
        {
            string xml = "<mimic></mimic>";

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Mimic mimic = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(Joint.DEFAULT_NAME, mimic.Joint.Name);
            Assert.AreEqual(Joint.JointType.Unknown, mimic.Joint.Type);
            Assert.AreEqual(1, mimic.Multiplier);
            Assert.AreEqual(0, mimic.Offset);
        }
    }
}
