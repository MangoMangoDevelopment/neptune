using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models.JointElements;

namespace UrdfUnityTest.Urdf.Models.JointElements
{
    [TestClass]
    public class LimitTest
    {
        [TestMethod]
        public void ConstructLimit()
        {
            double lower = 1;
            double upper = 2;
            double effort = 3;
            double velocity = 4;
            Limit limit = new Limit(lower, upper, effort, velocity);

            Assert.AreEqual(lower, limit.Lower);
            Assert.AreEqual(upper, limit.Upper);
            Assert.AreEqual(effort, limit.Effort);
            Assert.AreEqual(velocity, limit.Velocity);
        }

        [TestMethod]
        public void ConstructLimitNoLowerUpper()
        {
            double effort = 1;
            double velocity = 2;
            Limit limit = new Limit(effort, velocity);

            Assert.AreEqual(0, limit.Lower);
            Assert.AreEqual(0, limit.Upper);
            Assert.AreEqual(effort, limit.Effort);
            Assert.AreEqual(velocity, limit.Velocity);
        }
    }
}
