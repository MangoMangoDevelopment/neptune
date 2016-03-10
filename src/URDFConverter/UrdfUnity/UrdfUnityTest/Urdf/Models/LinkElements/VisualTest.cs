using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models;
using UrdfUnity.Urdf.Models.LinkElements;
using UrdfUnity.Urdf.Models.LinkElements.GeometryElements;
using UrdfUnity.Urdf.Models.LinkElements.VisualElements;

namespace UrdfUnityTest.Urdf.Models.LinkElements
{
    [TestClass]
    public class VisualTest
    {
        [TestMethod]
        public void ConstructVisual()
        {
            Origin origin = new Origin();
            Geometry geometry = new Geometry(new Sphere(1));
            Material material = new Material("name");
            Visual visual = new Visual.Builder(geometry).SetOrigin(origin).SetMaterial(material).Build();

            Assert.AreEqual(origin, visual.Origin);
            Assert.AreEqual(geometry, visual.Geometry);
            Assert.AreEqual(material, visual.Material);
        }

        [TestMethod]
        public void ConstructVisualOnlyGeometry()
        {
            Geometry geometry = new Geometry(new Sphere(1));
            Visual visual = new Visual.Builder(geometry).Build();

            Assert.AreEqual(geometry, visual.Geometry);
            Assert.IsNull(visual.Material);
            Assert.AreEqual(new Origin(), visual.Origin);
        }

        [TestMethod]
        public void ConstructVisualWithOrigin()
        {
            Origin origin = new Origin();
            Geometry geometry = new Geometry(new Sphere(1));
            Visual visual = new Visual.Builder(geometry).SetOrigin(origin).Build();

            Assert.AreEqual(origin, visual.Origin);
            Assert.AreEqual(geometry, visual.Geometry);
            Assert.IsNull(visual.Material);
        }

        [TestMethod]
        public void ConstructVisualWithMaterial()
        {
            Geometry geometry = new Geometry(new Sphere(1));
            Material material = new Material("name");
            Visual visual = new Visual.Builder(geometry).SetMaterial(material).Build();

            Assert.AreEqual(new Origin(), visual.Origin);
            Assert.AreEqual(geometry, visual.Geometry);
            Assert.AreEqual(material, visual.Material);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructVisualNullGeometry()
        {
            Visual visual = new Visual.Builder(null).Build();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructVisualNullOrigin()
        {
            Visual visual = new Visual.Builder(new Geometry(new Sphere(1))).SetOrigin(null).Build();
        }

        [TestMethod]
        public void EqualsAndHash()
        {
            Visual visual = new Visual.Builder(new Geometry(new Sphere(1))).Build();
            Visual same = new Visual.Builder(new Geometry(new Sphere(1))).Build();
            Visual diff = new Visual.Builder(new Geometry(new Sphere(2))).Build();

            Assert.IsTrue(visual.Equals(visual));
            Assert.IsFalse(visual.Equals(null));
            Assert.IsTrue(visual.Equals(same));
            Assert.IsTrue(same.Equals(visual));
            Assert.IsFalse(visual.Equals(diff));
            Assert.AreEqual(visual.GetHashCode(), same.GetHashCode());
            Assert.AreNotEqual(visual.GetHashCode(), diff.GetHashCode());
        }
    }
}
