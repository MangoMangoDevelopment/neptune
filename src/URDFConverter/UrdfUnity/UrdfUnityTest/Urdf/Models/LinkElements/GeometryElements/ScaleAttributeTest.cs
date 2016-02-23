using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models.LinkElements.GeometryElements;

namespace UrdfUnityTest.Urdf.Models.LinkElements.GeometryElements
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
    }
}
