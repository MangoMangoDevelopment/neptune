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

        [TestMethod]
        public void EqualsAndHash()
        {
            SafetyController safetyController = new SafetyController(1, 2, 3, 4);
            SafetyController same = new SafetyController(1, 2, 3, 4);
            SafetyController diff = new SafetyController(5, 6, 7, 8);

            Assert.IsTrue(safetyController.Equals(safetyController));
            Assert.IsFalse(safetyController.Equals(null));
            Assert.IsTrue(safetyController.Equals(same));
            Assert.IsTrue(same.Equals(safetyController));
            Assert.IsFalse(safetyController.Equals(diff));
            Assert.AreEqual(safetyController.GetHashCode(), same.GetHashCode());
            Assert.AreNotEqual(safetyController.GetHashCode(), diff.GetHashCode());
        }
    }
}
