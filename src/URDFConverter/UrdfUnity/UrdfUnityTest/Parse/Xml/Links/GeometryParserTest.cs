using System;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Parse.Xml.Links;
using UrdfUnity.Urdf.Models.Attributes;
using UrdfUnity.Urdf.Models.Links;
using UrdfUnity.Urdf.Models.Links.Geometries;

namespace UrdfUnityTest.Parse.Xml.Links
{
    [TestClass]
    public class GeometryParserTest
    {
        private readonly GeometryParser parser = new GeometryParser();
        private readonly XmlDocument xmlDoc = new XmlDocument();


        [TestMethod]
        public void ParseGeometryBox()
        {
            Box box = new Box(new SizeAttribute(1, 2, 3));
            string xml = String.Format("<geometry><box size='{0} {1} {2}'/></geometry>", box.Size.Length, box.Size.Width, box.Size.Height);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Geometry geometry = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(Geometry.Shapes.Box, geometry.Shape);
            Assert.AreEqual(box, geometry.Box);
            Assert.IsNull(geometry.Cylinder);
            Assert.IsNull(geometry.Sphere);
            Assert.IsNull(geometry.Mesh);
        }

        [TestMethod]
        public void ParseGeometryCylinder()
        {
            Cylinder cylinder = new Cylinder(1, 2);
            string xml = String.Format("<geometry><cylinder radius='{0}' length='{1}'/></geometry>", cylinder.Radius, cylinder.Length);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Geometry geometry = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(Geometry.Shapes.Cylinder, geometry.Shape);
            Assert.AreEqual(cylinder, geometry.Cylinder);
            Assert.IsNull(geometry.Box);
            Assert.IsNull(geometry.Sphere);
            Assert.IsNull(geometry.Mesh);
        }

        [TestMethod]
        public void ParseGeometrySphere()
        {
            Sphere sphere = new Sphere(1);
            string xml = String.Format("<geometry><sphere radius='{0}'/></geometry>", sphere.Radius);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Geometry geometry = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(Geometry.Shapes.Sphere, geometry.Shape);
            Assert.AreEqual(sphere, geometry.Sphere);
            Assert.IsNull(geometry.Box);
            Assert.IsNull(geometry.Cylinder);
            Assert.IsNull(geometry.Mesh);
        }

        [TestMethod]
        public void ParseGeometryMesh()
        {
            Mesh mesh = new Mesh.Builder("filename").Build();
            string xml = String.Format("<geometry><mesh filename='{0}'/></geometry>", mesh.FileName);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Geometry geometry = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(Geometry.Shapes.Mesh, geometry.Shape);
            Assert.AreEqual(mesh, geometry.Mesh);
            Assert.IsNull(geometry.Box);
            Assert.IsNull(geometry.Cylinder);
            Assert.IsNull(geometry.Sphere);
        }

        [TestMethod]
        public void ParseGeometryMalformed()
        {
            string xml = "<geometry></geometry>"; // No shape

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Geometry geometry = this.parser.Parse(this.xmlDoc.DocumentElement);
            
            Assert.AreEqual(GeometryParser.DEFAULT_GEOMETRY, geometry);
        }
    }
}
