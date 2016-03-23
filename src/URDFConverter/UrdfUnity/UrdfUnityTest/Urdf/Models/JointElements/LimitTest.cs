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
            double effort = 1;
            double velocity = 2;
            double lower = 3;
            double upper = 4;
            Limit limit = new Limit(effort, velocity, lower, upper);

            Assert.AreEqual(effort, limit.Effort);
            Assert.AreEqual(velocity, limit.Velocity);
            Assert.AreEqual(lower, limit.Lower);
            Assert.AreEqual(upper, limit.Upper);
        }

        [TestMethod]
        public void ConstructLimitNoLowerUpper()
        {
            double effort = 1;
            double velocity = 2;
            Limit limit = new Limit(effort, velocity);

            Assert.AreEqual(effort, limit.Effort);
            Assert.AreEqual(velocity, limit.Velocity);
            Assert.AreEqual(0, limit.Lower);
            Assert.AreEqual(0, limit.Upper);
        }

        [TestMethod]
        public void ConstructLimitWithUpperNoLower()
        {
            double effort = 1;
            double velocity = 2;
            double upper = 3;
            Limit limit = new Limit(effort, velocity, upper:upper);

            Assert.AreEqual(effort, limit.Effort);
            Assert.AreEqual(velocity, limit.Velocity);
            Assert.AreEqual(0, limit.Lower);
            Assert.AreEqual(upper, limit.Upper);
        }

        [TestMethod]
        public void EqualsAndHash()
        {
            Limit limit = new Limit(1, 2);
            Limit same = new Limit(1, 2, 0, 0);
            Limit diff = new Limit(3, 4);

            Assert.IsTrue(limit.Equals(limit));
            Assert.IsFalse(limit.Equals(null));
            Assert.IsTrue(limit.Equals(same));
            Assert.IsTrue(same.Equals(limit));
            Assert.IsFalse(limit.Equals(diff));
            Assert.AreEqual(limit.GetHashCode(), same.GetHashCode());
            Assert.AreNotEqual(limit.GetHashCode(), diff.GetHashCode());
        }
    }
}
