using System;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Parse.Xml.LinkElements.GeometryElements;
using UrdfUnity.Urdf.Models.LinkElements.GeometryElements;

namespace UrdfUnityTest.Parse.Xml.LinkElements.GeometryElements
{
    [TestClass]
    public class SphereParserTest
    {
        private readonly string FORMAT_STRING = "<sphere radius='{0}'/>";
        private readonly SphereParser parser = new SphereParser();
        private readonly XmlDocument xmlDoc = new XmlDocument();

        [TestMethod]
        public void ParseSphere()
        {
            double radius = 1;
            string xml = String.Format(FORMAT_STRING, radius);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Sphere sphere = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(radius, sphere.Radius);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseCylinderNullNode()
        {
            this.parser.Parse(null);
        }

        [TestMethod]
        public void ParseSphereMalformed()
        {
            string xml = "<sphere></sphere>";

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Sphere sphere = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(0, sphere.Radius);
        }
    }
}
