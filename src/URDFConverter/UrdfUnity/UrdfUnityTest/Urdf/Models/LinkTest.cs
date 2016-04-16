using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models;
using UrdfUnity.Urdf.Models.Attributes;
using UrdfUnity.Urdf.Models.Links;
using UrdfUnity.Urdf.Models.Links.Geometries;
using UrdfUnity.Urdf.Models.Links.Inertials;

namespace UrdfUnityTest.Urdf.Models
{[TestClass]
    public class LinkTest
    {
        [TestMethod]
        public void DefaultName()
        {
            Assert.AreEqual("missing_name", Link.DEFAULT_NAME);
        }

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
            Assert.AreEqual(0, link.Visual.Count);
            Assert.AreEqual(collisions, link.Collision);
        }

        [TestMethod]
        public void ConstructLinkNameOnly()
        {
            string name = "linkName";
            Link link = new Link.Builder(name).Build();

            Assert.AreEqual(name, link.Name);
            Assert.IsNull(link.Inertial);
            Assert.AreEqual(0, link.Visual.Count);
            Assert.AreEqual(0, link.Collision.Count);
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

        [TestMethod]
        public void EqualsAndHash()
        {
            Inertial inertial = new Inertial(new Mass(1), new Inertia(1, 1, 1, 1, 1, 1));
            Visual visual = new Visual.Builder(new Geometry(new Sphere(1))).Build();
            List<Visual> visualList = new List<Visual>();
            List<Visual> diffVisualList = new List<Visual>();
            List<Collision> collisionList = new List<Collision>();

            visualList.Add(visual);
            diffVisualList.Add(visual);
            diffVisualList.Add(new Visual.Builder(new Geometry(new Box(new SizeAttribute(1, 2, 3)))).Build());

            Link link = new Link.Builder("link").SetInertial(inertial).SetVisual(visual).SetCollision(collisionList).Build();
            Link same1 = new Link.Builder("link").SetInertial(inertial).SetVisual(visual).SetCollision(collisionList).Build();
            Link same2 = new Link.Builder("link").SetInertial(inertial).SetVisual(visualList).SetCollision(collisionList).Build();
            Link same3 = new Link.Builder("link").SetInertial(inertial).SetVisual(visualList).Build();
            Link diff1 = new Link.Builder("different_link").SetInertial(inertial).SetVisual(visual).SetCollision(collisionList).Build();
            Link diff2 = new Link.Builder("link").SetVisual(visual).SetCollision(collisionList).Build();
            Link diff3 = new Link.Builder("link").SetInertial(inertial).SetCollision(collisionList).Build();
            Link diff4 = new Link.Builder("link").SetInertial(inertial).SetVisual(diffVisualList).SetCollision(collisionList).Build();
            Link diff5 = new Link.Builder("link").Build();


            Assert.IsTrue(link.Equals(link));
            Assert.IsFalse(link.Equals(null));
            Assert.IsTrue(link.Equals(same1));
            Assert.IsTrue(link.Equals(same2));
            Assert.IsTrue(link.Equals(same3));
            Assert.IsTrue(same1.Equals(link));
            Assert.IsTrue(same2.Equals(link));
            Assert.IsTrue(same3.Equals(link));
            Assert.IsFalse(link.Equals(diff1));
            Assert.IsFalse(link.Equals(diff2));
            Assert.IsFalse(link.Equals(diff3));
            Assert.IsFalse(link.Equals(diff4));
            Assert.IsFalse(link.Equals(diff5));
            Assert.AreEqual(link.GetHashCode(), link.GetHashCode());
            Assert.AreEqual(link.GetHashCode(), same1.GetHashCode());
            Assert.AreEqual(link.GetHashCode(), same2.GetHashCode());
            Assert.AreEqual(link.GetHashCode(), same3.GetHashCode());
            Assert.AreNotEqual(link.GetHashCode(), diff1.GetHashCode());
            Assert.AreNotEqual(link.GetHashCode(), diff2.GetHashCode());
            Assert.AreNotEqual(link.GetHashCode(), diff3.GetHashCode());
            Assert.AreNotEqual(link.GetHashCode(), diff4.GetHashCode());
            Assert.AreNotEqual(link.GetHashCode(), diff5.GetHashCode());
        }
    }
}
