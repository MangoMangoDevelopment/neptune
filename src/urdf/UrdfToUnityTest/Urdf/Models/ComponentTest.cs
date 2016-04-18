using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfToUnity.Urdf.Models;
using UrdfToUnity.Urdf.Models.Attributes;
using UrdfToUnity.Urdf.Models.Links.Geometries;

namespace UrdfToUnityTest.Urdf.Models
{
    [TestClass]
    public class ComponentTest
    {
        [TestMethod]
        public void ConstructComponentMesh()
        {
            string name = "name";
            string filepath = "file";
            Component component = new Component(name, filepath);

            Assert.AreEqual(name, component.Name);
            Assert.AreEqual(filepath, component.FileName);
            Assert.IsNull(component.Box);
        }

        [TestMethod]
        public void ConstructComponentBox()
        {
            string name = "name";
            Box box = new Box(new SizeAttribute(1, 2, 3));
            Component component = new Component(name, box);

            Assert.AreEqual(name, component.Name);
            Assert.AreEqual(box, component.Box);
            Assert.IsNull(component.FileName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructComponentEmptyName()
        {
            new Component("", "file");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructComponentNullName()
        {
            new Component(null, "file");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructComponentEmptyoFilepath()
        {
            new Component("name", "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructComponentNullFilepath()
        {
            new Component("name", (string)null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructComponentNullBox()
        {
            new Component("name", (Box)null);
        }

        [TestMethod]
        public void EqualsAndHash()
        {
            Component component = new Component("name", "file");
            Component same = new Component("name", "file");
            Component diff = new Component("different name", "different file");

            Assert.IsTrue(component.Equals(component));
            Assert.IsFalse(component.Equals(null));
            Assert.IsTrue(component.Equals(same));
            Assert.IsTrue(same.Equals(component));
            Assert.IsFalse(component.Equals(diff));
            Assert.AreEqual(component.GetHashCode(), same.GetHashCode());
            Assert.AreNotEqual(component.GetHashCode(), diff.GetHashCode());
        }
    }
}
