using System;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfToUnity.Parse.Xml.Joints;
using UrdfToUnity.Urdf.Models.Joints;

namespace UrdfToUnityTest.Parse.Xml.Joints
{
    [TestClass]
    public class SafetyControllerParserTest
    {
        private readonly SafetyControllerParser parser = new SafetyControllerParser();
        private readonly XmlDocument xmlDoc = new XmlDocument();


        [TestMethod]
        public void ParseSafetyController()
        {
            double lower = 1;
            double upper = 2;
            double position = 3;
            double velocity = 4;
            string xml = String.Format("<safety_controller soft_lower_limit='{0}' soft_upper_limit='{1}' k_position='{2}' k_velocity='{3}'/>", lower, upper, position, velocity);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            SafetyController safetyController = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(lower, safetyController.SoftLowerLimit);
            Assert.AreEqual(upper, safetyController.SoftUpperLimit);
            Assert.AreEqual(position, safetyController.KPosition);
            Assert.AreEqual(velocity, safetyController.KVelocity);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseSafetyControllerNullNode()
        {
            this.parser.Parse(null);
        }

        [TestMethod]
        public void ParseSafetyControllerMalformed()
        {
            string xml = "<safety_controller></safety_controller>";

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            SafetyController safetyController = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(0, safetyController.SoftLowerLimit);
            Assert.AreEqual(0, safetyController.SoftUpperLimit);
            Assert.AreEqual(0, safetyController.KPosition);
            Assert.AreEqual(0, safetyController.KVelocity);
        }

        [TestMethod]
        public void ParseSafetyControllerVelocityOnly()
        {
            double velocity = 4;
            string xml = String.Format("<safety_controller k_velocity='{0}'/>", velocity);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            SafetyController safetyController = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(0, safetyController.SoftLowerLimit);
            Assert.AreEqual(0, safetyController.SoftUpperLimit);
            Assert.AreEqual(0, safetyController.KPosition);
            Assert.AreEqual(velocity, safetyController.KVelocity);
        }
    }
}
