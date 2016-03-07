using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Parse.Xml.LinkElements;
using UrdfUnity.Parse.Xml.LinkElements.InertialElements;
using UrdfUnity.Urdf.Models.LinkElements;

namespace UrdfUnityTest.Parse.Xml.LinkElements
{
    [TestClass]
    public class InertialParserTest
    {
        private readonly InertialParser parser = new InertialParser();
        private readonly XmlDocument xmlDoc = new XmlDocument();


        [TestMethod]
        public void ParseInertial()
        {
            string xml = @"
                <inertial>
                    <origin xyz='0 0 0.5' rpy='0 0 0'/>
                    <mass value='1'/>
                    <inertia ixx='100'  ixy='0'  ixz='0' iyy='100' iyz='0' izz='100'/>
                </inertial>";

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Inertial inertial = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(0, inertial.Origin.Xyz.X);
            Assert.AreEqual(0, inertial.Origin.Xyz.Y);
            Assert.AreEqual(0.5, inertial.Origin.Xyz.Z);
            Assert.AreEqual(0, inertial.Origin.Rpy.R);
            Assert.AreEqual(0, inertial.Origin.Rpy.P);
            Assert.AreEqual(0, inertial.Origin.Rpy.Y);
            Assert.AreEqual(1, inertial.Mass.Value);
            Assert.AreEqual(100, inertial.Inertia.Ixx);
            Assert.AreEqual(0, inertial.Inertia.Ixy);
            Assert.AreEqual(0, inertial.Inertia.Ixz);
            Assert.AreEqual(100, inertial.Inertia.Iyy);
            Assert.AreEqual(0, inertial.Inertia.Iyz);
            Assert.AreEqual(100, inertial.Inertia.Izz);
        }

        [TestMethod]
        public void ParseInertialNoOrigin()
        {
            string xml = @"
                <inertial>
                    <origin xyz='0 0 0.5' rpy='0 0 0'/>
                    <inertia ixx='100'  ixy='0'  ixz='0' iyy='100' iyz='0' izz='100'/>
                </inertial>";

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Inertial inertial = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(0, inertial.Origin.Xyz.X);
            Assert.AreEqual(0, inertial.Origin.Xyz.Y);
            Assert.AreEqual(0.5, inertial.Origin.Xyz.Z);
            Assert.AreEqual(0, inertial.Origin.Rpy.R);
            Assert.AreEqual(0, inertial.Origin.Rpy.P);
            Assert.AreEqual(0, inertial.Origin.Rpy.Y);
            Assert.AreEqual(MassParser.DEFAULT_MASS, inertial.Mass.Value);
            Assert.AreEqual(100, inertial.Inertia.Ixx);
            Assert.AreEqual(0, inertial.Inertia.Ixy);
            Assert.AreEqual(0, inertial.Inertia.Ixz);
            Assert.AreEqual(100, inertial.Inertia.Iyy);
            Assert.AreEqual(0, inertial.Inertia.Iyz);
            Assert.AreEqual(100, inertial.Inertia.Izz);
        }

        [TestMethod]
        public void ParseInertialNoMass()
        {
            string xml = @"
                <inertial>
                    <origin xyz='0 0 0.5' rpy='0 0 0'/>
                    <mass value='1'/>
                    <inertia ixx='100'  ixy='0'  ixz='0' iyy='100' iyz='0' izz='100'/>
                </inertial>";

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Inertial inertial = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(0, inertial.Origin.Xyz.X);
            Assert.AreEqual(0, inertial.Origin.Xyz.Y);
            Assert.AreEqual(0.5, inertial.Origin.Xyz.Z);
            Assert.AreEqual(0, inertial.Origin.Rpy.R);
            Assert.AreEqual(0, inertial.Origin.Rpy.P);
            Assert.AreEqual(0, inertial.Origin.Rpy.Y);
            Assert.AreEqual(1, inertial.Mass.Value);
            Assert.AreEqual(100, inertial.Inertia.Ixx);
            Assert.AreEqual(0, inertial.Inertia.Ixy);
            Assert.AreEqual(0, inertial.Inertia.Ixz);
            Assert.AreEqual(100, inertial.Inertia.Iyy);
            Assert.AreEqual(0, inertial.Inertia.Iyz);
            Assert.AreEqual(100, inertial.Inertia.Izz);
        }

        [TestMethod]
        public void ParseInertialNoInertia()
        {
            string xml = @"
                <inertial>
                    <origin xyz='0 0 0.5' rpy='0 0 0'/>
                    <mass value='1'/>
                </inertial>";

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Inertial inertial = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(0, inertial.Origin.Xyz.X);
            Assert.AreEqual(0, inertial.Origin.Xyz.Y);
            Assert.AreEqual(0.5, inertial.Origin.Xyz.Z);
            Assert.AreEqual(0, inertial.Origin.Rpy.R);
            Assert.AreEqual(0, inertial.Origin.Rpy.P);
            Assert.AreEqual(0, inertial.Origin.Rpy.Y);
            Assert.AreEqual(1, inertial.Mass.Value);
            Assert.AreEqual(InertiaParser.DEFAULT_VALUE, inertial.Inertia.Ixx);
            Assert.AreEqual(InertiaParser.DEFAULT_VALUE, inertial.Inertia.Ixy);
            Assert.AreEqual(InertiaParser.DEFAULT_VALUE, inertial.Inertia.Ixz);
            Assert.AreEqual(InertiaParser.DEFAULT_VALUE, inertial.Inertia.Iyy);
            Assert.AreEqual(InertiaParser.DEFAULT_VALUE, inertial.Inertia.Iyz);
            Assert.AreEqual(InertiaParser.DEFAULT_VALUE, inertial.Inertia.Izz);
        }
    }
}
