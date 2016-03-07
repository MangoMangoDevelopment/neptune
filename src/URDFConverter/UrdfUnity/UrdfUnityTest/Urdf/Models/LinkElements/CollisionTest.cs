using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models;
using UrdfUnity.Urdf.Models.LinkElements;
using UrdfUnity.Urdf.Models.LinkElements.GeometryElements;

namespace UrdfUnityTest.Urdf.Models.LinkElements
{
    [TestClass]
    public class CollisionTest
    {
        [TestMethod]
        public void ConstructCollision()
        {
            Origin origin = new Origin();
            Geometry geometry = new Geometry(new Sphere(1));
            Collision collision = new Collision(origin, geometry);

            Assert.AreEqual(origin, collision.Origin);
            Assert.AreEqual(geometry, collision.Geometry);
        }

        [TestMethod]
        public void ConstructCollisionNoOrigin()
        {
            Geometry geometry = new Geometry(new Sphere(1));
            Collision collision = new Collision(geometry);
            
            Assert.AreEqual(geometry, collision.Geometry);
            Assert.AreEqual(new Origin(), collision.Origin);
        }

        [TestMethod]
        public void EqualsAndHash()
        {
            Collision collision = new Collision(new Geometry(new Sphere(1)));
            Collision same = new Collision(new Geometry(new Sphere(1)));
            Collision diff = new Collision(new Origin.Builder().SetXyz(new XyzAttribute(1, 2, 3)).Build(), 
                new Geometry(new Sphere(1)));

            Assert.IsTrue(collision.Equals(collision));
            Assert.IsFalse(collision.Equals(null));
            Assert.IsTrue(collision.Equals(same));
            Assert.IsTrue(same.Equals(collision));
            Assert.IsFalse(collision.Equals(diff));
            Assert.AreEqual(collision.GetHashCode(), same.GetHashCode());
            Assert.AreNotEqual(collision.GetHashCode(), diff.GetHashCode());
        }
    }
}
