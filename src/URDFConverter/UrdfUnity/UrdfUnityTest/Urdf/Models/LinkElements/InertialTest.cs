using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models;
using UrdfUnity.Urdf.Models.LinkElements;
using UrdfUnity.Urdf.Models.LinkElements.InertialElements;

namespace UrdfUnityTest.Urdf.Models.LinkElements
{
    [TestClass]
    public class InertialTest
    {
        [TestMethod]
        public void ConstructInertial()
        {
            Origin origin = new Origin();
            Mass mass = new Mass(1);
            Inertia inertia = new Inertia(0, 0, 0, 0, 0, 0);
            Inertial inertial = new Inertial(origin, mass, inertia);

            Assert.AreEqual(origin, inertial.Origin);
            Assert.AreEqual(mass, inertial.Mass);
            Assert.AreEqual(inertia, inertial.Inertia);
        }

        [TestMethod]
        public void ConstructInertialNoOrigin()
        {
            Mass mass = new Mass(1);
            Inertia inertia = new Inertia(0, 0, 0, 0, 0, 0);
            Inertial inertial = new Inertial(mass, inertia);
            
            Assert.AreEqual(mass, inertial.Mass);
            Assert.AreEqual(inertia, inertial.Inertia);
            Assert.AreEqual(0, inertial.Origin.Xyz.X);
            Assert.AreEqual(0, inertial.Origin.Xyz.Y);
            Assert.AreEqual(0, inertial.Origin.Xyz.Z);
            Assert.AreEqual(0, inertial.Origin.Rpy.R);
            Assert.AreEqual(0, inertial.Origin.Rpy.P);
            Assert.AreEqual(0, inertial.Origin.Rpy.Y);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructInertialNullOrigin()
        {
            Inertial inertial = new Inertial(null, new Mass(1), new Inertia(0, 0, 0, 0, 0, 0));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructInertialNullMass()
        {
            Inertial inertial = new Inertial(new Origin(), null, new Inertia(0, 0, 0, 0, 0, 0));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructInertialNullInertia()
        {
            Inertial inertial = new Inertial(new Origin(), new Mass(1), null);
        }
    }
}
