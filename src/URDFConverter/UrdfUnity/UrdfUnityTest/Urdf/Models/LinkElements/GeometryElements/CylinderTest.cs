using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models.LinkElements.GeometryElements;

namespace UrdfUnityTest.Urdf.Models.LinkElements.GeometryElements
{
    [TestClass]
    public class CylinderTest
    {
        [TestMethod]
        public void ConstructCylinder()
        {
            double radius = 1;
            double length = 2;
            Cylinder cylinder = new Cylinder(radius, length);

            Assert.AreEqual(radius, cylinder.Radius);
            Assert.AreEqual(length, cylinder.Length);
        }
    }
}
