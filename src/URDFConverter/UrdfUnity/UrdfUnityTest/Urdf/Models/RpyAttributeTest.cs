using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models;

namespace UrdfUnityTest.Urdf.Models
{
    [TestClass]
    public class RpyAttributeTest
    {
        [TestMethod]
        public void ConstructDefault()
        {
            RpyAttribute rpy = new RpyAttribute();

            Assert.AreEqual(rpy.R, 0);
            Assert.AreEqual(rpy.P, 0);
            Assert.AreEqual(rpy.Y, 0);
        }

        [TestMethod]
        public void ConstructRpy()
        {
            int r = 1;
            int p = 2;
            int y = 3;
            RpyAttribute rpy = new RpyAttribute(r, p, y);

            Assert.AreEqual(rpy.R, r);
            Assert.AreEqual(rpy.P, p);
            Assert.AreEqual(rpy.Y, y);
        }

        [TestMethod]
        public void EqualsAndHash()
        {
            RpyAttribute rpy = new RpyAttribute(1, 2, 3);
            RpyAttribute same = new RpyAttribute(1, 2, 3);
            RpyAttribute diff = new RpyAttribute(3, 2, 1);

            Assert.IsTrue(rpy.Equals(rpy));
            Assert.IsFalse(rpy.Equals(null));
            Assert.IsTrue(rpy.Equals(same));
            Assert.IsTrue(same.Equals(rpy));
            Assert.IsFalse(rpy.Equals(diff));
            Assert.AreEqual(rpy.GetHashCode(), same.GetHashCode());
            Assert.AreNotEqual(rpy.GetHashCode(), diff.GetHashCode());
        }
    }
}
