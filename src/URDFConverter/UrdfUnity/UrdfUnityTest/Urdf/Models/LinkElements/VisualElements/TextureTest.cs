using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models.LinkElements.VisualElements;

namespace UrdfUnityTest.Urdf.Models.LinkElements.VisualElements
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
    }
}
