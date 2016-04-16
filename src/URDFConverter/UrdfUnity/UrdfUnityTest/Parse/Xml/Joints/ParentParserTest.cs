using System;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Parse.Xml.Joints;
using UrdfUnity.Urdf.Models;

namespace UrdfUnityTest.Parse.Xml.Joints
{
    [TestClass]
    public class ParentParserTest
    {
        private readonly ParentParser parser = new ParentParser();
        private readonly XmlDocument xmlDoc = new XmlDocument();


        [TestMethod]
        public void ParserParent()
        {
            string link = "parent";
            string xml = String.Format("<parent link='{0}'/>", link);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            String parent = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(link, parent);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseParentNullNode()
        {
            this.parser.Parse(null);
        }

        [TestMethod]
        public void ParseChildMalformed()
        {
            string xml = "<parent></parent>";

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            String child = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(Link.DEFAULT_NAME, child);
        }
    }
}
