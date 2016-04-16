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
        public void ToStringLimit()
        {
            Assert.AreEqual("<limit effort=\"0\" velocity=\"0\" lower=\"0\" upper=\"0\"/>", new Limit(0, 0, 0, 0).ToString());
            Assert.AreEqual("<limit effort=\"-1\" velocity=\"1\" lower=\"1000\" upper=\"0\"/>", new Limit(-1, 1, 1000, 0).ToString());
            Assert.AreEqual("<limit effort=\"3.1415\" velocity=\"0.125\" lower=\"0.1\" upper=\"1000.0001\"/>", new Limit(3.1415, 0.125, 0.1, 1000.0001).ToString());
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
