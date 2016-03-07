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
            RgbAttribute rgb = new RgbAttribute(255, 255, 255);
            Color colour = new Color(rgb);

            Assert.AreEqual(rgb, colour.Rgb);
            Assert.AreEqual(1d, colour.Alpha);
        }

        [TestMethod]
        public void ConstructColorWithAlpha()
        {
            double alpha = 0.5;
            RgbAttribute rgb = new RgbAttribute(255, 255, 255);
            Color colour = new Color(rgb, alpha);

            Assert.AreEqual(rgb, colour.Rgb);
            Assert.AreEqual(alpha, colour.Alpha);

            // Test bounds [0,1]
            colour = new Color(rgb, 0d);
            colour = new Color(rgb, 1d);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructColorNullRgb()
        {
            Color colour = new Color(null);
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

        [TestMethod]
        public void EqualsAndHash()
        {
            Color colour = new Color(new RgbAttribute(1, 2, 3));
            Color same = new Color(new RgbAttribute(1, 2, 3), 1);
            Color diff = new Color(new RgbAttribute(3, 2, 1));

            Assert.IsTrue(colour.Equals(colour));
            Assert.IsFalse(colour.Equals(null));
            Assert.IsTrue(colour.Equals(same));
            Assert.IsTrue(same.Equals(colour));
            Assert.IsFalse(colour.Equals(diff));
            Assert.AreEqual(colour.GetHashCode(), same.GetHashCode());
            Assert.AreNotEqual(colour.GetHashCode(), diff.GetHashCode());
        }
    }
}
