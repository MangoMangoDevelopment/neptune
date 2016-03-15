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

            foreach (Joint joint in robot.Joints)
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

        [TestMethod]
        public void EqualsAndHash()
        {
            List<Link> links = new List<Link>();
            List<Link> differentLinks = new List<Link>();
            List<Link> mostlySameLinks = new List<Link>();
            List<Joint> joints = new List<Joint>();
            List<Joint> mostlySameJoints = new List<Joint>();
            List<Joint> differentJoints = new List<Joint>();

            links.Add(new Link.Builder("link1").Build());
            links.Add(new Link.Builder("link2").Build());
            links.Add(new Link.Builder("link3").Build());
            joints.Add(new Joint.Builder("joint1", Joint.JointType.Continuous, links[0], links[1]).Build());
            joints.Add(new Joint.Builder("joint2", Joint.JointType.Continuous, links[0], links[2]).Build());
            mostlySameLinks.Add(links[0]);
            mostlySameLinks.Add(links[1]);
            mostlySameJoints.Add(joints[0]);
            differentLinks.Add(new Link.Builder("different_link1").Build());
            differentLinks.Add(new Link.Builder("different_link2").Build());
            differentJoints.Add(new Joint.Builder("differnt_joint", Joint.JointType.Continuous, differentLinks[0], differentLinks[1]).Build());

            Robot robot = new Robot("robot", links, joints);
            Robot same1 = new Robot("robot", links, joints);
            Robot same2 = new Robot("robot", new List<Link>(links), new List<Joint>(joints));
            Robot diff1 = new Robot("different_robot", links, joints);
            Robot diff2 = new Robot("robot", differentLinks, differentJoints);
            Robot diff3 = new Robot("robot", links, mostlySameJoints);
            Robot diff4 = new Robot("robot", mostlySameLinks, joints);

            Assert.IsTrue(robot.Equals(robot));
            Assert.IsFalse(robot.Equals(null));
            Assert.IsTrue(robot.Equals(same1));
            Assert.IsTrue(robot.Equals(same2));
            Assert.IsTrue(same1.Equals(robot));
            Assert.IsTrue(same2.Equals(robot));
            Assert.IsFalse(robot.Equals(diff1));
            Assert.IsFalse(robot.Equals(diff2));
            Assert.IsFalse(robot.Equals(diff3));
            Assert.IsFalse(robot.Equals(diff4));
            Assert.AreEqual(robot.GetHashCode(), robot.GetHashCode());
            Assert.AreEqual(robot.GetHashCode(), same1.GetHashCode());
            Assert.AreEqual(robot.GetHashCode(), same2.GetHashCode());
            Assert.AreNotEqual(robot.GetHashCode(), diff1.GetHashCode());
            Assert.AreNotEqual(robot.GetHashCode(), diff2.GetHashCode());
            Assert.AreNotEqual(robot.GetHashCode(), diff3.GetHashCode());
            Assert.AreNotEqual(robot.GetHashCode(), diff4.GetHashCode());
        }
    }
}
