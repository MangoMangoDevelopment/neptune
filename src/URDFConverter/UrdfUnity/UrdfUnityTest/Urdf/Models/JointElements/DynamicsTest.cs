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
