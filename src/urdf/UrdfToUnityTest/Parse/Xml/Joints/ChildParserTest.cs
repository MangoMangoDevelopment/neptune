using System;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfToUnity.Parse.Xml.Joints;
using UrdfToUnity.Urdf.Models;

namespace UrdfToUnityTest.Parse.Xml.Joints
{
    [TestClass]
    public class ChildParserTest
    {
        private readonly ChildParser parser = new ChildParser();
        private readonly XmlDocument xmlDoc = new XmlDocument();


        [TestMethod]
        public void ParseChild()
        {
            string link = "parent";
            string xml = String.Format("<child link='{0}'/>", link);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            String child = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(link, child);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseChildNullNode()
        {
            this.parser.Parse(null);
        }

        [TestMethod]
        public void ParseChildMalformed()
        {
            string xml = "<child></child>";

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            String child = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(Link.DEFAULT_NAME, child);
        }
    }
}
