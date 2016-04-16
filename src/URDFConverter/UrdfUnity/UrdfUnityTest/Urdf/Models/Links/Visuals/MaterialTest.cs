using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models.Attributes;
using UrdfUnity.Urdf.Models.Links.Visuals;

namespace UrdfUnityTest.Urdf.Models.Links.Visuals
{
    [TestClass]
    public class MaterialTest
    {
        [TestMethod]
        public void DefaultName()
        {
            Assert.AreEqual("missing_name", Material.DEFAULT_NAME);
        }

        [TestMethod]
        public void ConstructMaterial()
        {
            string name = "name";
            Color color = new Color(new RgbAttribute(0, 0, 0));
            Texture texture = new Texture("fileName");
            Material material = new Material(name, color, texture);

            Assert.AreEqual(name, material.Name);
            Assert.AreEqual(color, material.Color);
            Assert.AreEqual(texture, material.Texture);
        }

        [TestMethod]
        public void ConstructMaterialNameOnly()
        {
            string name = "name";
            Material material = new Material(name);

            Assert.AreEqual(name, material.Name);
            Assert.IsNull(material.Color);
            Assert.IsNull(material.Texture);
        }

        [TestMethod]
        public void ConstructMaterialWithColor()
        {
            string name = "name";
            Color color = new Color(new RgbAttribute(0, 0, 0));
            Material material = new Material(name, color);

            Assert.AreEqual(name, material.Name);
            Assert.AreEqual(color, material.Color);
            Assert.IsNull(material.Texture);
        }

        [TestMethod]
        public void ConstructMaterialWithTexture()
        {
            string name = "name";
            Texture texture = new Texture("fileName");
            Material material = new Material(name, texture);

            Assert.AreEqual(name, material.Name);
            Assert.IsNull(material.Color);
            Assert.AreEqual(texture, material.Texture);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ContructMaterialNoName()
        {
            Material material = new Material("");
        }

        [TestMethod]
        public void EqualsAndHash()
        {
            Material material = new Material("name", new Texture("fileName"));
            Material same = new Material("name", new Texture("fileName"));
            Material diff = new Material("differentName");

            Assert.IsTrue(material.Equals(material));
            Assert.IsFalse(material.Equals(null));
            Assert.IsTrue(material.Equals(same));
            Assert.IsTrue(same.Equals(material));
            Assert.IsFalse(material.Equals(diff));
            Assert.AreEqual(material.GetHashCode(), same.GetHashCode());
            Assert.AreNotEqual(material.GetHashCode(), diff.GetHashCode());
        }
    }
}
