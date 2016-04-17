using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models.Links.Geometries;

namespace UrdfUnityTest.Urdf.Models.Links.Geometries
{
    [TestClass]
    public class CylinderTest
    {
        [TestMethod]
        public void ConstructCylinder()
        {
            double radius = 1;
            double length = 2;
            Cylinder cylinder = new Cylinder(radius, length);

            Assert.AreEqual(radius, cylinder.Radius);
            Assert.AreEqual(length, cylinder.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructCylinderInvalidRadius()
        {
            new Cylinder(1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructCylinderInvalidLength()
        {
            new Cylinder(1, 0);
        }

        [TestMethod]
        public void ToStringCylinder()
        {
            Assert.AreEqual("<cylinder radius=\"1\" length=\"1\"/>", new Cylinder(1, 1).ToString());
            Assert.AreEqual("<cylinder radius=\"3.1415\" length=\"1.25\"/>", new Cylinder(3.1415, 1.25).ToString());
        }

        [TestMethod]
        public void EqualsAndHash()
        {
            Cylinder cylinder = new Cylinder(1, 2);
            Cylinder same = new Cylinder(1, 2);
            Cylinder diff = new Cylinder(7, 7);

            Assert.IsTrue(cylinder.Equals(cylinder));
            Assert.IsFalse(cylinder.Equals(null));
            Assert.IsTrue(cylinder.Equals(same));
            Assert.IsTrue(same.Equals(cylinder));
            Assert.IsFalse(cylinder.Equals(diff));
            Assert.AreEqual(cylinder.GetHashCode(), same.GetHashCode());
            Assert.AreNotEqual(cylinder.GetHashCode(), diff.GetHashCode());
        }
    }
}
