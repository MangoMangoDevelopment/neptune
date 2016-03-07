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
            Mesh mesh = new Mesh.Builder(fileName).Build();

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
            Mesh mesh = new Mesh.Builder(fileName).SetScale(scale).Build();

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
            Mesh mesh = new Mesh.Builder(fileName).SetSize(size).Build();

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
            Mesh mesh = new Mesh.Builder(fileName).SetScale(scale).SetSize(size).Build();

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
            Mesh mesh = new Mesh.Builder("").Build();
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void ConstructMeshNullScale()
        {
            Mesh mesh = new Mesh.Builder("fileName").SetScale(null).Build();
        }

        [TestMethod]
        public void ConstructMeshNullSize()
        {
            Mesh mesh = new Mesh.Builder("fileName").SetSize(null).Build();
            Assert.IsNull(mesh.Size);
        }

        [TestMethod]
        public void EqualsAndHash()
        {
            Mesh mesh = new Mesh.Builder("fileName").SetScale(new ScaleAttribute(1, 2, 3)).Build();
            Mesh same = new Mesh.Builder("fileName").SetScale(new ScaleAttribute(1, 2, 3)).Build();
            Mesh diff = new Mesh.Builder("fileName").SetScale(new ScaleAttribute(7, 7, 7)).Build();

            Assert.IsTrue(mesh.Equals(mesh));
            Assert.IsFalse(mesh.Equals(null));
            Assert.IsTrue(mesh.Equals(same));
            Assert.IsTrue(same.Equals(mesh));
            Assert.IsFalse(mesh.Equals(diff));
            Assert.AreEqual(mesh.GetHashCode(), same.GetHashCode());
            Assert.AreNotEqual(mesh.GetHashCode(), diff.GetHashCode());
        }
    }
}
