using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models;

namespace UrdfUnityTest.Urdf.Models
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
        public void SetProperties()
        {
            int x = 1;
            int y = 2;
            int z = 3;
            XyzAttribute xyz = new XyzAttribute();

            Assert.AreNotEqual(xyz.X, x);
            Assert.AreNotEqual(xyz.Y, y);
            Assert.AreNotEqual(xyz.Z, z);

            xyz.X = x;
            xyz.Y = y;
            xyz.Z = z;

            Assert.AreEqual(xyz.X, x);
            Assert.AreEqual(xyz.Y, y);
            Assert.AreEqual(xyz.Z, z);
        }
    }
}
