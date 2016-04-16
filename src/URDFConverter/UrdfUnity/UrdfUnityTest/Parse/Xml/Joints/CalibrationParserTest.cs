using System;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Parse.Xml.Joints;
using UrdfUnity.Urdf.Models.Joints;

namespace UrdfUnityTest.Parse.Xml.Joints
{
    [TestClass]
    public class CalibrationParserTest
    {
        private readonly CalibrationParser parser = new CalibrationParser();
        private readonly XmlDocument xmlDoc = new XmlDocument();


        [TestMethod]
        public void ParseCalibration()
        {
            double rising = 1;
            double falling = 2;
            string xml = String.Format("<calibration rising='{0}' falling='{1}'/>", rising, falling);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Calibration calibration = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(rising, calibration.Rising);
            Assert.AreEqual(falling, calibration.Falling);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseCalibrationNullNode()
        {
            this.parser.Parse(null);
        }

        [TestMethod]
        public void ParseCalibrationRisingOnly()
        {
            double rising = 1;
            string xml = String.Format("<calibration rising='{0}'/>", rising);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Calibration calibration = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(rising, calibration.Rising);
            Assert.AreEqual(0, calibration.Falling);
        }

        [TestMethod]
        public void ParseCalibrationFallingOnly()
        {
            double falling = 2;
            string xml = String.Format("<calibration falling='{0}'/>", falling);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Calibration calibration = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(0, calibration.Rising);
            Assert.AreEqual(falling, calibration.Falling);
        }

        [TestMethod]
        public void ParseCalibrationMalformed()
        {
            string xml = String.Format("<calibration/>");

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Calibration calibration = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(0, calibration.Rising);
            Assert.AreEqual(0, calibration.Falling);
        }
    }
}
