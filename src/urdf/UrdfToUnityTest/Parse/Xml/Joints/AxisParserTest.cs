using System;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfToUnity.Parse.Xml.Joints;
using UrdfToUnity.Urdf.Models.Joints;

namespace UrdfToUnityTest.Parse.Xml.Joints
{
    [TestClass]
    public class AxisParserTest
    {
        private readonly AxisParser parser = new AxisParser();
        private readonly XmlDocument xmlDoc = new XmlDocument();


        [TestMethod]
        public void ParseAxis()
        {
            double x = 1;
            double y = 2;
            double z = 3;
            string xml = String.Format("<axis xyz='{0} {1} {2}'/>", x, y, z);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Axis axis = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(x, axis.Xyz.X);
            Assert.AreEqual(y, axis.Xyz.Y);
            Assert.AreEqual(z, axis.Xyz.Z);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseAxisNullNode()
        {
            this.parser.Parse(null);
        }

        [TestMethod]
        public void ParseAxisMalformed()
        {
            string xml = "<axis></axis>";

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Axis axis = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(1, axis.Xyz.X);
            Assert.AreEqual(0, axis.Xyz.Y);
            Assert.AreEqual(0, axis.Xyz.Z);
        }
    }
}
