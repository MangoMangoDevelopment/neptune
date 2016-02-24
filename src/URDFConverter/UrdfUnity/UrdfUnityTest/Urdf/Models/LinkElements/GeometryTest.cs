using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models.LinkElements;
using UrdfUnity.Urdf.Models.LinkElements.GeometryElements;

namespace UrdfUnityTest.Urdf.Models.LinkElements
{
    [TestClass]
    public class GeometryTest
    {
        [TestMethod]
        public void ConstructGeometryBox()
        {
            Box box = new Box(new SizeAttribute(1, 1, 1));
            Geometry geometry = new Geometry(box);

            Assert.AreEqual(Geometry.Shapes.Box, geometry.Shape);
            Assert.AreEqual(box, geometry.Box);
            Assert.IsNull(geometry.Cylinder);
            Assert.IsNull(geometry.Sphere);
            Assert.IsNull(geometry.Mesh);
        }

        [TestMethod]
        public void ConstructGeometryCylinder()
        {
            Cylinder cylinder = new Cylinder(1, 1);
            Geometry geometry = new Geometry(cylinder);

            Assert.AreEqual(Geometry.Shapes.Cylinder, geometry.Shape);
            Assert.AreEqual(cylinder, geometry.Cylinder);
            Assert.IsNull(geometry.Box);
            Assert.IsNull(geometry.Sphere);
            Assert.IsNull(geometry.Mesh);
        }

        [TestMethod]
        public void ConstructGeometrySphere()
        {
            Sphere sphere = new Sphere(1);
            Geometry geometry = new Geometry(sphere);

            Assert.AreEqual(Geometry.Shapes.Sphere, geometry.Shape);
            Assert.AreEqual(sphere, geometry.Sphere);
            Assert.IsNull(geometry.Box);
            Assert.IsNull(geometry.Cylinder);
            Assert.IsNull(geometry.Mesh);
        }

        [TestMethod]
        public void ConstructGeometryMesh()
        {
            Mesh mesh = new Mesh("name");
            Geometry geometry = new Geometry(mesh);

            Assert.AreEqual(Geometry.Shapes.Mesh, geometry.Shape);
            Assert.AreEqual(mesh, geometry.Mesh);
            Assert.IsNull(geometry.Box);
            Assert.IsNull(geometry.Cylinder);
            Assert.IsNull(geometry.Sphere);
        }
    }
}
