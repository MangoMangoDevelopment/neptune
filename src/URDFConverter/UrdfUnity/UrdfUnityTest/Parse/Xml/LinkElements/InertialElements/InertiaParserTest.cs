using System;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Parse.Xml.LinkElements.InertialElements;
using UrdfUnity.Urdf.Models.LinkElements.InertialElements;

namespace UrdfUnityTest.Parse.Xml.LinkElements.InertialElements
{
    [TestClass]
    public class InertiaParserTest
    {
        private static readonly string FORMAT_STRING = "<inertia ixx='{0}' ixy='{1}' ixz='{2}' iyy='{3}' iyz='{4}' izz='{5}'/>";

        private readonly InertiaParser parser = new InertiaParser();
        private readonly XmlDocument xmlDoc = new XmlDocument();


        [TestMethod]
        public void ParseInertia()
        {
            double ixx = 1;
            double ixy = 2;
            double ixz = 3;
            double iyy = 4;
            double iyz = 5;
            double izz = 6;

            string xml = String.Format(FORMAT_STRING, ixx, ixy, ixz, iyy, iyz, izz);
            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Inertia inertia = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(ixx, inertia.Ixx);
            Assert.AreEqual(ixy, inertia.Ixy);
            Assert.AreEqual(ixz, inertia.Ixz);
            Assert.AreEqual(iyy, inertia.Iyy);
            Assert.AreEqual(iyz, inertia.Iyz);
            Assert.AreEqual(izz, inertia.Izz);
        }

        [TestMethod]
        public void ParseInertiaUnorderedAttributes()
        {
            double ixx = 1;
            double ixy = 2;
            double ixz = 3;
            double iyy = 4;
            double iyz = 5;
            double izz = 6;

            string xml = String.Format("<inertia ixx='{0}' izz='{5}' ixy='{1}' iyz='{4}' iyy='{3}' ixz='{2}'/>",
                ixx, ixy, ixz, iyy, iyz, izz);
            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Inertia inertia = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(ixx, inertia.Ixx);
            Assert.AreEqual(ixy, inertia.Ixy);
            Assert.AreEqual(ixz, inertia.Ixz);
            Assert.AreEqual(iyy, inertia.Iyy);
            Assert.AreEqual(iyz, inertia.Iyz);
            Assert.AreEqual(izz, inertia.Izz);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseInertiaNullNode()
        {
            this.parser.Parse(null);
        }

        [TestMethod]
        public void ParseInertiaMissingAttribute()
        {
            string xml = "<inertia ixx='1' ixy='1' ixz='1' iyy='1' iyz='1'/>"; // No izz attribute
            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Inertia inertia = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(1, inertia.Ixx);
            Assert.AreEqual(1, inertia.Ixy);
            Assert.AreEqual(1, inertia.Ixz);
            Assert.AreEqual(1, inertia.Iyy);
            Assert.AreEqual(1, inertia.Iyz);
            Assert.AreEqual(InertiaParser.DEFAULT_VALUE, inertia.Izz);
        }

        [TestMethod]
        public void DefaultValue()
        {
            Assert.AreEqual(Double.NaN, InertiaParser.DEFAULT_VALUE);
        }
    }
}
