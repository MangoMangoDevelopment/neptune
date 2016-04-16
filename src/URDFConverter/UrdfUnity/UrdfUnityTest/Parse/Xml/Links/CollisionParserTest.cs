using System;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Parse.Xml.Links;
using UrdfUnity.Urdf.Models;
using UrdfUnity.Urdf.Models.Attributes;
using UrdfUnity.Urdf.Models.Links;
using UrdfUnity.Urdf.Models.Links.Geometries;

namespace UrdfUnityTest.Parse.Xml.Links
{
    [TestClass]
    public class CollisionParserTest
    {
        private readonly CollisionParser parser = new CollisionParser();
        private readonly XmlDocument xmlDoc = new XmlDocument();


        [TestMethod]
        public void ParseCollision()
        {
            string name = "name";
            Origin origin = new Origin();
            Geometry geometry = new Geometry(new Box(new SizeAttribute(1, 2, 3)));
            string format = @"<collision name='{0}'>
                <origin xyz='{1} {2} {3}' rpy='{4} {5} {6}'/>
                <geometry><box size='{7} {8} {9}'/></geometry>
                </collision>";
            string xml = String.Format(format, name, origin.Xyz.X, origin.Xyz.Y, origin.Xyz.Z, 
                origin.Rpy.R, origin.Rpy.P, origin.Rpy.Y, 
                geometry.Box.Size.Length, geometry.Box.Size.Width, geometry.Box.Size.Height);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Collision collision = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(name, collision.Name);
            Assert.AreEqual(origin, collision.Origin);
            Assert.AreEqual(geometry, collision.Geometry);
        }

        [TestMethod]
        public void ParseCollisionNoNameOrOrigin()
        {
            Geometry geometry = new Geometry(new Box(new SizeAttribute(1, 2, 3)));
            string format = @"<collision>
                <geometry><box size='{0} {1} {2}'/></geometry>
                </collision>";
            string xml = String.Format(format, geometry.Box.Size.Length, geometry.Box.Size.Width, geometry.Box.Size.Height);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Collision collision = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(null, collision.Name);
            Assert.AreEqual(new Origin(), collision.Origin);
            Assert.AreEqual(geometry, collision.Geometry);
        }

        [TestMethod]
        public void ParseCollisionMalformed()
        {
            string xml = "<collision></collision>";
            
            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Collision collision = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(null, collision.Name);
            Assert.AreEqual(new Origin(), collision.Origin);
            Assert.AreEqual(GeometryParser.DEFAULT_GEOMETRY, collision.Geometry);
        }

        [TestMethod]
        public void ParseCollisionMalformedGeometry()
        {
            string xml = "<collision><geometry/></collision>";

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Collision collision = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(null, collision.Name);
            Assert.AreEqual(new Origin(), collision.Origin);
            Assert.AreEqual(GeometryParser.DEFAULT_GEOMETRY, collision.Geometry);
        }
    }
}
