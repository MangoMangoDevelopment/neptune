using System;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Parse.Xml.LinkElements;
using UrdfUnity.Urdf.Models;
using UrdfUnity.Urdf.Models.LinkElements;
using UrdfUnity.Urdf.Models.LinkElements.GeometryElements;
using UrdfUnity.Urdf.Models.LinkElements.VisualElements;

namespace UrdfUnityTest.Parse.Xml.LinkElements
{
    [TestClass]
    public class VisualParserTest
    {
        private readonly VisualParser parser = new VisualParser();
        private readonly XmlDocument xmlDoc = new XmlDocument();


        [TestMethod]
        public void ParseVisual()
        {
            string name = "name";
            Geometry geometry = new Geometry(new Box(new SizeAttribute(1, 2, 3)));
            Origin origin = new Origin.Builder().SetXyz(new XyzAttribute(1, 2, 3)).SetRpy(new RpyAttribute(4, 5, 6)).Build();
            Material material = new Material("material", new Color(new RgbAttribute(255, 255, 255)));
            string format = @"<visual name='{0}'>
                    <geometry><box size='{1} {2} {3}'/></geometry>
                    <origin xyz='{4} {5} {6}' rpy='{7} {8} {9}'/>
                    <material name='{10}'><color rgb='{11} {12} {13}'/></material>
                </visual>";
            string xml = String.Format(format, name, geometry.Box.Size.Length, geometry.Box.Size.Width,
                geometry.Box.Size.Height,
                origin.Xyz.X, origin.Xyz.Y, origin.Xyz.Z, origin.Rpy.R, origin.Rpy.P, origin.Rpy.Y,
                material.Name, material.Color.Rgb.R, material.Color.Rgb.G, material.Color.Rgb.B);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Visual visual = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(name, visual.Name);
            Assert.AreEqual(geometry, visual.Geometry);
            Assert.AreEqual(origin, visual.Origin);
            Assert.AreEqual(material, visual.Material);
        }

        [TestMethod]
        public void ParseVisualGeometryOnly()
        {
            Geometry geometry = new Geometry(new Box(new SizeAttribute(1, 2, 3)));
            string format = @"<visual><geometry><box size='{0} {1} {2}'/></geometry></visual>";
            string xml = String.Format(format, geometry.Box.Size.Length, geometry.Box.Size.Width, geometry.Box.Size.Height);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Visual visual = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.IsNull(visual.Name);
            Assert.AreEqual(geometry, visual.Geometry);
            Assert.AreEqual(new Origin(), visual.Origin);
            Assert.IsNull(visual.Material);
        }

        [TestMethod]
        public void ParseVisualNoGeometry()
        {
            string xml = "<visual></visual>";
            
            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Visual visual = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.IsNull(visual.Name);
            Assert.AreEqual(GeometryParser.DEFAULT_GEOMETRY, visual.Geometry);
            Assert.AreEqual(new Origin(), visual.Origin);
            Assert.IsNull(visual.Material);
        }
    }
}
