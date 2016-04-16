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

        [TestMethod]
        public void ToStringMimic()
        {
            Assert.AreEqual("<mimic joint=\"joint\" multiplier=\"0\" offset=\"0\"/>", new Mimic(TEST_JOINT, 0, 0).ToString());
            Assert.AreEqual("<mimic joint=\"joint\" multiplier=\"-1\" offset=\"1\"/>", new Mimic(TEST_JOINT, -1, 1).ToString());
            Assert.AreEqual("<mimic joint=\"joint\" multiplier=\"3.1415\" offset=\"0.125\"/>", new Mimic(TEST_JOINT, 3.1415, 0.125).ToString());
        }

        [TestMethod]
        public void EqualsAndHash()
        {
            Joint joint = new Joint.Builder("joint", Joint.JointType.Continuous, new Link.Builder("parent").Build(), new Link.Builder("child").Build()).Build();
            Mimic mimic = new Mimic(joint);
            Mimic same = new Mimic(joint);
            Mimic diff = new Mimic(joint, 1, 2);

            Assert.IsTrue(mimic.Equals(mimic));
            Assert.IsFalse(mimic.Equals(null));
            Assert.IsTrue(mimic.Equals(same));
            Assert.IsTrue(same.Equals(mimic));
            Assert.IsFalse(mimic.Equals(diff));
            Assert.AreEqual(mimic.GetHashCode(), same.GetHashCode());
            Assert.AreNotEqual(mimic.GetHashCode(), diff.GetHashCode());
        }
    }
}
