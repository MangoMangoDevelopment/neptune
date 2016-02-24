using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models;
using UrdfUnity.Urdf.Models.JointElements;

namespace UrdfUnityTest.Urdf.Models.JointElements
{
    [TestClass]
    public class MimicTest
    {
        [TestMethod]
        public void ConstructMimic()
        {
            Joint joint = new Joint("joint");
            double multiplier = 10;
            double offest = 10;
            Mimic mimic = new Mimic(joint, multiplier, offest);

            Assert.AreEqual(joint, mimic.Joint);
            Assert.AreEqual(joint.Name, mimic.Joint.Name);
            Assert.AreEqual(multiplier, mimic.Multiplier);
            Assert.AreEqual(offest, mimic.Offset);
        }

        [TestMethod]
        public void ConstructMimicNoMultiplierOffset()
        {
            Joint joint = new Joint("joint");
            Mimic mimic = new Mimic(joint);

            Assert.AreEqual(joint, mimic.Joint);
            Assert.AreEqual(1, mimic.Multiplier);
            Assert.AreEqual(0, mimic.Offset);
        }
    }
}
