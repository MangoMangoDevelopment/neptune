using System;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Parse.Xml.LinkElements.VisualElements;
using UrdfUnity.Urdf.Models.LinkElements.VisualElements;

namespace UrdfUnityTest.Parse.Xml.LinkElements.VisualElements
{
    [TestClass]
    public class TextureParserTest
    {
        private readonly TextureParser parser = new TextureParser();
        private readonly XmlDocument xmlDoc = new XmlDocument();

        [TestMethod]
        public void ParseTexture()
        {
            string filename = "filename";
            string xml = String.Format("<texture filename='{0}'/>", filename);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Texture texture = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(filename, texture.FileName);
        }

        [TestMethod]
        public void ParseTextureMalformed()
        {
            string xml = "<texture>filename</texture>";

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Texture texture = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(Texture.DEFAULT_FILE_NAME, texture.FileName);
        }
    }
}
