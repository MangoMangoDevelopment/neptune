using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models.Attributes;

namespace UrdfUnityTest.Urdf.Models.Attributes
{
    [TestClass]
    public class ScaleAttributeTest
    {
        [TestMethod]
        public void ConstructScaleAttribute()
        {
            double x = 1;
            double y = 2;
            double z = 3;
            ScaleAttribute scale = new ScaleAttribute(x, y, z);

            Assert.AreEqual(x, scale.X);
            Assert.AreEqual(y, scale.Y);
            Assert.AreEqual(z, scale.Z);
        }

        [TestMethod]
        public void EqualsAndHash()
        {
            ScaleAttribute scale = new ScaleAttribute(1, 2, 3);
            ScaleAttribute same = new ScaleAttribute(1, 2, 3);
            ScaleAttribute diff = new ScaleAttribute(7, 7, 7);

            Assert.IsTrue(scale.Equals(scale));
            Assert.IsFalse(scale.Equals(null));
            Assert.IsTrue(scale.Equals(same));
            Assert.IsTrue(same.Equals(scale));
            Assert.IsFalse(scale.Equals(diff));
            Assert.AreEqual(scale.GetHashCode(), same.GetHashCode());
            Assert.AreNotEqual(scale.GetHashCode(), diff.GetHashCode());
        }
    }
}
