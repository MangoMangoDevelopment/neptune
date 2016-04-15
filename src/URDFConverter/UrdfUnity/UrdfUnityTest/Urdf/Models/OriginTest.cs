using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models;
using UrdfUnity.Urdf.Models.Attributes;

namespace UrdfUnityTest.Urdf.Models
{
    [TestClass]
    public class OriginTest
    {
        [TestMethod]
        public void ConstructOrigin()
        {
            XyzAttribute xyz = new XyzAttribute(1, 2, 3);
            RpyAttribute rpy = new RpyAttribute(4, 5, 6);
            Origin origin = new Origin.Builder().SetXyz(xyz).SetRpy(rpy).Build();

            Assert.AreEqual(xyz, origin.Xyz);
            Assert.AreEqual(rpy, origin.Rpy);
        }

        [TestMethod]
        public void ConstructXyzOrigin()
        {
            XyzAttribute xyz = new XyzAttribute(1, 2, 3);
            Origin origin = new Origin.Builder().SetXyz(xyz).Build();

            Assert.AreEqual(xyz, origin.Xyz);
            Assert.AreEqual(0, origin.Rpy.R);
            Assert.AreEqual(0, origin.Rpy.P);
            Assert.AreEqual(0, origin.Rpy.Y);
        }

        [TestMethod]
        public void ConstructRpyOrigin()
        {
            RpyAttribute rpy = new RpyAttribute(4, 5, 6);
            Origin origin = new Origin.Builder().SetRpy(rpy).Build();
            
            Assert.AreEqual(rpy, origin.Rpy);
            Assert.AreEqual(0, origin.Xyz.X);
            Assert.AreEqual(0, origin.Xyz.Y);
            Assert.AreEqual(0, origin.Xyz.Z);
        }

        [TestMethod]
        public void ConstructDefaultOrigin()
        {
            Origin origin = new Origin();

            Assert.AreEqual(0, origin.Xyz.X);
            Assert.AreEqual(0, origin.Xyz.Y);
            Assert.AreEqual(0, origin.Xyz.Z);
            Assert.AreEqual(0, origin.Rpy.R);
            Assert.AreEqual(0, origin.Rpy.P);
            Assert.AreEqual(0, origin.Rpy.Y);
        }

        [TestMethod]
        public void ConstructDefaultOriginWithBuilder()
        {
            Origin origin = new Origin.Builder().Build();

            Assert.AreEqual(0, origin.Xyz.X);
            Assert.AreEqual(0, origin.Xyz.Y);
            Assert.AreEqual(0, origin.Xyz.Z);
            Assert.AreEqual(0, origin.Rpy.R);
            Assert.AreEqual(0, origin.Rpy.P);
            Assert.AreEqual(0, origin.Rpy.Y);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructDefaultOriginWithBuilderNullXyz()
        {
            Origin.Builder builder = new Origin.Builder().SetXyz(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructDefaultOriginWithBuilderNullRpy()
        {
            Origin.Builder builder = new Origin.Builder().SetRpy(null);
        }

        [TestMethod]
        public void EqualsAndHash()
        {
            Origin origin = new Origin.Builder().SetXyz(new XyzAttribute(1, 2, 3)).Build();
            Origin same = new Origin.Builder().SetXyz(new XyzAttribute(1, 2, 3)).SetRpy(new RpyAttribute()).Build();
            Origin diff = new Origin();

            Assert.IsTrue(origin.Equals(origin));
            Assert.IsFalse(origin.Equals(null));
            Assert.IsTrue(origin.Equals(same));
            Assert.IsTrue(same.Equals(origin));
            Assert.IsFalse(origin.Equals(diff));
            Assert.AreEqual(origin.GetHashCode(), same.GetHashCode());
            Assert.AreNotEqual(origin.GetHashCode(), diff.GetHashCode());
        }
    }
}
