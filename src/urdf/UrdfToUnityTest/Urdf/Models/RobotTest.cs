using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfToUnity.Urdf.Models;
using UrdfToUnity.Urdf.Models.Attributes;
using UrdfToUnity.Urdf.Models.Links;

namespace UrdfToUnityTest.Urdf.Models
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
        public void ConstructRobotNullJoints()
        {
            Robot robot = new Robot("robot", new Dictionary<string, Link>(), null);
        }

        [TestMethod]
        public void AddComponent()
        {
            Dictionary<string, Link> links = new Dictionary<string, Link>();
            Dictionary<string, Joint> joints = new Dictionary<string, Joint>();
            Robot robot = new Robot("robot", links, joints);
            Component component = new Component("component", "file");
            string parentName = "parent";

            links.Add(parentName, new Link.Builder(parentName).Build());
            string result = robot.AddComponent(component, parentName, new XyzAttribute(), new RpyAttribute());

            Assert.AreEqual(component.Name, result);
            Assert.AreEqual(2, robot.Links.Count);
            Assert.AreEqual(1, robot.Joints.Count);
            Assert.IsTrue(robot.Links.ContainsKey(parentName));
            Assert.IsTrue(robot.Links.ContainsKey(component.Name));
            Assert.IsTrue(robot.Joints.ContainsKey($"{component.Name}_joint"));
            Assert.AreEqual(Geometry.Shapes.Mesh, robot.Links[component.Name].Visual[0].Geometry.Shape);
            Assert.AreEqual(component.FileName, robot.Links[component.Name].Visual[0].Geometry.Mesh.FileName);
        }

        [TestMethod]
        public void AddComponentParentDoesNotExist()
        {
            Robot robot = new Robot("robot", new Dictionary<string, Link>(), new Dictionary<string, Joint>());
            Component component = new Component("component", "file");

            string result = robot.AddComponent(component, "parent does not exist", new XyzAttribute(), new RpyAttribute());

            Assert.AreEqual(null, result);
            Assert.AreEqual(0, robot.Links.Count);
            Assert.AreEqual(0, robot.Joints.Count);
            Assert.IsFalse(robot.Links.ContainsKey(component.Name));
        }

        [TestMethod]
        public void AddComponentNonUniqueName()
        {
            Dictionary<string, Link> links = new Dictionary<string, Link>();
            Dictionary<string, Joint> joints = new Dictionary<string, Joint>();
            Robot robot = new Robot("robot", links, joints);
            string parentName1 = "parent";
            string parentName2 = "parent_1";
            string jointName = $"{parentName1}_joint";
            string childName = parentName1;
            string expectedUniqueLinkName = $"{childName}_2";
            string expectedUniqueJointName = $"{jointName}_1";
            Component component = new Component(childName, "file");

            links.Add(parentName1, new Link.Builder(parentName1).Build());
            links.Add(parentName2, new Link.Builder(parentName2).Build());
            joints.Add(jointName, new Joint.Builder(jointName, Joint.JointType.Continuous, links[parentName1], links[parentName2]).Build());
            string result = robot.AddComponent(component, parentName2, new XyzAttribute(), new RpyAttribute());

            Assert.AreEqual(expectedUniqueLinkName, result);
            Assert.AreEqual(3, robot.Links.Count);
            Assert.AreEqual(2, robot.Joints.Count);
            Assert.IsTrue(robot.Links.ContainsKey(parentName1));
            Assert.IsTrue(robot.Links.ContainsKey(parentName2));
            Assert.IsTrue(robot.Links.ContainsKey(expectedUniqueLinkName));
            Assert.IsTrue(robot.Joints.ContainsKey(jointName));
            Assert.IsTrue(robot.Joints.ContainsKey(expectedUniqueJointName));
            Assert.AreEqual(Geometry.Shapes.Mesh, robot.Links[expectedUniqueLinkName].Visual[0].Geometry.Shape);
            Assert.AreEqual(component.FileName, robot.Links[expectedUniqueLinkName].Visual[0].Geometry.Mesh.FileName);
        }

        [TestMethod]
        public void ToStringRobot()
        {
            Dictionary<string, Link> links = new Dictionary<string, Link>();
            Dictionary<string, Joint> joints = new Dictionary<string, Joint>();
            Robot robot = new Robot("robo", links, joints);

            Link link1 = new Link.Builder("link1").Build();
            Link link2 = new Link.Builder("link2").Build();
            Joint joint1 = new Joint.Builder("joint1", Joint.JointType.Fixed, link1, link2).Build();

            links.Add(link1.Name, link1);
            links.Add(link2.Name, link2);
            joints.Add(joint1.Name, joint1);

            Assert.AreEqual("<robot name=\"robo\">\r\n<link name=\"link1\"/>\r\n<link name=\"link2\"/>\r\n<joint name=\"joint1\" type=\"fixed\">\r\n<parent link=\"link1\"/>\r\n<child link=\"link2\"/>\r\n</joint>\r\n</robot>",
                robot.ToString().Replace("  ", ""));
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
