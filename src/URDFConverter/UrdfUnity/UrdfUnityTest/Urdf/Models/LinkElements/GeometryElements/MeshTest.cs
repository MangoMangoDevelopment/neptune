using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models.LinkElements.GeometryElements;

namespace UrdfUnityTest.Urdf.Models.LinkElements.GeometryElements
{
    [TestClass]
    public class MeshTest
    {
        [TestMethod]
        public void ConstructMeshWithFileName()
        {
            string fileName = "fileName";
            Mesh mesh = new Mesh(fileName);

            Assert.AreEqual(fileName, mesh.FileName);
            Assert.IsNotNull(mesh.Scale);
            Assert.IsNull(mesh.Size);
            Assert.AreEqual(1d, mesh.Scale.X);
            Assert.AreEqual(1d, mesh.Scale.Y);
            Assert.AreEqual(1d, mesh.Scale.Z);
        }

        [TestMethod]
        public void ConstructMeshWithScale()
        {
            string fileName = "fileName";
            double scaleX = 1;
            double scaleY = 2;
            double scaleZ = 3;
            ScaleAttribute scale = new ScaleAttribute(scaleX, scaleY, scaleZ);
            Mesh mesh = new Mesh(fileName, scale);

            Assert.AreEqual(fileName, mesh.FileName);
            Assert.AreEqual(scale, mesh.Scale);
            Assert.IsNull(mesh.Size);
            Assert.AreEqual(scaleX, mesh.Scale.X);
            Assert.AreEqual(scaleY, mesh.Scale.Y);
            Assert.AreEqual(scaleZ, mesh.Scale.Z);
        }

        [TestMethod]
        public void ConstructMeshWithSize()
        {
            string fileName = "fileName";
            double length = 1;
            double width = 2;
            double height = 3;
            SizeAttribute size = new SizeAttribute(length, width, height);
            Mesh mesh = new Mesh(fileName, size);

            Assert.AreEqual(fileName, mesh.FileName);
            Assert.IsNotNull(mesh.Scale);
            Assert.AreEqual(size, mesh.Size);
            Assert.AreEqual(length, size.Length);
            Assert.AreEqual(width, size.Width);
            Assert.AreEqual(height, size.Height);
        }

        [TestMethod]
        public void ConstructMeshWithScaleAndSize()
        {
            string fileName = "fileName";
            double scaleX = 1;
            double scaleY = 2;
            double scaleZ = 3;
            ScaleAttribute scale = new ScaleAttribute(scaleX, scaleY, scaleZ);
            double length = 11;
            double width = 22;
            double height = 33;
            SizeAttribute size = new SizeAttribute(length, width, height);
            Mesh mesh = new Mesh(fileName, scale, size);

            Assert.AreEqual(fileName, mesh.FileName);
            Assert.AreEqual(scale, mesh.Scale);
            Assert.AreEqual(scaleX, mesh.Scale.X);
            Assert.AreEqual(scaleY, mesh.Scale.Y);
            Assert.AreEqual(scaleZ, mesh.Scale.Z);
            Assert.AreEqual(size, mesh.Size);
            Assert.AreEqual(length, size.Length);
            Assert.AreEqual(width, size.Width);
            Assert.AreEqual(height, size.Height);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void ConstructMeshEmptyFileName()
        {
            Mesh mesh = new Mesh("");
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void ConstructMeshNullScale()
        {
            Mesh mesh = new Mesh("fileName", (ScaleAttribute)null);
        }

        [TestMethod]
        public void ConstructMeshNullSize()
        {
            Mesh mesh = new Mesh("fileName", (SizeAttribute)null);
            Assert.IsNull(mesh.Size);
        }
    }
}
