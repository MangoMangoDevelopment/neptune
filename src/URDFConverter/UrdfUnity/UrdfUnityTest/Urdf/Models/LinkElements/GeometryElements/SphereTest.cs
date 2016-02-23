using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models.LinkElements.GeometryElements;

namespace UrdfUnityTest.Urdf.Models.LinkElements.GeometryElements
{
    [TestClass]
    class SphereTest
    {
        [TestMethod]
        public void ConstructSphere()
        {
            double radius = 1;
            Sphere sphere = new Sphere(radius);

            Assert.AreEqual(radius, sphere.Radius);
        }
    }
}
