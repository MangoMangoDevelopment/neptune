using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models.LinkElements.InertialElements;

namespace UrdfUnityTest.Urdf.Models.LinkElements.InertialElements
{
    [TestClass]
    public class MassTest
    {
        [TestMethod]
        public void ConstructMass()
        {
            double value = 1;
            Mass mass = new Mass(value);

            Assert.AreEqual(value, mass.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructNegativeMass()
        {
            Mass mass = new Mass(-1);
        }

        [TestMethod]
        public void EqualsAndHash()
        {
            Mass mass = new Mass(1);
            Mass same = new Mass(1);
            Mass diff = new Mass(2);

            Assert.IsTrue(mass.Equals(mass));
            Assert.IsFalse(mass.Equals(null));
            Assert.IsTrue(mass.Equals(same));
            Assert.IsTrue(same.Equals(mass));
            Assert.IsFalse(mass.Equals(diff));
            Assert.AreEqual(mass.GetHashCode(), same.GetHashCode());
            Assert.AreNotEqual(mass.GetHashCode(), diff.GetHashCode());
        }
    }
}
