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
        public void ConstructRgbAttributeDoubles()
        {
            double r = 0.5d;
            double g = 0.75d;
            double b = 0d;
            RgbAttribute rgb = new RgbAttribute(r, g, b);

            Assert.AreEqual((int)(255 * r), rgb.R);
            Assert.AreEqual((int)(255 * g), rgb.G);
            Assert.AreEqual((int)(255 * b), rgb.B);
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

        [TestMethod]
        public void EqualsAndHash()
        {
            RgbAttribute rgb = new RgbAttribute(1, 2, 3);
            RgbAttribute same = new RgbAttribute(1, 2, 3);
            RgbAttribute diff = new RgbAttribute(3, 2, 1);

            Assert.IsTrue(rgb.Equals(rgb));
            Assert.IsFalse(rgb.Equals(null));
            Assert.IsTrue(rgb.Equals(same));
            Assert.IsTrue(same.Equals(rgb));
            Assert.IsFalse(rgb.Equals(diff));
            Assert.AreEqual(rgb.GetHashCode(), same.GetHashCode());
            Assert.AreNotEqual(rgb.GetHashCode(), diff.GetHashCode());
        }
    }
}
