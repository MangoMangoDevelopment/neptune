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
            string name = "name";
            Inertial inertial = new Inertial(new Mass(1), new Inertia(1, 1, 1, 1, 1, 1));
            List<Visual> visuals = new List<Visual>();
            visuals.Add(new Visual(new Geometry(new Sphere(1))));
            List<Collision> collisions = new List<Collision>();
            Link link = new Link(name, inertial, visuals, collisions);

            Assert.AreEqual(name, link.Name);
            Assert.AreEqual(inertial, link.Inertial);
            Assert.AreEqual(visuals, link.Visual);
            Assert.AreEqual(visuals[0], link.Visual[0]);
            Assert.AreEqual(collisions, link.Collision);
        }

        [TestMethod]
        public void ConstructLinkWithNulls()
        {
            string name = "name";
            Link link = new Link(name, null, null, null);

            Assert.AreEqual(name, link.Name);
            Assert.IsNull(link.Inertial);
            Assert.IsNull(link.Visual);
            Assert.IsNull(link.Collision);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructLinkNoName()
        {
            Link link = new Link(null, null, null, null);
        }
    }
}
