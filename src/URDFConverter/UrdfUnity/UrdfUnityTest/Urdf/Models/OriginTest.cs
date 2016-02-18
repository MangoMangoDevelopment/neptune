using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models;

namespace UrdfUnityTest.Urdf.Models
{
    [TestClass]
    public class OriginTest
    {
        private class NonAbstractOrigin : Origin
        {
            public NonAbstractOrigin(XyzAttribute xyz, RpyAttribute rpy) : base(xyz, rpy)
            {
                // Invoke base constructor.
            }
        }

        [TestMethod]
        public void ConstructOrigin()
        {
            XyzAttribute xyz = new XyzAttribute(1, 2, 3);
            RpyAttribute rpy = new RpyAttribute(4, 5, 6);
            Origin origin = new NonAbstractOrigin(xyz, rpy);

            Assert.AreEqual(origin.Xyz, xyz);
            Assert.AreEqual(origin.Rpy, rpy);
        }

        [TestMethod]
        public void SetProperties()
        {
            XyzAttribute xyz = new XyzAttribute(1, 2, 3);
            RpyAttribute rpy = new RpyAttribute(4, 5, 6);
            Origin origin = new NonAbstractOrigin(new XyzAttribute(), new RpyAttribute());

            Assert.AreNotEqual(origin.Xyz, xyz);
            Assert.AreNotEqual(origin.Rpy, rpy);

            origin.Xyz = xyz;
            origin.Rpy = rpy;

            Assert.AreEqual(origin.Xyz, xyz);
            Assert.AreEqual(origin.Rpy, rpy);
        }
    }
}
