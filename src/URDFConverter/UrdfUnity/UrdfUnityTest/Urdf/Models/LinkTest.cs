using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models;
using UrdfUnity.Urdf.Models.LinkElements;
using UrdfUnity.Urdf.Models.LinkElements.GeometryElements;
using UrdfUnity.Urdf.Models.LinkElements.InertialElements;

namespace UrdfUnityTest.Urdf.Models
{
    [TestClass]
    public class LinkTest
    {
        [TestMethod]
        public void ConstructLink()
        {
            string name = "linkName";
            Inertial inertial = new Inertial(new Mass(1), new Inertia(1, 1, 1, 1, 1, 1));
            List<Visual> visuals = new List<Visual>();
            visuals.Add(new Visual.Builder(new Geometry(new Sphere(1))).Build());
            List<Collision> collisions = new List<Collision>();

            Link.Builder builder = new Link.Builder(name);
            builder.SetInertial(inertial);
            builder.SetVisual(visuals);
            builder.SetCollision(collisions);
            Link link = builder.Build();

            Assert.AreEqual(name, link.Name);
            Assert.AreEqual(inertial, link.Inertial);
            Assert.AreEqual(visuals, link.Visual);
            Assert.AreEqual(visuals[0], link.Visual[0]);
            Assert.AreEqual(collisions, link.Collision);
        }

        [TestMethod]
        public void ConstructLinkChainBuilderSetters()
        {
            string name = "linkName";
            Inertial inertial = new Inertial(new Mass(1), new Inertia(1, 1, 1, 1, 1, 1));
            Visual visual = new Visual.Builder(new Geometry(new Sphere(1))).Build();
            List<Collision> collisions = new List<Collision>();
            Link link = new Link.Builder(name).SetInertial(inertial).SetVisual(visual).SetCollision(collisions).Build();

            Assert.AreEqual(name, link.Name);
            Assert.AreEqual(inertial, link.Inertial);
            Assert.AreEqual(visual, link.Visual[0]);
            Assert.AreEqual(collisions, link.Collision);
        }

        [TestMethod]
        public void ConstructLinkTwoOfThreeBuilderSetters()
        {
            string name = "linkName";
            Inertial inertial = new Inertial(new Mass(1), new Inertia(1, 1, 1, 1, 1, 1));
            List<Collision> collisions = new List<Collision>();
            Link link = new Link.Builder(name).SetInertial(inertial).SetCollision(collisions).Build();

            Assert.AreEqual(name, link.Name);
            Assert.AreEqual(inertial, link.Inertial);
            Assert.IsNull(link.Visual);
            Assert.AreEqual(collisions, link.Collision);
        }

        [TestMethod]
        public void ConstructLinkNameOnly()
        {
            string name = "linkName";
            Link link = new Link.Builder(name).Build();
            
            Assert.AreEqual(name, link.Name);
            Assert.IsNull(link.Inertial);
            Assert.IsNull(link.Visual);
            Assert.IsNull(link.Collision);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructLinkNoName()
        {
            Link link = new Link.Builder("").Build();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BuilderNullInertial()
        {
            Link link = new Link.Builder("link").SetInertial(null).Build();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BuilderNullVisual()
        {
            Visual visual = null;
            Link link = new Link.Builder("link").SetVisual(visual).Build();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BuilderNullVisualList()
        {
            List<Visual> visual = null;
            Link link = new Link.Builder("link").SetVisual(visual).Build();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BuilderNullCollision()
        {
            Collision collision = null;
            Link link = new Link.Builder("link").SetCollision(collision).Build();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BuilderNullCollisionList()
        {
            List<Collision> collision = null;
            Link link = new Link.Builder("link").SetCollision(collision).Build();
        }
    }
}
