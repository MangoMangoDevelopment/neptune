using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models.LinkElements.GeometryElements;

namespace UrdfUnityTest.Urdf.Models.LinkElements.GeometryElements
{
    [TestClass]
    public class SizeAttributeTest
    {
        [TestMethod]
        public void ConsructSizeAttribute()
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
    }
}
