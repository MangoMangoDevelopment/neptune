using System;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Parse.Xml.Joints;
using UrdfUnity.Urdf.Models.Joints;

namespace UrdfUnityTest.Parse.Xml.Joints
{
    [TestClass]
    public class DynamicsParserTest
    {
        private readonly DynamicsParser parser = new DynamicsParser();
        private readonly XmlDocument xmlDoc = new XmlDocument();


        [TestMethod]
        public void ParseDynamics()
        {
            double damping = 1;
            double friction = 2;
            string xml = String.Format("<dynamics damping='{0}' friction='{1}'/>", damping, friction);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Dynamics dynamics = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(damping, dynamics.Damping);
            Assert.AreEqual(friction, dynamics.Friction);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseDynamicsNullNode()
        {
            this.parser.Parse(null);
        }

        [TestMethod]
        public void ParseDynamicsDampingOnly()
        {
            double damping = 1;
            string xml = String.Format("<dynamics damping='{0}'/>", damping);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Dynamics dynamics = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(damping, dynamics.Damping);
            Assert.AreEqual(0, dynamics.Friction);
        }

        [TestMethod]
        public void ParseDynamicsFrictionOnly()
        {
            double friction = 2;
            string xml = String.Format("<dynamics friction='{0}'/>", friction);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Dynamics dynamics = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(0, dynamics.Damping);
            Assert.AreEqual(friction, dynamics.Friction);
        }

        [TestMethod]
        public void ParseDynamicsMalformed()
        {
            string xml = String.Format("<dynamics/>");

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Dynamics dynamics = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(0, dynamics.Damping);
            Assert.AreEqual(0, dynamics.Friction);
        }
    }
}
