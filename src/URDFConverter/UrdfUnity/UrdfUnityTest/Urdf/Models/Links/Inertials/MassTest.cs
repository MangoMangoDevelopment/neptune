using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models.Links.Inertials;

namespace UrdfUnityTest.Urdf.Models.Links.Inertials
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
        public void ToStringMass()
        {
            Assert.AreEqual("<mass value=\"1\"/>", new Mass(1).ToString());
            Assert.AreEqual("<mass value=\"3.1415\"/>", new Mass(3.1415).ToString());
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
