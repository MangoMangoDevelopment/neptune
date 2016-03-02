using System;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Parse.Xml;
using UrdfUnity.Urdf.Models;

namespace UrdfUnityTest.Parse.Xml
{
    [TestClass]
    public class OriginParserTest
    {
        private readonly OriginParser parser = new OriginParser();
        private readonly XmlDocument xmlDoc = new XmlDocument();


        [TestMethod]
        public void ParseOriginXyzAndRpy()
        {
            double xyzX = 0;
            double xyzY = 0;
            double xyzZ = -0.3;
            double rpyR = 0;
            double rpyP = 1.57075;
            double rpyY = 0;

            // <origin rpy='0 1.57075 0' xyz='0 0 -0.3'/>
            string xml = String.Format("<origin rpy='{0} {1} {2}' xyz='{3} {4} {5}'/>", rpyR, rpyP, rpyY, xyzX, xyzY, xyzZ);
            
            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Origin origin = this.parser.Parse(xmlDoc.DocumentElement);

            Assert.AreEqual(xyzX, origin.Xyz.X);
            Assert.AreEqual(xyzY, origin.Xyz.Y);
            Assert.AreEqual(xyzZ, origin.Xyz.Z);
            Assert.AreEqual(rpyR, origin.Rpy.R);
            Assert.AreEqual(rpyP, origin.Rpy.P);
            Assert.AreEqual(rpyY, origin.Rpy.Y);
        }

        [TestMethod]
        public void ParseOriginXyzOnly()
        {
            double xyzX = 0;
            double xyzY = -0.22;
            double xyzZ = .25;

            // <origin xyz='0 -0.22 0.25'/>
            string xml = String.Format("<origin xyz='{0} {1} {2}'/>", xyzX, xyzY, xyzZ);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Origin origin = this.parser.Parse(xmlDoc.DocumentElement);

            Assert.AreEqual(xyzX, origin.Xyz.X);
            Assert.AreEqual(xyzY, origin.Xyz.Y);
            Assert.AreEqual(xyzZ, origin.Xyz.Z);
            Assert.AreEqual(0, origin.Rpy.R);
            Assert.AreEqual(0, origin.Rpy.P);
            Assert.AreEqual(0, origin.Rpy.Y);

        }

        [TestMethod]
        public void ParseOriginRpyOnly()
        {
            double rpyR = 0;
            double rpyP = 1.57075;
            double rpyY = 0;

            // <origin rpy='0 1.57075 0'/>
            string xml = String.Format("<origin rpy='{0} {1} {2}'/>", rpyR, rpyP, rpyY);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Origin origin = this.parser.Parse(xmlDoc.DocumentElement);

            Assert.AreEqual(0, origin.Xyz.X);
            Assert.AreEqual(0, origin.Xyz.Y);
            Assert.AreEqual(0, origin.Xyz.Z);
            Assert.AreEqual(rpyR, origin.Rpy.R);
            Assert.AreEqual(rpyP, origin.Rpy.P);
            Assert.AreEqual(rpyY, origin.Rpy.Y);
        }

        [TestMethod]
        public void ParseMalformedOrigin()
        {
            double x = 1;
            double y = 2;
            double z = 3;

            string xml = String.Format("<origin xyz='{0} {1} {2}' rpy='1 2'/>", x, y, z); // Missing y-value for rpy
            
            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Origin origin = this.parser.Parse(xmlDoc.DocumentElement);

            Assert.AreEqual(x, origin.Xyz.X);
            Assert.AreEqual(y, origin.Xyz.Y);
            Assert.AreEqual(z, origin.Xyz.Z);
            Assert.AreEqual(0, origin.Rpy.R);
            Assert.AreEqual(0, origin.Rpy.P);
            Assert.AreEqual(0, origin.Rpy.Y);
        }
    }
}
