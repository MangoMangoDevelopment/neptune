using System;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Parse.Xml.LinkElements.VisualElements;
using UrdfUnity.Urdf.Models.LinkElements.VisualElements;

namespace UrdfUnityTest.Parse.Xml.LinkElements.VisualElements
{
    [TestClass]
    public class ColorParserTest
    {
        private readonly ColorParser parser = new ColorParser();
        private readonly XmlDocument xmlDoc = new XmlDocument();

        [TestMethod]
        public void ParseColourRgb()
        {
            int r = 1;
            int g = 2;
            int b = 3;
            string xml = String.Format("<color rgb='{0} {1} {2}'/>", r, g, b);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Color color = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(r, color.Rgb.R);
            Assert.AreEqual(g, color.Rgb.G);
            Assert.AreEqual(b, color.Rgb.B);
            Assert.AreEqual(1d, color.Alpha);
        }

        [TestMethod]
        public void ParseColourRgbAlpha()
        {
            int r = 1;
            int g = 2;
            int b = 3;
            double alpha = 0d;
            string xml = String.Format("<color rgb='{0} {1} {2}' alpha='{3}'/>", r, g, b, alpha);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Color color = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(r, color.Rgb.R);
            Assert.AreEqual(g, color.Rgb.G);
            Assert.AreEqual(b, color.Rgb.B);
            Assert.AreEqual(alpha, color.Alpha);
        }

        [TestMethod]
        public void ParseColourRgba()
        {
            double r = 0.25;
            double g = 0.5;
            double b = 0.75;
            double alpha = 0d;
            string xml = String.Format("<color rgba='{0} {1} {2} {3}'/>", r, g, b, alpha);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Color color = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual((int)(r * 255), color.Rgb.R);
            Assert.AreEqual((int)(g * 255), color.Rgb.G);
            Assert.AreEqual((int)(b * 255), color.Rgb.B);
            Assert.AreEqual(alpha, color.Alpha);
        }

        [TestMethod]
        public void ParseColourMalformedRgb()
        {
            string xml = "<color rgb='255 255'/>"; // Missing value

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Color color = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(0, color.Rgb.R);
            Assert.AreEqual(0, color.Rgb.G);
            Assert.AreEqual(0, color.Rgb.B);
            Assert.AreEqual(1d, color.Alpha);
        }

        [TestMethod]
        public void ParseColourMalformedAlpha()
        {
            int r = 1;
            int g = 2;
            int b = 3;
            string xml = String.Format("<color rgb='{0} {1} {2}' alpha='no alpha'/>", r, g, b);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Color color = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(r, color.Rgb.R);
            Assert.AreEqual(g, color.Rgb.G);
            Assert.AreEqual(b, color.Rgb.B);
            Assert.AreEqual(1d, color.Alpha);
        }

        [TestMethod]
        public void ParseColourMalformedRgba()
        {
            double r = 0.25;
            double g = 0.5;
            double b = 0.75;
            string xml = String.Format("<color rgba='{0} {1} {2}'/>", r, g, b); // Missing value

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Color color = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(0, color.Rgb.R);
            Assert.AreEqual(0, color.Rgb.G);
            Assert.AreEqual(0, color.Rgb.B);
            Assert.AreEqual(1, color.Alpha);
        }

        [TestMethod]
        public void ParseColourNoAttributes()
        {
            string xml = "<color/>";

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Color color = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(0, color.Rgb.R);
            Assert.AreEqual(0, color.Rgb.G);
            Assert.AreEqual(0, color.Rgb.B);
            Assert.AreEqual(1, color.Alpha);
        }
    }
}
