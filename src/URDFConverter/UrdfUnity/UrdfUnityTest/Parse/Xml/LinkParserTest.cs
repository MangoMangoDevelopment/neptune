using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Parse.Xml;
using UrdfUnity.Urdf.Models;
using UrdfUnity.Urdf.Models.Attributes;
using UrdfUnity.Urdf.Models.LinkElements;
using UrdfUnity.Urdf.Models.LinkElements.GeometryElements;
using UrdfUnity.Urdf.Models.LinkElements.InertialElements;
using UrdfUnity.Urdf.Models.LinkElements.VisualElements;

namespace UrdfUnityTest.Parse.Xml
{
    [TestClass]
    public class LinkParserTest
    {
        private readonly LinkParser parser = new LinkParser(new Dictionary<string, Material>());
        private readonly XmlDocument xmlDoc = new XmlDocument();


        [TestMethod]
        public void ParseLink()
        {
            string name = "name";
            Geometry geometry1 = new Geometry(new Sphere(1));
            Geometry geometry2 = new Geometry(new Box(new SizeAttribute(1, 2, 3)));
            Visual visual1 = new Visual.Builder(geometry1).Build();
            Visual visual2 = new Visual.Builder(geometry2).Build();
            Collision collision = new Collision.Builder().SetGeometry(geometry1).Build();
            Inertial inertial = new Inertial(new Mass(1), new Inertia(1, 2, 3, 4, 5, 6));
            string format = @"<link name='{0}'>
                    <visual><geometry><sphere radius='{1}'/></geometry></visual>
                    <visual><geometry><box size='{2} {3} {4}'/></geometry></visual>
                    <collision><geometry><sphere radius='{5}'/></geometry></collision>
                    <inertial><mass value='{6}'/><inertia ixx='{7}' ixy='{8}' ixz='{9}' iyy='{10}' iyz='{11}' izz='{12}'/></inertial>
                </link>";
            string xml = String.Format(format, name, visual1.Geometry.Sphere.Radius, 
                visual2.Geometry.Box.Size.Length, visual2.Geometry.Box.Size.Width, visual2.Geometry.Box.Size.Height,
                collision.Geometry.Sphere.Radius, inertial.Mass.Value, inertial.Inertia.Ixx, inertial.Inertia.Ixy, 
                inertial.Inertia.Ixz, inertial.Inertia.Iyy, inertial.Inertia.Iyz, inertial.Inertia.Izz);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Link link = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(name, link.Name);
            Assert.AreEqual(2, link.Visual.Count);
            Assert.AreEqual(visual1, link.Visual[0]);
            Assert.AreEqual(visual2, link.Visual[1]);
            Assert.AreEqual(1, link.Collision.Count);
            Assert.AreEqual(collision, link.Collision[0]);
            Assert.AreEqual(inertial, link.Inertial);
        }

        [TestMethod]
        public void ParseLinkVisualOnly()
        {
            string name = "name";
            Geometry geometry = new Geometry(new Sphere(1));
            Visual visual = new Visual.Builder(geometry).Build();
            string format = @"<link name='{0}'>
                    <visual><geometry><sphere radius='{1}'/></geometry></visual>
                </link>";
            string xml = String.Format(format, name, visual.Geometry.Sphere.Radius);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Link link = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(name, link.Name);
            Assert.AreEqual(1, link.Visual.Count);
            Assert.AreEqual(visual, link.Visual[0]);
            Assert.AreEqual(0, link.Collision.Count);
            Assert.IsNull(link.Inertial);
        }

        [TestMethod]
        public void ParseLinkNoNameOrSubElements()
        {
            string xml = "<link></link>";

            this.xmlDoc.Load(XmlReader.Create(new StringReader(xml)));
            Link link = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual(Link.DEFAULT_NAME, link.Name);
            Assert.AreEqual(0, link.Visual.Count);
            Assert.AreEqual(0, link.Collision.Count);
            Assert.IsNull(link.Inertial);
        }
    }
}
