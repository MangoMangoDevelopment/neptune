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
    }
}
