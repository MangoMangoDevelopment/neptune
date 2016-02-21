using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models;

namespace UrdfUnityTest.Urdf.Models
{
    [TestClass]
    public class AbstractOriginTest
    {
        private class NonAbstractOrigin : AbstractOrigin
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
            AbstractOrigin origin = new NonAbstractOrigin(xyz, rpy);

            Assert.AreEqual(origin.Xyz, xyz);
            Assert.AreEqual(origin.Rpy, rpy);
        }
    }
}
