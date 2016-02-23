using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models.LinkElements.VisualElements;

namespace UrdfUnityTest.Urdf.Models.LinkElements.VisualElements
{
    [TestClass]
    public class ColorTest
    {
        [TestMethod]
        public void ConstructColorNoAlpha()
        {
            int r = 255;
            int g = 255;
            int b = 255;
            RgbAttribute rgb = new RgbAttribute(r, g, b);
            Color colour = new Color(rgb);

            Assert.AreEqual(rgb, colour.Rgb);
            Assert.AreEqual(r, colour.Rgb.R);
            Assert.AreEqual(g, colour.Rgb.G);
            Assert.AreEqual(b, colour.Rgb.B);
            Assert.AreEqual(1d, colour.Alpha);
        }

        [TestMethod]
        public void ConstructColorWithAlpha()
        {
            int r = 255;
            int g = 255;
            int b = 255;
            double alpha = 0.5;
            RgbAttribute rgb = new RgbAttribute(r, g, b);
            Color colour = new Color(rgb, alpha);

            Assert.AreEqual(rgb, colour.Rgb);
            Assert.AreEqual(r, colour.Rgb.R);
            Assert.AreEqual(g, colour.Rgb.G);
            Assert.AreEqual(b, colour.Rgb.B);
            Assert.AreEqual(alpha, colour.Alpha);

            // Test bounds [0,1]
            new Color(rgb, 0d);
            new Color(rgb, 1d);
        }

        [TestMethod]
        public void ConstructColorInvalidAlpha()
        {
            int testCount = 0;
            int exceptionCount = 0;
            RgbAttribute rgb = new RgbAttribute(0, 0, 0);
            double[] alphaTestValues = new double[] { -1, -0.0001, 1.00000001, 2 };

            for (int i = 0; i < alphaTestValues.Length; i++)
            {
                testCount++;
                try
                {
                    Color colour = new Color(rgb, alphaTestValues[i]);
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
