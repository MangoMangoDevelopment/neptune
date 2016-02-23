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
            Assert.AreEqual(0, collision.Origin.Xyz.X);
            Assert.AreEqual(0, collision.Origin.Xyz.Y);
            Assert.AreEqual(0, collision.Origin.Xyz.Z);
            Assert.AreEqual(0, collision.Origin.Rpy.R);
            Assert.AreEqual(0, collision.Origin.Rpy.P);
            Assert.AreEqual(0, collision.Origin.Rpy.Y);
        }
    }
}
