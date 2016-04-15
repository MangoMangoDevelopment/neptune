using System;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models.Attributes;
using UrdfUnity.Parse.Xml.LinkElements.GeometryElements;
using UrdfUnity.Urdf.Models.LinkElements.GeometryElements;

namespace UrdfUnityTest.Parse.Xml.LinkElements.GeometryElements
{
    [TestClass]
    public class MeshParserTest
    {
        private readonly MeshParser parser = new MeshParser();
        private readonly XmlDocument xmlDoc = new XmlDocument();


        [TestMethod]
        public void ParseMesh()
        {
            string fileName = "fileName";
            ScaleAttribute scale = new ScaleAttribute(1, 2, 3);
            SizeAttribute size = new SizeAttribute(4, 5, 6);
            string xml = String.Format("<mesh filename='{0}' scale='{1} {2} {3}' size='{4} {5} {6}'/>",
                fileName, scale.X, scale.Y, scale.Z, size.Length, size.Width, size.Height);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Mesh mesh = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(fileName, mesh.FileName);
            Assert.AreEqual(scale, mesh.Scale);
            Assert.AreEqual(size, mesh.Size);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseMeshNullNode()
        {
            this.parser.Parse(null);
        }

        [TestMethod]
        public void ParseMeshNoScale()
        {
            string fileName = "fileName";
            SizeAttribute size = new SizeAttribute(4, 5, 6);
            string xml = String.Format("<mesh filename='{0}' size='{1} {2} {3}'/>",
                fileName, size.Length, size.Width, size.Height);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Mesh mesh = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(fileName, mesh.FileName);
            Assert.AreEqual(new ScaleAttribute(1, 1, 1), mesh.Scale);
            Assert.AreEqual(size, mesh.Size);
        }

        [TestMethod]
        public void ParseMeshNoSize()
        {
            string fileName = "fileName";
            ScaleAttribute scale = new ScaleAttribute(1, 2, 3);
            string xml = String.Format("<mesh filename='{0}' scale='{1} {2} {3}'/>",
                fileName, scale.X, scale.Y, scale.Z);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Mesh mesh = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(fileName, mesh.FileName);
            Assert.AreEqual(scale, mesh.Scale);
            Assert.AreEqual(null, mesh.Size);
        }

        [TestMethod]
        public void ParseMeshNoFileName()
        {
            ScaleAttribute scale = new ScaleAttribute(1, 2, 3);
            SizeAttribute size = new SizeAttribute(4, 5, 6);
            string xml = String.Format("<mesh scale='{0} {1} {2}' size='{3} {4} {5}'/>",
                scale.X, scale.Y, scale.Z, size.Length, size.Width, size.Height);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Mesh mesh = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(Mesh.DEFAULT_FILE_NAME, mesh.FileName);
            Assert.AreEqual(scale, mesh.Scale);
            Assert.AreEqual(size, mesh.Size);
        }
    }
}
