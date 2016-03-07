using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models.LinkElements.GeometryElements;

namespace UrdfUnityTest.Urdf.Models.LinkElements.GeometryElements
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
