using System;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Parse.Xml.Links.Geometries;
using UrdfUnity.Urdf.Models.Links.Geometries;

namespace UrdfUnityTest.Parse.Xml.Links.Geometries
{
    [TestClass]
    public class BoxParserTest
    {
        private static readonly string FORMAT_STRING = "<box size='{0} {1} {2}'/>";

        private readonly BoxParser parser = new BoxParser();
        private readonly XmlDocument xmlDoc = new XmlDocument();


        [TestMethod]
        public void ParseBox()
        {
            double l = 1;
            double w = 2;
            double h = 3;
            string xml = String.Format(FORMAT_STRING, l, w, h);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Box box = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(l, box.Size.Length);
            Assert.AreEqual(w, box.Size.Width);
            Assert.AreEqual(h, box.Size.Height);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseBoxNullNode()
        {
            this.parser.Parse(null);
        }
        
        [TestMethod]
        public void ParseBoxMalformed()
        {
            string xml = "<box></box>";

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Box box = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(0, box.Size.Length);
            Assert.AreEqual(0, box.Size.Width);
            Assert.AreEqual(0, box.Size.Height);
        }
    }
}
