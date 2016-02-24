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
    }
}
