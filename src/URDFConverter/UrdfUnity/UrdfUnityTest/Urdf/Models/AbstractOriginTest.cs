using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models;

namespace UrdfUnityTest.Urdf.Models
{
    [TestClass]
    public class AbstractOriginTest
    {
        private class NonAbstractOrigin : AbstractOrigin
        {
            public NonAbstractOrigin() : base()
            {
                // Invoke base constructor.
            }

            public NonAbstractOrigin(XyzAttribute xyz) : base(xyz)
            {
                // Invoke base constructor.
            }

            public NonAbstractOrigin(RpyAttribute rpy) : base(rpy)
            {
                // Invoke base constructor.
            }

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

            Assert.AreEqual(xyz, origin.Xyz);
            Assert.AreEqual(rpy, origin.Rpy);
        }

        [TestMethod]
        public void ConstructXyzOrigin()
        {
            XyzAttribute xyz = new XyzAttribute(1, 2, 3);
            AbstractOrigin origin = new NonAbstractOrigin(xyz);

            Assert.AreEqual(xyz, origin.Xyz);
            Assert.AreEqual(0, origin.Rpy.R);
            Assert.AreEqual(0, origin.Rpy.P);
            Assert.AreEqual(0, origin.Rpy.Y);
        }

        [TestMethod]
        public void ConstructRpyOrigin()
        {
            RpyAttribute rpy = new RpyAttribute(4, 5, 6);
            AbstractOrigin origin = new NonAbstractOrigin(rpy);
            
            Assert.AreEqual(rpy, origin.Rpy);
            Assert.AreEqual(0, origin.Xyz.X);
            Assert.AreEqual(0, origin.Xyz.Y);
            Assert.AreEqual(0, origin.Xyz.Z);
        }

        [TestMethod]
        public void ConstructDefaultOrigin()
        {
            AbstractOrigin origin = new NonAbstractOrigin();

            Assert.AreEqual(0, origin.Xyz.X);
            Assert.AreEqual(0, origin.Xyz.Y);
            Assert.AreEqual(0, origin.Xyz.Z);
            Assert.AreEqual(0, origin.Rpy.R);
            Assert.AreEqual(0, origin.Rpy.P);
            Assert.AreEqual(0, origin.Rpy.Y);
        }
    }
}
