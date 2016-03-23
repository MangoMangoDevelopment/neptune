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
            double kVelocity = 1;
            double kPosition = 2;
            double lowerLimit = 3;
            double upperLimit = 4;
            SafetyController safetyController = new SafetyController(kVelocity, kPosition, lowerLimit, upperLimit);

            Assert.AreEqual(kVelocity, safetyController.KVelocity);
            Assert.AreEqual(kPosition, safetyController.KPostition);
            Assert.AreEqual(lowerLimit, safetyController.SoftLowerLimit);
            Assert.AreEqual(upperLimit, safetyController.SoftUpperLimit);
        }

        [TestMethod]
        public void ConstructSafetyControllerOnlyVelocity()
        {
            double kVelocity = 1;
            SafetyController safetyController = new SafetyController(kVelocity);

            Assert.AreEqual(kVelocity, safetyController.KVelocity);
            Assert.AreEqual(0, safetyController.KPostition);
            Assert.AreEqual(0, safetyController.SoftLowerLimit);
            Assert.AreEqual(0, safetyController.SoftUpperLimit);
        }

        [TestMethod]
        public void ConstructSafetyControllerVelocityAndEffort()
        {
            double kVelocity = 1;
            double kPosition = 2;
            SafetyController safetyController = new SafetyController(kVelocity, kPosition);
            SafetyController safetyControllerNamedArg = new SafetyController(kVelocity, kPosition: kPosition);

            Assert.AreEqual(kVelocity, safetyController.KVelocity);
            Assert.AreEqual(kPosition, safetyController.KPostition);
            Assert.AreEqual(0, safetyController.SoftLowerLimit);
            Assert.AreEqual(0, safetyController.SoftUpperLimit);
            Assert.AreEqual(kVelocity, safetyControllerNamedArg.KVelocity);
            Assert.AreEqual(kPosition, safetyControllerNamedArg.KPostition);
            Assert.AreEqual(0, safetyControllerNamedArg.SoftLowerLimit);
            Assert.AreEqual(0, safetyControllerNamedArg.SoftUpperLimit);
        }

        [TestMethod]
        public void ConstructSafetyControllerVelocityAndLower()
        {
            double kVelocity = 1;
            double lowerLimit = 3;
            SafetyController safetyController = new SafetyController(kVelocity, lowerLimit: lowerLimit);

            Assert.AreEqual(kVelocity, safetyController.KVelocity);
            Assert.AreEqual(0, safetyController.KPostition);
            Assert.AreEqual(lowerLimit, safetyController.SoftLowerLimit);
            Assert.AreEqual(0, safetyController.SoftUpperLimit);
        }

        [TestMethod]
        public void ConstructSafetyControllerVelocityAndUpper()
        {
            double kVelocity = 1;
            double upperLimit = 4;
            SafetyController safetyController = new SafetyController(kVelocity, upperLimit: upperLimit);

            Assert.AreEqual(kVelocity, safetyController.KVelocity);
            Assert.AreEqual(0, safetyController.KPostition);
            Assert.AreEqual(0, safetyController.SoftLowerLimit);
            Assert.AreEqual(upperLimit, safetyController.SoftUpperLimit);
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
