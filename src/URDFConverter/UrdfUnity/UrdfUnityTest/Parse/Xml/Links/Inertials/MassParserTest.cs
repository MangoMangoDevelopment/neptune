using System;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Parse.Xml.Links.Inertials;
using UrdfUnity.Urdf.Models.Links.Inertials;

namespace UrdfUnityTest.Parse.Xml.Links.Inertials
{
    [TestClass]
    public class MassParserTest
    {
        private static readonly string FORMAT_STRING = "<mass value='{0}'/>";

        private readonly MassParser parser = new MassParser();
        private readonly XmlDocument xmlDoc = new XmlDocument();

        [TestMethod]
        public void ParseMass()
        {
            double[] testValues = { 0, 25, 123.456 };

            foreach (double value in testValues)
            {
                string xml = String.Format(FORMAT_STRING, value);
                this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
                Mass mass = this.parser.Parse(xmlDoc.DocumentElement);

                Assert.AreEqual(value, mass.Value);
            }

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseMassNullNode()
        {
            this.parser.Parse(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseNegativeMass()
        {
            string xml = String.Format(FORMAT_STRING, -1);
            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Mass mass = this.parser.Parse(xmlDoc.DocumentElement);
        }

        [TestMethod]
        public void ParseMassNoValue()
        {
            string xml = String.Format(FORMAT_STRING, String.Empty);
            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Mass mass = this.parser.Parse(xmlDoc.DocumentElement);

            Assert.AreEqual(0d, mass.Value);
        }
    }
}
