using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models.Attributes;

namespace UrdfUnityTest.Urdf.Models.Attributes
{
    [TestClass]
    public class SizeAttributeTest
    {
        [TestMethod]
        public void ConstructSizeAttribute()
        {
            double length = 1;
            double width = 2;
            double height = 3;
            SizeAttribute size = new SizeAttribute(length, width, height);

            Assert.AreEqual(length, size.Length);
            Assert.AreEqual(width, size.Width);
            Assert.AreEqual(height, size.Height);
            Assert.AreNotEqual(length, size.Width);
            Assert.AreNotEqual(length, size.Height);
            Assert.AreNotEqual(width, size.Length);
            Assert.AreNotEqual(width, size.Height);
            Assert.AreNotEqual(height, size.Length);
            Assert.AreNotEqual(height, size.Width);
        }

        [TestMethod]
        public void ToStringSize()
        {
            Assert.AreEqual("0 0 0", new SizeAttribute(0, 0, 0).ToString());
            Assert.AreEqual("0.1 0 1", new SizeAttribute(0.1, 0, 1.0).ToString());
            Assert.AreEqual("3.1415 0 -1.25", new SizeAttribute(3.1415, 0, -1.25).ToString());
        }

        [TestMethod]
        public void EqualsAndHash()
        {
            SizeAttribute size = new SizeAttribute(1, 2, 3);
            SizeAttribute same = new SizeAttribute(1, 2, 3);
            SizeAttribute diff = new SizeAttribute(7, 7, 7);

            Assert.IsTrue(size.Equals(size));
            Assert.IsFalse(size.Equals(null));
            Assert.IsTrue(size.Equals(same));
            Assert.IsTrue(same.Equals(size));
            Assert.IsFalse(size.Equals(diff));
            Assert.AreEqual(size.GetHashCode(), same.GetHashCode());
            Assert.AreNotEqual(size.GetHashCode(), diff.GetHashCode());
        }
    }
}
