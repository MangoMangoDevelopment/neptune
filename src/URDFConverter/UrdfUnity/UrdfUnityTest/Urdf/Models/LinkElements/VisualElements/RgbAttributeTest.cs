using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models.LinkElements.VisualElements;

namespace UrdfUnityTest.Urdf.Models.LinkElements.VisualElements
{
    [TestClass]
    public class RgbAttributeTest
    {
        [TestMethod]
        public void ConstructRgbAttribute()
        {
            int r = 255;
            int g = 255;
            int b = 255;
            RgbAttribute rgb = new RgbAttribute(r, g, b);

            Assert.AreEqual(r, rgb.R);
            Assert.AreEqual(g, rgb.G);
            Assert.AreEqual(b, rgb.B);
        }

        [TestMethod]
        public void ConstructRgbAttributeBadValues()
        {
            int testCount = 0;
            int exceptionCount = 0;

            // Test values outside valid range [0, 255]
            int[,] rgbTestValues = new int[,] { { -1, 0, 0 }, { 0, -1, 0 }, { 0, 0, -1 }, 
                { 256, 0, 0 }, { 0, 256, 0 }, { 0, 0, 256 } };

            for(int i = 0; i < rgbTestValues.GetLength(0); i++)
            {
                try
                {
                    testCount++;
                    RgbAttribute rgb = new RgbAttribute(rgbTestValues[i, 0], rgbTestValues[i, 1], rgbTestValues[i, 2]);
                }
                catch (ArgumentException)
                {
                    exceptionCount++;
                }
            }

            Assert.AreEqual(testCount, exceptionCount);
        }
    }
}
