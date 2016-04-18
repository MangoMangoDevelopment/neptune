using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfToUnity.Urdf.Models.Links.Visuals;

namespace UrdfToUnityTest.Urdf.Models.Links.Visuals
{
    [TestClass]
    public class TextureTest
    {
        [TestMethod]
        public void ConstructTexture()
        {
            string fileName = "fileName";
            Texture texture = new Texture(fileName);

            Assert.AreEqual(fileName, texture.FileName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructTextureNoFileName()
        {
            Texture texture = new Texture("");
        }

        [TestMethod]
        public void ToStringTexture()
        {
            Assert.AreEqual("<texture filename=\"file\"/>", new Texture("file").ToString());
        }

        [TestMethod]
        public void EqualsAndHash()
        {
            Texture texture = new Texture("fileName");
            Texture same = new Texture("fileName");
            Texture diff = new Texture("differentFileName");

            Assert.IsTrue(texture.Equals(texture));
            Assert.IsFalse(texture.Equals(null));
            Assert.IsTrue(texture.Equals(same));
            Assert.IsTrue(same.Equals(texture));
            Assert.IsFalse(texture.Equals(diff));
            Assert.AreEqual(texture.GetHashCode(), same.GetHashCode());
            Assert.AreNotEqual(texture.GetHashCode(), diff.GetHashCode());
        }
    }
}
