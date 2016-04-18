using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfToUnity.Util;

namespace UrdfToUnityTest.Util
{
    [TestClass]
    public class PreconditionsUtilsTest
    {
        private static readonly string ERROR_MESSAGE = "Error!  Error!";
        private static readonly string PARAM_NAME = "paramName";


        #region IsNotNull tests

        [TestMethod]
        public void IsNotNullTrue()
        {
            string str = "Oh, hello";
            Preconditions.IsNotNull(str, ERROR_MESSAGE);
            Preconditions.IsNotNull(str, ERROR_MESSAGE, PARAM_NAME);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsNotNullFalseNoParamName()
        {
            Preconditions.IsNotNull(null, ERROR_MESSAGE);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsNotNullFalseWithParamName()
        {
            Preconditions.IsNotNull(null, ERROR_MESSAGE, PARAM_NAME);
        }

        #endregion
        #region IsNotEmpty tests

        [TestMethod]
        public void IsNotEmptyStringTrue()
        {
            string str = "Oh, hello";
            Preconditions.IsNotEmpty(str, ERROR_MESSAGE);
            Preconditions.IsNotEmpty(str, ERROR_MESSAGE, PARAM_NAME);
        }

        [TestMethod]
        public void IsNotEmptyStringFalseNoParamName()
        {
            int exceptionCount = 0;

            try
            {
                Preconditions.IsNotEmpty(null, ERROR_MESSAGE);
            }
            catch (ArgumentException)
            {
                exceptionCount++;
            }
            try
            {
                Preconditions.IsNotEmpty("", ERROR_MESSAGE);
            }
            catch (ArgumentException)
            {
                exceptionCount++;
            }
            try
            {
                Preconditions.IsNotEmpty(String.Empty, ERROR_MESSAGE);
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
                Preconditions.IsNotEmpty(null, ERROR_MESSAGE, PARAM_NAME);
            }
            catch (ArgumentException)
            {
                exceptionCount++;
            }
            try
            {
                Preconditions.IsNotEmpty("", ERROR_MESSAGE, PARAM_NAME);
            }
            catch (ArgumentException)
            {
                exceptionCount++;
            }
            try
            {
                Preconditions.IsNotEmpty(String.Empty, ERROR_MESSAGE, PARAM_NAME);
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

            Preconditions.IsWithinRange((lowerBound + upperBound) / 2, lowerBound, upperBound, ERROR_MESSAGE);
            Preconditions.IsWithinRange(lowerBound, lowerBound, upperBound, ERROR_MESSAGE);
            Preconditions.IsWithinRange(upperBound, lowerBound, upperBound, ERROR_MESSAGE);
            Preconditions.IsWithinRange(lowerBound, lowerBound, lowerBound, ERROR_MESSAGE);
            Preconditions.IsWithinRange(upperBound, upperBound, upperBound, ERROR_MESSAGE);

            Preconditions.IsWithinRange((lowerBound + upperBound) / 2, lowerBound, upperBound, ERROR_MESSAGE, "paramName");
            Preconditions.IsWithinRange(lowerBound, lowerBound, upperBound, ERROR_MESSAGE, PARAM_NAME);
            Preconditions.IsWithinRange(upperBound, lowerBound, upperBound, ERROR_MESSAGE, PARAM_NAME);
            Preconditions.IsWithinRange(lowerBound, lowerBound, lowerBound, ERROR_MESSAGE, PARAM_NAME);
            Preconditions.IsWithinRange(upperBound, upperBound, upperBound, ERROR_MESSAGE, PARAM_NAME);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsWithinRangeFalseBelowLower()
        {
            int lowerBound = 0;
            int upperBound = 255;
            double value = lowerBound - 0.000000001;

            Assert.IsTrue(value < lowerBound);
            Preconditions.IsWithinRange(value, lowerBound, upperBound, ERROR_MESSAGE);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsWithinRangeFalseAboveUpper()
        {
            int lowerBound = 0;
            int upperBound = 255;
            double value = upperBound + 0.000000001;

            Assert.IsTrue(value > upperBound);
            Preconditions.IsWithinRange(value, lowerBound, upperBound, ERROR_MESSAGE);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void IsWithinRangeBadBounds()
        {
            int lowerBound = 100;
            int upperBound = 0;

            Assert.IsTrue(lowerBound > upperBound);
            Preconditions.IsWithinRange(0, lowerBound, upperBound, ERROR_MESSAGE);
        }

        #endregion
        #region IsTrue tests

        [TestMethod]
        public void IsTrue()
        {
            Preconditions.IsTrue(true, ERROR_MESSAGE);
            Preconditions.IsTrue(true, ERROR_MESSAGE, PARAM_NAME);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsTrueFalse()
        {
            Preconditions.IsTrue(false, ERROR_MESSAGE);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsTrueFalseWithParamName()
        {
            Preconditions.IsTrue(false, ERROR_MESSAGE, PARAM_NAME);
        }

        #endregion
    }
}
