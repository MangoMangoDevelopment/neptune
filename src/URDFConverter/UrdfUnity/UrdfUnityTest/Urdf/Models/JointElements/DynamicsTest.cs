using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models.JointElements;

namespace UrdfUnityTest.Urdf.Models.JointElements
{
    [TestClass]
    public class DynamicsTest
    {
        [TestMethod]
        public void ConstructDynamics()
        {
            double damping = 1;
            double friction = 2;
            Dynamics dynamics = new Dynamics(damping, friction);

            Assert.AreEqual(damping, dynamics.Damping);
            Assert.AreEqual(friction, dynamics.Friction);
        }

        [TestMethod]
        public void ConstructDynamicsDefaultValues()
        {
            Dynamics dynamics = new Dynamics();

            Assert.AreEqual(0, dynamics.Damping);
            Assert.AreEqual(0, dynamics.Friction);
        }

        [TestMethod]
        public void ConstructDynamicsDampingOnly()
        {
            double damping = 1;
            Dynamics dynamics = new Dynamics(damping);
            Dynamics dynamicsNamedArg = new Dynamics(damping: damping);

            Assert.AreEqual(damping, dynamics.Damping);
            Assert.AreEqual(0, dynamics.Friction);
            Assert.AreEqual(damping, dynamicsNamedArg.Damping);
            Assert.AreEqual(0, dynamicsNamedArg.Friction);
        }

        [TestMethod]
        public void ConstructDynamicsFrictionOnly()
        {
            double friction = 1;
            Dynamics dynamics = new Dynamics(friction: friction);

            Assert.AreEqual(0, dynamics.Damping);
            Assert.AreEqual(friction, dynamics.Friction);
        }

        [TestMethod]
        public void ToStringDynamics()
        {
            Assert.AreEqual("<dynamics damping=\"0\" friction=\"0\"/>", new Dynamics(0, 0).ToString());
            Assert.AreEqual("<dynamics damping=\"-1\" friction=\"1\"/>", new Dynamics(-1, 1).ToString());
            Assert.AreEqual("<dynamics damping=\"3.1415\" friction=\"0.125\"/>", new Dynamics(3.1415, 0.125).ToString());
        }

        [TestMethod]
        public void EqualsAndHash()
        {
            Dynamics dynamics = new Dynamics(1, 2);
            Dynamics same = new Dynamics(1, 2);
            Dynamics diff = new Dynamics(3, 4);

            Assert.IsTrue(dynamics.Equals(dynamics));
            Assert.IsFalse(dynamics.Equals(null));
            Assert.IsTrue(dynamics.Equals(same));
            Assert.IsTrue(same.Equals(dynamics));
            Assert.IsFalse(dynamics.Equals(diff));
            Assert.AreEqual(dynamics.GetHashCode(), same.GetHashCode());
            Assert.AreNotEqual(dynamics.GetHashCode(), diff.GetHashCode());
        }
    }
}
