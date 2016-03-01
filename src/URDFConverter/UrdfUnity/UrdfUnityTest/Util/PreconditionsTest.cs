using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Util;

namespace UrdfUnityTest.Util
{
    [TestClass]
    public class PreconditionsTest
    {
        #region IsNotNull tests

        [TestMethod]
        public void IsNotNullTrue()
        {
            string str = "Oh, hello";
            Preconditions.IsNotNull(str);
            Preconditions.IsNotNull(str, "paramName");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsNotNullFalseNoParamName()
        {
            Preconditions.IsNotNull(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsNotNullFalseWithParamName()
        {
            Preconditions.IsNotNull(null, "paramName");
        }

        #endregion
        #region IsNotEmpty tests

        [TestMethod]
        public void IsNotEmptyStringTrue()
        {
            string str = "Oh, hello";
            Preconditions.IsNotEmpty(str);
            Preconditions.IsNotEmpty(str, "paramName");
        }

        [TestMethod]
        public void IsNotEmptyStringFalseNoParamName()
        {
            int exceptionCount = 0;

            try
            {
                Preconditions.IsNotEmpty(null);
            }
            catch (ArgumentException)
            {
                exceptionCount++;
            }
            try
            {
                Preconditions.IsNotEmpty("");
            }
            catch (ArgumentException)
            {
                exceptionCount++;
            }
            try
            {
                Preconditions.IsNotEmpty(String.Empty);
            }
            catch (ArgumentException)
            {
                exceptionCount++;
            }

            Assert.AreEqual(3, exceptionCount);
        }

        [TestMethod]
        public void IsNotEmptyStringFalseWithParamName()
        {
            int exceptionCount = 0;

            try
            {
                Preconditions.IsNotEmpty(null, "paramName");
            }
            catch (ArgumentException)
            {
                exceptionCount++;
            }
            try
            {
                Preconditions.IsNotEmpty("", "paramName");
            }
            catch (ArgumentException)
            {
                exceptionCount++;
            }
            try
            {
                Preconditions.IsNotEmpty(String.Empty, "paramName");
            }
            catch (ArgumentException)
            {
                exceptionCount++;
            }

            Assert.AreEqual(3, exceptionCount);
        }

        #endregion
        #region IsWithinRange tests

        [TestMethod]
        public void IsWithinRangeTrue()
        {
            int lowerBound = 0;
            int upperBound = 255;

            Preconditions.IsWithinRange((lowerBound + upperBound) / 2, lowerBound, upperBound);
            Preconditions.IsWithinRange(lowerBound, lowerBound, upperBound);
            Preconditions.IsWithinRange(upperBound, lowerBound, upperBound);
            Preconditions.IsWithinRange(lowerBound, lowerBound, lowerBound);
            Preconditions.IsWithinRange(upperBound, upperBound, upperBound);

            Preconditions.IsWithinRange((lowerBound + upperBound) / 2, lowerBound, upperBound, "paramName");
            Preconditions.IsWithinRange(lowerBound, lowerBound, upperBound, "paramName");
            Preconditions.IsWithinRange(upperBound, lowerBound, upperBound, "paramName");
            Preconditions.IsWithinRange(lowerBound, lowerBound, lowerBound, "paramName");
            Preconditions.IsWithinRange(upperBound, upperBound, upperBound, "paramName");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsWithinRangeFalseBelowLower()
        {
            int lowerBound = 0;
            int upperBound = 255;
            double value = lowerBound - 0.000000001;

            Assert.IsTrue(value < lowerBound);
            Preconditions.IsWithinRange(value, lowerBound, upperBound);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsWithinRangeFalseAboveUpper()
        {
            int lowerBound = 0;
            int upperBound = 255;
            double value = upperBound + 0.000000001;

            Assert.IsTrue(value > upperBound);
            Preconditions.IsWithinRange(value, lowerBound, upperBound);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void IsWithinRangeBadBounds()
        {
            int lowerBound = 100;
            int upperBound = 0;

            Assert.IsTrue(lowerBound > upperBound);
            Preconditions.IsWithinRange(0, lowerBound, upperBound);
        }

        #endregion
        #region IsTrue tests

        [TestMethod]
        public void IsTrue()
        {
            Preconditions.IsTrue(true);
            Preconditions.IsTrue(true, "paramName");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsTrueFalse()
        {
            Preconditions.IsTrue(false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsTrueFalseWithParamName()
        {
            Preconditions.IsTrue(false, "paramName");
        }

        #endregion
    }
}
