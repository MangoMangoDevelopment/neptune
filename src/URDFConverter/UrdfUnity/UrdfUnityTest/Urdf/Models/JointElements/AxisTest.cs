using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models;
using UrdfUnity.Urdf.Models.JointElements;

namespace UrdfUnityTest.Urdf.Models.JointElements
{
    [TestClass]
    public class AxisTest
    {
        [TestMethod]
        public void ConstructAxis()
        {
            double x = 1;
            double y = 2;
            double z = 3;
            XyzAttribute xyz = new XyzAttribute(x, y, z);
            Axis axis = new Axis(xyz);

            Assert.AreEqual(xyz, axis.Xyz);
            Assert.AreEqual(x, axis.Xyz.X);
            Assert.AreEqual(y, axis.Xyz.Y);
            Assert.AreEqual(z, axis.Xyz.Z);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructAxisNullXyz()
        {
            Axis axis = new Axis(null);
        }

        [TestMethod]
        public void EqualsAndHash()
        {
            Axis axis = new Axis(new XyzAttribute(1, 2, 3));
            Axis same = new Axis(new XyzAttribute(1, 2, 3));
            Axis diff = new Axis(new XyzAttribute(4, 5, 6));

            Assert.IsTrue(axis.Equals(axis));
            Assert.IsFalse(axis.Equals(null));
            Assert.IsTrue(axis.Equals(same));
            Assert.IsTrue(same.Equals(axis));
            Assert.IsFalse(axis.Equals(diff));
            Assert.AreEqual(axis.GetHashCode(), same.GetHashCode());
            Assert.AreNotEqual(axis.GetHashCode(), diff.GetHashCode());
        }
    }
}
