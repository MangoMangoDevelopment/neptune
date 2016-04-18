using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfToUnity.Urdf.Models.Links.Geometries;

namespace UrdfToUnityTest.Urdf.Models.Links.Geometries
{
    [TestClass]
    public class SphereTest
    {
        [TestMethod]
        public void ConstructSphere()
        {
            double radius = 1;
            Sphere sphere = new Sphere(radius);

            Assert.AreEqual(radius, sphere.Radius);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructSphereInvalidRadius()
        {
            new Sphere(0);
        }

        [TestMethod]
        public void ToStringSphere()
        {
            Assert.AreEqual("<sphere radius=\"1\"/>", new Sphere(1).ToString());
            Assert.AreEqual("<sphere radius=\"3.1415\"/>", new Sphere(3.1415).ToString());
        }

        [TestMethod]
        public void EqualsAndHash()
        {
            Sphere sphere = new Sphere(1);
            Sphere same = new Sphere(1);
            Sphere diff = new Sphere(7);

            Assert.IsTrue(sphere.Equals(sphere));
            Assert.IsFalse(sphere.Equals(null));
            Assert.IsTrue(sphere.Equals(same));
            Assert.IsTrue(same.Equals(sphere));
            Assert.IsFalse(sphere.Equals(diff));
            Assert.AreEqual(sphere.GetHashCode(), same.GetHashCode());
            Assert.AreNotEqual(sphere.GetHashCode(), diff.GetHashCode());
        }
    }
}
