using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UrdfUnityTest.Preconditions.Util
{
    [TestClass]
    public class AssertTest
    {
        [TestMethod]
        public void IsNotNullTrue()
        {
            string str = "Oh, hello";
            UrdfUnity.Util.Preconditions.Assert.IsNotNull(str);
            UrdfUnity.Util.Preconditions.Assert.IsNotNull(str, "paramName");
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void IsNotNullFalseNoParamName()
        {
            UrdfUnity.Util.Preconditions.Assert.IsNotNull(null);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void IsNotNullFalseWithParamName()
        {
            UrdfUnity.Util.Preconditions.Assert.IsNotNull(null, "paramName");
        }

        [TestMethod]
        public void IsNotEmptyStringTrue()
        {
            string str = "Oh, hello";
            UrdfUnity.Util.Preconditions.Assert.IsNotEmpty(str);
            UrdfUnity.Util.Preconditions.Assert.IsNotEmpty(str, "paramName");
        }

        [TestMethod]
        public void IsNotEmptyStringFalseNoParamName()
        {
            int exceptionCount = 0;

            try
            {
                UrdfUnity.Util.Preconditions.Assert.IsNotEmpty(null);
            }
            catch (ArgumentException)
            {
                exceptionCount++;
            }
            try
            {
                UrdfUnity.Util.Preconditions.Assert.IsNotEmpty("");
            }
            catch (ArgumentException)
            {
                exceptionCount++;
            }
            try
            {
                UrdfUnity.Util.Preconditions.Assert.IsNotEmpty(String.Empty);
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
                UrdfUnity.Util.Preconditions.Assert.IsNotEmpty(null, "paramName");
            }
            catch (ArgumentException)
            {
                exceptionCount++;
            }
            try
            {
                UrdfUnity.Util.Preconditions.Assert.IsNotEmpty("", "paramName");
            }
            catch (ArgumentException)
            {
                exceptionCount++;
            }
            try
            {
                UrdfUnity.Util.Preconditions.Assert.IsNotEmpty(String.Empty, "paramName");
            }
            catch (ArgumentException)
            {
                exceptionCount++;
            }

            Assert.AreEqual(3, exceptionCount);
        }
    }
}
