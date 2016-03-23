using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models.JointElements;

namespace UrdfUnityTest.Urdf.Models.JointElements
{
    [TestClass]
    public class CalibrationTest
    {
        [TestMethod]
        public void ConstructCalibration()
        {
            double rising = 0.5;
            double falling = 0.5;
            Calibration calibration = new Calibration(rising, falling);

            Assert.AreEqual(rising, calibration.Rising);
            Assert.AreEqual(falling, calibration.Falling);
        }

        [TestMethod]
        public void ConstructCalibrationDefaultValues()
        {
            Calibration calibration = new Calibration();

            Assert.AreEqual(0, calibration.Rising);
            Assert.AreEqual(0, calibration.Falling);
        }

        [TestMethod]
        public void ConstructCalibrationRisingOnly()
        {
            double rising = 0.5;
            Calibration calibration = new Calibration(rising);
            Calibration calibrationNamedArg = new Calibration(rising: rising);

            Assert.AreEqual(rising, calibration.Rising);
            Assert.AreEqual(0, calibration.Falling);
            Assert.AreEqual(rising, calibrationNamedArg.Rising);
            Assert.AreEqual(0, calibrationNamedArg.Falling);
        }

        [TestMethod]
        public void ConstructCalibrationFallingOnly()
        {
            double falling = 0.5;
            Calibration calibration = new Calibration(falling: falling);

            Assert.AreEqual(0, calibration.Rising);
            Assert.AreEqual(falling, calibration.Falling);
        }

        [TestMethod]
        public void EqualsAndHash()
        {
            Calibration calibration = new Calibration(1, 2);
            Calibration same = new Calibration(1, 2);
            Calibration diff = new Calibration(3, 4);

            Assert.IsTrue(calibration.Equals(calibration));
            Assert.IsFalse(calibration.Equals(null));
            Assert.IsTrue(calibration.Equals(same));
            Assert.IsTrue(same.Equals(calibration));
            Assert.IsFalse(calibration.Equals(diff));
            Assert.AreEqual(calibration.GetHashCode(), same.GetHashCode());
            Assert.AreNotEqual(calibration.GetHashCode(), diff.GetHashCode());
        }
    }
}
