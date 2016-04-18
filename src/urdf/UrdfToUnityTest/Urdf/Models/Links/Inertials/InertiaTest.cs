using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfToUnity.Urdf.Models.Links.Inertials;

namespace UrdfToUnityTest.Urdf.Models.Links.Inertials
{
    [TestClass]
    public class InertiaTest
    {
        [TestMethod]
        public void ConstructInertia()
        {
            double ixx = 1;
            double ixy = 2;
            double ixz = 3;
            double iyy = 4;
            double iyz = 5;
            double izz = 6;
            Inertia inertia = new Inertia(ixx, ixy, ixz, iyy, iyz, izz);

            Assert.AreEqual(ixx, inertia.Ixx);
            Assert.AreEqual(ixy, inertia.Ixy);
            Assert.AreEqual(ixz, inertia.Ixz);
            Assert.AreEqual(iyy, inertia.Iyy);
            Assert.AreEqual(iyz, inertia.Iyz);
            Assert.AreEqual(izz, inertia.Izz);
        }

        [TestMethod]
        public void ToStringInertia()
        {
            Assert.AreEqual("<inertia ixx=\"1\" ixy=\"1\" ixz=\"1\" iyy=\"1\" iyz=\"1\" izz=\"1\"/>", new Inertia(1, 1, 1, 1, 1, 1).ToString());
            Assert.AreEqual("<inertia ixx=\"3.1415\" ixy=\"0\" ixz=\"-1.25\" iyy=\"1\" iyz=\"1\" izz=\"1\"/>", new Inertia(3.1415, 0, -1.25, 1, 1, 1).ToString());
        }

        [TestMethod]
        public void EqualsAndHash()
        {
            Inertia inertia = new Inertia(1, 2, 3, 4, 5, 6);
            Inertia same = new Inertia(1, 2, 3, 4, 5, 6);
            Inertia diff = new Inertia(7, 7, 7, 7, 7, 7);

            Assert.IsTrue(inertia.Equals(inertia));
            Assert.IsFalse(inertia.Equals(null));
            Assert.IsTrue(inertia.Equals(same));
            Assert.IsTrue(same.Equals(inertia));
            Assert.IsFalse(inertia.Equals(diff));
            Assert.AreEqual(inertia.GetHashCode(), same.GetHashCode());
            Assert.AreNotEqual(inertia.GetHashCode(), diff.GetHashCode());
        }
    }
}
