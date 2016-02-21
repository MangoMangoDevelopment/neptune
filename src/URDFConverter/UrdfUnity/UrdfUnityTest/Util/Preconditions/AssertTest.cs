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
            String str = "Oh, hello";
            UrdfUnity.Util.Preconditions.Assert.IsNotNull(str, "str");
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void IsNotNullFalse()
        {
            String str = null;
            UrdfUnity.Util.Preconditions.Assert.IsNotNull(str, "str");
        }
    }
}
