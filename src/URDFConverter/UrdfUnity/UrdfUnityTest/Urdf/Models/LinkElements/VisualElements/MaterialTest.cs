using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models.LinkElements.VisualElements;

namespace UrdfUnityTest.Urdf.Models.LinkElements.VisualElements
{
    [TestClass]
    public class MaterialTest
    {
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
    }
}
