using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models.Attributes;

namespace UrdfUnityTest.Urdf.Models.Attributes
{
    [TestClass]
    public class XyzAttributeTest
    {
        [TestMethod]
        public void ConstructDefault()
        {
            XyzAttribute xyz = new XyzAttribute();

            Assert.AreEqual(xyz.X, 0);
            Assert.AreEqual(xyz.Y, 0);
            Assert.AreEqual(xyz.Z, 0);
        }

        [TestMethod]
        public void ConstructXyz()
        {
            int x = 1;
            int y = 2;
            int z = 3;
            XyzAttribute xyz = new XyzAttribute(x, y, z);

            Assert.AreEqual(xyz.X, x);
            Assert.AreEqual(xyz.Y, y);
            Assert.AreEqual(xyz.Z, z);
        }

        [TestMethod]
        public void ToStringXyz()
        {
            Assert.AreEqual("0 0 0", new XyzAttribute(0, 0, 0).ToString());
            Assert.AreEqual("0.1 0 1", new XyzAttribute(0.1, 0, 1.0).ToString());
            Assert.AreEqual("3.1415 0 -1.25", new XyzAttribute(3.1415, 0, -1.25).ToString());
        }

        [TestMethod]
        public void EqualsAndHash()
        {
            XyzAttribute xyz = new XyzAttribute(1, 2, 3);
            XyzAttribute same = new XyzAttribute(1, 2, 3);
            XyzAttribute diff = new XyzAttribute(3, 2, 1);

            Assert.IsTrue(xyz.Equals(xyz));
            Assert.IsFalse(xyz.Equals(null));
            Assert.IsTrue(xyz.Equals(same));
            Assert.IsTrue(same.Equals(xyz));
            Assert.IsFalse(xyz.Equals(diff));
            Assert.AreEqual(xyz.GetHashCode(), same.GetHashCode());
            Assert.AreNotEqual(xyz.GetHashCode(), diff.GetHashCode());
        }
    }
}
