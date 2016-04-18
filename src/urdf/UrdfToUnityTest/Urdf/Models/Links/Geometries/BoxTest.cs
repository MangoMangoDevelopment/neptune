using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfToUnity.Urdf.Models.Attributes;
using UrdfToUnity.Urdf.Models.Links.Geometries;

namespace UrdfToUnityTest.Urdf.Models.Links.Geometries
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

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructBoxInvalidLength()
        {
            new Box(new SizeAttribute(0, 1, 1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructBoxInvalidWidth()
        {
            new Box(new SizeAttribute(1, 0, 1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructBoxInvalidHeight()
        {
            new Box(new SizeAttribute(1, 1, 0));
        }

        [TestMethod]
        public void ToStringBox()
        {
            Assert.AreEqual("<box size=\"1 1 1\"/>", new Box(new SizeAttribute(1, 1, 1)).ToString());
            Assert.AreEqual("<box size=\"0.1 1 1\"/>", new Box(new SizeAttribute(0.1, 1, 1.0)).ToString());
            Assert.AreEqual("<box size=\"3.1415 1 1.25\"/>", new Box(new SizeAttribute(3.1415, 1, 1.25)).ToString());
        }

        [TestMethod]
        public void EqualsAndHash()
        {
            Box box = new Box(new SizeAttribute(1, 2, 3));
            Box same = new Box(new SizeAttribute(1, 2, 3));
            Box diff = new Box(new SizeAttribute(7, 7, 7));

            Assert.IsTrue(box.Equals(box));
            Assert.IsFalse(box.Equals(null));
            Assert.IsTrue(box.Equals(same));
            Assert.IsTrue(same.Equals(box));
            Assert.IsFalse(box.Equals(diff));
            Assert.AreEqual(box.GetHashCode(), same.GetHashCode());
            Assert.AreNotEqual(box.GetHashCode(), diff.GetHashCode());
        }
    }
}
