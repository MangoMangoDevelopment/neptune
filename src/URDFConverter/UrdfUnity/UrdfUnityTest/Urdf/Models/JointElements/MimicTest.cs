using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models;
using UrdfUnity.Urdf.Models.JointElements;

namespace UrdfUnityTest.Urdf.Models.JointElements
{
    [TestClass]
    public class MimicTest
    {
        private static readonly Joint TEST_JOINT = new Joint.Builder("joint", Joint.JointType.Fixed, 
            new Link.Builder("parent").Build(), new Link.Builder("child").Build()).Build();

        [TestMethod]
        public void ConstructMimic()
        {
            double multiplier = 10;
            double offest = 10;
            Mimic mimic = new Mimic(TEST_JOINT, multiplier, offest);

            Assert.AreEqual(TEST_JOINT, mimic.Joint);
            Assert.AreEqual(TEST_JOINT.Name, mimic.Joint.Name);
            Assert.AreEqual(multiplier, mimic.Multiplier);
            Assert.AreEqual(offest, mimic.Offset);
        }

        [TestMethod]
        public void ConstructMimicNoMultiplierOffset()
        {
            Mimic mimic = new Mimic(TEST_JOINT);

            Assert.AreEqual(TEST_JOINT, mimic.Joint);
            Assert.AreEqual(1, mimic.Multiplier);
            Assert.AreEqual(0, mimic.Offset);
        }
    }
}
