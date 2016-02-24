using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models.JointElements;

namespace UrdfUnityTest.Urdf.Models.JointElements
{
    [TestClass]
    public class SafetyControllerTest
    {
        [TestMethod]
        public void ConstructSafetyController()
        {
            double lowerLimit = 1;
            double upperLimit = 2;
            double kPosition = 3;
            double kVelocity = 4;
            SafetyController safetyController = new SafetyController(lowerLimit, upperLimit, kPosition, kVelocity);

            Assert.AreEqual(lowerLimit, safetyController.SoftLowerLimit);
            Assert.AreEqual(upperLimit, safetyController.SoftUpperLimit);
            Assert.AreEqual(kPosition, safetyController.KPostition);
            Assert.AreEqual(kVelocity, safetyController.KVelocity);
        }

        [TestMethod]
        public void ConstructSafetyControllerOnlyVelocity()
        {
            double kVelocity = 4;
            SafetyController safetyController = new SafetyController(kVelocity);

            Assert.AreEqual(0, safetyController.SoftLowerLimit);
            Assert.AreEqual(0, safetyController.SoftUpperLimit);
            Assert.AreEqual(0, safetyController.KPostition);
            Assert.AreEqual(kVelocity, safetyController.KVelocity);
        }
    }
}
