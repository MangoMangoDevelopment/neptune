using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models;

namespace UrdfUnityTest.Urdf.Models
{
    [TestClass]
    public class RobotTest
    {
        [TestMethod]
        public void ConstructRobot()
        {
            string name = "robot";
            List<Link> links = new List<Link>();
            List<Joint> joints = new List<Joint>();
            Robot robot = new Robot(name, links, joints);

            links.Add(new Link.Builder("link1").Build());
            links.Add(new Link.Builder("link2").Build());
            links.Add(new Link.Builder("link3").Build());
            joints.Add(new Joint.Builder("joint1", Joint.JointType.Continuous, links[0], links[1]).Build());
            joints.Add(new Joint.Builder("joint2", Joint.JointType.Continuous, links[0], links[2]).Build());

            Assert.AreEqual(name, robot.Name);
            Assert.AreEqual(links, robot.Links);
            Assert.AreEqual(joints, robot.Joints);

            foreach(Joint joint in robot.Joints)
            {
                Assert.IsTrue(robot.Links.Contains(joint.Parent));
                Assert.IsTrue(robot.Links.Contains(joint.Child));
            }
        }

        [TestMethod]
        public void ConstructEmptyRobot()
        {
            string name = "robot";
            Robot robot = new Robot(name);

            Assert.AreEqual(name, robot.Name);
            Assert.IsNotNull(robot.Links);
            Assert.IsNotNull(robot.Joints);
            Assert.IsTrue(robot.Links.Count == 0);
            Assert.IsTrue(robot.Joints.Count == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructRobotNullLinks()
        {
            Robot robot = new Robot("robot", null, new List<Joint>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructRobotNullJointks()
        {
            Robot robot = new Robot("robot", new List<Link>(), null);
        }
    }
}
