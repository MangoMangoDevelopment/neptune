using System;
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
    }
}
