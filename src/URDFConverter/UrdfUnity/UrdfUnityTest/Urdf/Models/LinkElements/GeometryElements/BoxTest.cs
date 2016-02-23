using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models.LinkElements.GeometryElements;

namespace UrdfUnityTest.Urdf.Models.LinkElements.GeometryElements
{
    [TestClass]
    public class BoxTest
    {
        [TestMethod]
        public void ConstructBox()
        {
            double length = 1;
            double width = 2;
            double height = 3;
            SizeAttribute size = new SizeAttribute(length, width, height);
            Box box = new Box(size);

            Assert.AreEqual(size, box.Size);
            Assert.AreEqual(size.Length, length);
            Assert.AreEqual(size.Width, width);
            Assert.AreEqual(size.Height, height);
            Assert.AreNotEqual(size.Length, 0);
            Assert.AreNotEqual(size.Width, 0);
            Assert.AreNotEqual(size.Height, 0);
        }
    }
}
