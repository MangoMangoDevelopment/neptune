using System;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Parse.Xml.JointElements;
using UrdfUnity.Urdf.Models.JointElements;

namespace UrdfUnityTest.Parse.Xml.JointElements
{
    [TestClass]
    public class LimitParserTest
    {
        private readonly LimitParser parser = new LimitParser();
        private readonly XmlDocument xmlDoc = new XmlDocument();


        [TestMethod]
        public void ParseLimit()
        {
            double lower = 1;
            double upper = 2;
            double effort = 3;
            double velocity = 4;
            string xml = String.Format("<limit lower='{0}' upper='{1}' effort='{2}' velocity='{3}'/>", lower, upper, effort, velocity);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Limit limit = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(lower, limit.Lower);
            Assert.AreEqual(upper, limit.Upper);
            Assert.AreEqual(effort, limit.Effort);
            Assert.AreEqual(velocity, limit.Velocity);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseLimitNullNode()
        {
            this.parser.Parse(null);
        }

        [TestMethod]
        public void ParseLimitMalformed()
        {
            string xml = "<limit></limit>";

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Limit limit = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(0, limit.Lower);
            Assert.AreEqual(0, limit.Upper);
            Assert.AreEqual(0, limit.Effort);
            Assert.AreEqual(0, limit.Velocity);
        }

        [TestMethod]
        public void ParseLimitEffortVelocityOnly()
        {
            double effort = 3;
            double velocity = 4;
            string xml = String.Format("<limit effort='{0}' velocity='{1}'/>", effort, velocity);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Limit limit = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(0, limit.Lower);
            Assert.AreEqual(0, limit.Upper);
            Assert.AreEqual(effort, limit.Effort);
            Assert.AreEqual(velocity, limit.Velocity);
        }
    }
}
