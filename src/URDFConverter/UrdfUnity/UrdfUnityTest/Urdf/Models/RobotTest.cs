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
        public void DefaultName()
        {
            Assert.AreEqual("missing_name", Robot.DEFAULT_NAME);
        }

        [TestMethod]
        public void ConstructRobot()
        {
            string name = "robot";
            Dictionary<string, Link> links = new Dictionary<string, Link>();
            Dictionary<string, Joint> joints = new Dictionary<string, Joint>();
            Robot robot = new Robot(name, links, joints);

            Link link1 = new Link.Builder("link1").Build();
            Link link2 = new Link.Builder("link2").Build();
            Link link3 = new Link.Builder("link3").Build();
            Joint joint1 = new Joint.Builder("joint1", Joint.JointType.Continuous, link1, link2).Build();
            Joint joint2 = new Joint.Builder("joint2", Joint.JointType.Continuous, link1, link3).Build();

            links.Add(link1.Name, link1);
            links.Add(link2.Name, link2);
            links.Add(link3.Name, link3);
            joints.Add(joint1.Name, joint1);
            joints.Add(joint2.Name, joint2);

            Assert.AreEqual(name, robot.Name);
            Assert.AreEqual(links, robot.Links);
            Assert.AreEqual(joints, robot.Joints);

            foreach (Joint joint in robot.Joints.Values)
            {
                Assert.IsTrue(robot.Links.ContainsValue(joint.Parent));
                Assert.IsTrue(robot.Links.ContainsValue(joint.Child));
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
            Robot robot = new Robot("robot", null, new Dictionary<string, Joint>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructRobotNullJointks()
        {
            Robot robot = new Robot("robot", new Dictionary<string, Link>(), null);
        }

        [TestMethod]
        public void EqualsAndHash()
        {
            Dictionary<string, Link> links = new Dictionary<string, Link>();
            Dictionary<string, Link> differentLinks = new Dictionary<string, Link>();
            Dictionary<string, Link> mostlySameLinks = new Dictionary<string, Link>();
            Dictionary<string, Joint> joints = new Dictionary<string, Joint>();
            Dictionary<string, Joint> mostlySameJoints = new Dictionary<string, Joint>();
            Dictionary<string, Joint> differentJoints = new Dictionary<string, Joint>();


            Link link1 = new Link.Builder("link1").Build();
            Link link2 = new Link.Builder("link2").Build();
            Link link3 = new Link.Builder("link3").Build();
            Joint joint1 = new Joint.Builder("joint1", Joint.JointType.Continuous, link1, link2).Build();
            Joint joint2 = new Joint.Builder("joint2", Joint.JointType.Continuous, link1, link3).Build();
            Link differentLink1 = new Link.Builder("different_link1").Build();
            Link differentLink2 = new Link.Builder("different_link2").Build();
            Joint differentJoint = new Joint.Builder("differnt_joint", Joint.JointType.Continuous, differentLink1, differentLink2).Build();

            links.Add(link1.Name, link1);
            links.Add(link2.Name, link2);
            links.Add(link3.Name, link3);
            joints.Add(joint1.Name, joint1);
            joints.Add(joint2.Name, joint2);
            mostlySameLinks.Add(link1.Name, link1);
            mostlySameLinks.Add(link2.Name, link2);
            mostlySameJoints.Add(joint1.Name, joint1);
            differentLinks.Add(differentLink1.Name, differentLink1);
            differentLinks.Add(differentLink2.Name, differentLink2);
            differentJoints.Add(differentJoint.Name, differentJoint);

            Robot robot = new Robot("robot", links, joints);
            Robot same1 = new Robot("robot", links, joints);
            Robot same2 = new Robot("robot", new Dictionary<string, Link>(links), new Dictionary<string, Joint>(joints));
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
