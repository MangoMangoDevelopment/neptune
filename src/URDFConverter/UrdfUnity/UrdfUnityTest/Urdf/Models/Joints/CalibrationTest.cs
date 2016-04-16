using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models.Joints;

namespace UrdfUnityTest.Urdf.Models.Joints
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
        public void ToStringCalibration()
        {
            Assert.AreEqual("<calibration rising=\"0\" falling=\"0\"/>", new Calibration(0, 0).ToString());
            Assert.AreEqual("<calibration rising=\"-1\" falling=\"1\"/>", new Calibration(-1, 1).ToString());
            Assert.AreEqual("<calibration rising=\"3.1415\" falling=\"0.125\"/>", new Calibration(3.1415, 0.125).ToString());
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
