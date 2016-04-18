using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfToUnity.Parse.Xml;
using UrdfToUnity.Urdf.Models;
using UrdfToUnity.Urdf.Models.Attributes;
using UrdfToUnity.Urdf.Models.Links.Visuals;

namespace UrdfToUnityTest.Parse.Xml
{
    [TestClass]
    public class RobotParserTest
    {
        private readonly RobotParser parser = new RobotParser();
        private readonly XmlDocument xmlDoc = new XmlDocument();


        [TestMethod]
        public void ParseRobot()
        {
            // TODO: Investigate file I/O for tests and use UrdfToUnityTest/TestData/example.urdf
            // TODO: Perhaps using: [DeploymentItem("UrdfToUnityTest\\TestData\\example.urdf")]
            //string filePath = "UrdfToUnityTest\\TestData\\example.urdf";
            //Assert.IsTrue(System.IO.File.Exists(filePath));
            //StreamReader reader = new StreamReader(filePath);

            this.xmlDoc.Load(XmlReader.Create(new StringReader(EXAMPLE_URDF)));
            Robot robot = this.parser.Parse(this.xmlDoc.DocumentElement);

            Assert.AreEqual("physics", robot.Name);

            Assert.IsTrue(robot.Links.ContainsKey("base_link"));
            Assert.IsTrue(robot.Links.ContainsKey("right_leg"));
            Assert.IsTrue(robot.Links.ContainsKey("right_base"));
            Assert.IsTrue(robot.Links.ContainsKey("right_front_wheel"));
            Assert.IsTrue(robot.Links.ContainsKey("right_back_wheel"));
            Assert.IsTrue(robot.Links.ContainsKey("left_leg"));
            Assert.IsTrue(robot.Links.ContainsKey("left_base"));
            Assert.IsTrue(robot.Links.ContainsKey("left_front_wheel"));
            Assert.IsTrue(robot.Links.ContainsKey("left_back_wheel"));
            Assert.IsTrue(robot.Links.ContainsKey("gripper_pole"));
            Assert.IsTrue(robot.Links.ContainsKey("left_gripper"));
            Assert.IsTrue(robot.Links.ContainsKey("left_tip"));
            Assert.IsTrue(robot.Links.ContainsKey("right_gripper"));
            Assert.IsTrue(robot.Links.ContainsKey("right_tip"));
            Assert.IsTrue(robot.Links.ContainsKey("head"));
            Assert.IsTrue(robot.Links.ContainsKey("box"));
            Assert.IsTrue(robot.Joints.ContainsKey("base_to_right_leg"));
            Assert.IsTrue(robot.Joints.ContainsKey("right_base_joint"));
            Assert.IsTrue(robot.Joints.ContainsKey("right_front_wheel_joint"));
            Assert.IsTrue(robot.Joints.ContainsKey("right_back_wheel_joint"));
            Assert.IsTrue(robot.Joints.ContainsKey("base_to_left_leg"));
            Assert.IsTrue(robot.Joints.ContainsKey("left_base_joint"));
            Assert.IsTrue(robot.Joints.ContainsKey("left_front_wheel_joint"));
            Assert.IsTrue(robot.Joints.ContainsKey("left_back_wheel_joint"));
            Assert.IsTrue(robot.Joints.ContainsKey("gripper_extension"));
            Assert.IsTrue(robot.Joints.ContainsKey("left_gripper_joint"));
            Assert.IsTrue(robot.Joints.ContainsKey("left_tip_joint"));
            Assert.IsTrue(robot.Joints.ContainsKey("right_gripper_joint"));
            Assert.IsTrue(robot.Joints.ContainsKey("right_tip_joint"));
            Assert.IsTrue(robot.Joints.ContainsKey("head_swivel"));
            Assert.IsTrue(robot.Joints.ContainsKey("tobox"));

            Material blue = new Material("blue", new Color(new RgbAttribute(0d, 0d, 0.8d), 1));
            Material black = new Material("black", new Color(new RgbAttribute(0d, 0d, 0d), 1));
            Material white = new Material("white", new Color(new RgbAttribute(1d, 1d, 1d), 1));

            Assert.AreEqual(blue, robot.Links["base_link"].Visual[0].Material);
            Assert.AreEqual(white, robot.Links["right_leg"].Visual[0].Material);
            Assert.AreEqual(white, robot.Links["right_base"].Visual[0].Material);
            Assert.AreEqual(black, robot.Links["right_front_wheel"].Visual[0].Material);
            Assert.AreEqual(black, robot.Links["right_back_wheel"].Visual[0].Material);
            Assert.AreEqual(white, robot.Links["left_leg"].Visual[0].Material);
            Assert.AreEqual(white, robot.Links["left_base"].Visual[0].Material);
            Assert.AreEqual(black, robot.Links["left_front_wheel"].Visual[0].Material);
            Assert.AreEqual(black, robot.Links["left_back_wheel"].Visual[0].Material);
            Assert.AreEqual(white, robot.Links["head"].Visual[0].Material);
            Assert.AreEqual(blue, robot.Links["box"].Visual[0].Material);
        }

        /// <summary>
        /// TODO: Investigate file I/O for tests and use UrdfToUnityTest/TestData/example.urdf
        /// The example.urdf is sourced from:
        /// https://github.com/ros/urdf_tutorial/blob/master/urdf/07-physics.urdf
        /// </summary>
        private static readonly string EXAMPLE_URDF = @"<?xml version='1.0'?>
<robot name='physics'>

<material name='blue'>
    <color rgba='0 0 .8 1'/>
  </material>
  <material name='black'>
    <color rgba='0 0 0 1'/>
  </material>
  <material name='white'>
    <color rgba='1 1 1 1'/>
  </material>

  <link name='base_link'>
    <visual>
      <geometry>
        <cylinder length='0.6' radius='0.2'/>
      </geometry>
      <material name='blue'/>
    </visual>
    <collision>
      <geometry>
        <cylinder length='0.6' radius='0.2'/>
      </geometry>
    </collision>
    <inertial>
      <mass value='10'/>
      <inertia ixx='1.0' ixy='0.0' ixz='0.0' iyy='1.0' iyz='0.0' izz='1.0'/>
    </inertial>
  </link>

  <link name='right_leg'>
    <visual>
      <geometry>
        <box size='0.6 .1 .2'/>
      </geometry>
      <origin rpy='0 1.57075 0' xyz='0 0 -0.3'/>
      <material name='white'/>
    </visual>
    <collision>
      <geometry>
        <box size='0.6 .1 .2'/>
      </geometry>
      <origin rpy='0 1.57075 0' xyz='0 0 -0.3'/>
    </collision>
    <inertial>
      <mass value='10'/>
      <inertia ixx='1.0' ixy='0.0' ixz='0.0' iyy='1.0' iyz='0.0' izz='1.0'/>
    </inertial>
  </link>
  <joint name='base_to_right_leg' type='fixed'>
    <parent link='base_link'/>
    <child link='right_leg'/>
    <origin xyz='0 -0.22 .25'/>
  </joint>

  <link name='right_base'>
    <visual>
      <geometry>
        <box size='0.4 .1 .1'/>
      </geometry>
      <material name='white'/>
    </visual>
    <collision>
      <geometry>
        <box size='0.4 .1 .1'/>
      </geometry>
    </collision>
    <inertial>
      <mass value='10'/>
      <inertia ixx='1.0' ixy='0.0' ixz='0.0' iyy='1.0' iyz='0.0' izz='1.0'/>
    </inertial>
  </link>

  <joint name='right_base_joint' type='fixed'>
    <parent link='right_leg'/>
    <child link='right_base'/>
    <origin xyz='0 0 -0.6'/>
  </joint>

  <link name='right_front_wheel'>
    <visual>
      <origin rpy='1.57075 0 0' xyz='0 0 0'/>
      <geometry>
        <cylinder length='0.1' radius='0.035'/>
      </geometry>
      <material name='black'/>
    </visual>
    <collision>
      <origin rpy='1.57075 0 0' xyz='0 0 0'/>
      <geometry>
        <cylinder length='0.1' radius='0.035'/>
      </geometry>
    </collision>
    <inertial>
      <mass value='1'/>
      <inertia ixx='1.0' ixy='0.0' ixz='0.0' iyy='1.0' iyz='0.0' izz='1.0'/>
    </inertial>
  </link>

  <joint name='right_front_wheel_joint' type='continuous'>
    <axis rpy='0 0 0' xyz='0 1 0'/>
    <parent link='right_base'/>
    <child link='right_front_wheel'/>
    <origin rpy='0 0 0' xyz='0.133333333333 0 -0.085'/>
  </joint>

  <link name='right_back_wheel'>
    <visual>
      <origin rpy='1.57075 0 0' xyz='0 0 0'/>
      <geometry>
        <cylinder length='0.1' radius='0.035'/>
      </geometry>
      <material name='black'/>
    </visual>
    <collision>
      <origin rpy='1.57075 0 0' xyz='0 0 0'/>
      <geometry>
        <cylinder length='0.1' radius='0.035'/>
      </geometry>
    </collision>
    <inertial>
      <mass value='1'/>
      <inertia ixx='1.0' ixy='0.0' ixz='0.0' iyy='1.0' iyz='0.0' izz='1.0'/>
    </inertial>
  </link>

  <joint name='right_back_wheel_joint' type='continuous'>
    <axis rpy='0 0 0' xyz='0 1 0'/>
    <parent link='right_base'/>
    <child link='right_back_wheel'/>
    <origin rpy='0 0 0' xyz='-0.133333333333 0 -0.085'/>
  </joint>

  <link name='left_leg'>
    <visual>
      <geometry>
        <box size='0.6 .1 .2'/>
      </geometry>
      <origin rpy='0 1.57075 0' xyz='0 0 -0.3'/>
      <material name='white'/>
    </visual>
    <collision>
      <geometry>
        <box size='0.6 .1 .2'/>
      </geometry>
      <origin rpy='0 1.57075 0' xyz='0 0 -0.3'/>
    </collision>
    <inertial>
      <mass value='10'/>
      <inertia ixx='1.0' ixy='0.0' ixz='0.0' iyy='1.0' iyz='0.0' izz='1.0'/>
    </inertial>
  </link>

  <joint name='base_to_left_leg' type='fixed'>
    <parent link='base_link'/>
    <child link='left_leg'/>
    <origin xyz='0 0.22 .25'/>
  </joint>

  <link name='left_base'>
    <visual>
      <geometry>
        <box size='0.4 .1 .1'/>
      </geometry>
      <material name='white'/>
    </visual>
    <collision>
      <geometry>
        <box size='0.4 .1 .1'/>
      </geometry>
    </collision>
    <inertial>
      <mass value='10'/>
      <inertia ixx='1.0' ixy='0.0' ixz='0.0' iyy='1.0' iyz='0.0' izz='1.0'/>
    </inertial>
  </link>

  <joint name='left_base_joint' type='fixed'>
    <parent link='left_leg'/>
    <child link='left_base'/>
    <origin xyz='0 0 -0.6'/>
  </joint>

  <link name='left_front_wheel'>
    <visual>
      <origin rpy='1.57075 0 0' xyz='0 0 0'/>
      <geometry>
        <cylinder length='0.1' radius='0.035'/>
      </geometry>
      <material name='black'/>
    </visual>
    <collision>
      <origin rpy='1.57075 0 0' xyz='0 0 0'/>
      <geometry>
        <cylinder length='0.1' radius='0.035'/>
      </geometry>
    </collision>
    <inertial>
      <mass value='1'/>
      <inertia ixx='1.0' ixy='0.0' ixz='0.0' iyy='1.0' iyz='0.0' izz='1.0'/>
    </inertial>
  </link>

  <joint name='left_front_wheel_joint' type='continuous'>
    <axis rpy='0 0 0' xyz='0 1 0'/>
    <parent link='left_base'/>
    <child link='left_front_wheel'/>
    <origin rpy='0 0 0' xyz='0.133333333333 0 -0.085'/>
  </joint>

  <link name='left_back_wheel'>
    <visual>
      <origin rpy='1.57075 0 0' xyz='0 0 0'/>
      <geometry>
        <cylinder length='0.1' radius='0.035'/>
      </geometry>
      <material name='black'/>
    </visual>
    <collision>
      <origin rpy='1.57075 0 0' xyz='0 0 0'/>
      <geometry>
        <cylinder length='0.1' radius='0.035'/>
      </geometry>
    </collision>
    <inertial>
      <mass value='1'/>
      <inertia ixx='1.0' ixy='0.0' ixz='0.0' iyy='1.0' iyz='0.0' izz='1.0'/>
    </inertial>
  </link>

  <joint name='left_back_wheel_joint' type='continuous'>
    <axis rpy='0 0 0' xyz='0 1 0'/>
    <parent link='left_base'/>
    <child link='left_back_wheel'/>
    <origin rpy='0 0 0' xyz='-0.133333333333 0 -0.085'/>
  </joint>

  <joint name='gripper_extension' type='prismatic'>
    <parent link='base_link'/>
    <child link='gripper_pole'/>
    <limit effort='1000.0' lower='-0.38' upper='0' velocity='0.5'/>
    <origin rpy='0 0 0' xyz='0.19 0 .2'/>
  </joint>

  <link name='gripper_pole'>
    <visual>
      <geometry>
        <cylinder length='0.2' radius='.01'/>
      </geometry>
      <origin rpy='0 1.57075 0 ' xyz='0.1 0 0'/>
    </visual>
    <collision>
      <geometry>
        <cylinder length='0.2' radius='.01'/>
      </geometry>
      <origin rpy='0 1.57075 0 ' xyz='0.1 0 0'/>
    </collision>
    <inertial>
      <mass value='0.05'/>
      <inertia ixx='1.0' ixy='0.0' ixz='0.0' iyy='1.0' iyz='0.0' izz='1.0'/>
    </inertial>
  </link>

  <joint name='left_gripper_joint' type='revolute'>
    <axis xyz='0 0 1'/>
    <limit effort='1000.0' lower='0.0' upper='0.548' velocity='0.5'/>
    <origin rpy='0 0 0' xyz='0.2 0.01 0'/>
    <parent link='gripper_pole'/>
    <child link='left_gripper'/>
  </joint>

  <link name='left_gripper'>
    <visual>
      <origin rpy='0.0 0 0' xyz='0 0 0'/>
      <geometry>
        <mesh filename='package://pr2_description/meshes/gripper_v0/l_finger.dae'/>
      </geometry>
    </visual>
    <collision>
      <geometry>
        <mesh filename='package://pr2_description/meshes/gripper_v0/l_finger.dae'/>
      </geometry>
      <origin rpy='0.0 0 0' xyz='0 0 0'/>
    </collision>
    <inertial>
      <mass value='0.05'/>
      <inertia ixx='1.0' ixy='0.0' ixz='0.0' iyy='1.0' iyz='0.0' izz='1.0'/>
    </inertial>
  </link>

  <joint name='left_tip_joint' type='fixed'>
    <parent link='left_gripper'/>
    <child link='left_tip'/>
  </joint>

  <link name='left_tip'>
    <visual>
      <origin rpy='0.0 0 0' xyz='0.09137 0.00495 0'/>
      <geometry>
        <mesh filename='package://pr2_description/meshes/gripper_v0/l_finger_tip.dae'/>
      </geometry>
    </visual>
    <collision>
      <geometry>
        <mesh filename='package://pr2_description/meshes/gripper_v0/l_finger_tip.dae'/>
      </geometry>
      <origin rpy='0.0 0 0' xyz='0.09137 0.00495 0'/>
    </collision>
    <inertial>
      <mass value='0.05'/>
      <inertia ixx='1.0' ixy='0.0' ixz='0.0' iyy='1.0' iyz='0.0' izz='1.0'/>
    </inertial>
  </link>

  <joint name='right_gripper_joint' type='revolute'>
    <axis xyz='0 0 -1'/>
    <limit effort='1000.0' lower='0.0' upper='0.548' velocity='0.5'/>
    <origin rpy='0 0 0' xyz='0.2 -0.01 0'/>
    <parent link='gripper_pole'/>
    <child link='right_gripper'/>
  </joint>

  <link name='right_gripper'>
    <visual>
      <origin rpy='-3.1415 0 0' xyz='0 0 0'/>
      <geometry>
        <mesh filename='package://pr2_description/meshes/gripper_v0/l_finger.dae'/>
      </geometry>
    </visual>
    <collision>
      <geometry>
        <mesh filename='package://pr2_description/meshes/gripper_v0/l_finger.dae'/>
      </geometry>
      <origin rpy='-3.1415 0 0' xyz='0 0 0'/>
    </collision>
    <inertial>
      <mass value='0.05'/>
      <inertia ixx='1.0' ixy='0.0' ixz='0.0' iyy='1.0' iyz='0.0' izz='1.0'/>
    </inertial>
  </link>

  <joint name='right_tip_joint' type='fixed'>
    <parent link='right_gripper'/>
    <child link='right_tip'/>
  </joint>

  <link name='right_tip'>
    <visual>
      <origin rpy='-3.1415 0 0' xyz='0.09137 0.00495 0'/>
      <geometry>
        <mesh filename='package://pr2_description/meshes/gripper_v0/l_finger_tip.dae'/>
      </geometry>
    </visual>
    <collision>
      <geometry>
        <mesh filename='package://pr2_description/meshes/gripper_v0/l_finger_tip.dae'/>
      </geometry>
      <origin rpy='-3.1415 0 0' xyz='0.09137 0.00495 0'/>
    </collision>
    <inertial>
      <mass value='0.05'/>
      <inertia ixx='1.0' ixy='0.0' ixz='0.0' iyy='1.0' iyz='0.0' izz='1.0'/>
    </inertial>
  </link>

  <link name='head'>
    <visual>
      <geometry>
        <sphere radius='0.2'/>
      </geometry>
      <material name='white'/>
    </visual>
    <collision>
      <geometry>
        <sphere radius='0.2'/>
      </geometry>
    </collision>
    <inertial>
      <mass value='2'/>
      <inertia ixx='1.0' ixy='0.0' ixz='0.0' iyy='1.0' iyz='0.0' izz='1.0'/>
    </inertial>
  </link>

  <joint name='head_swivel' type='continuous'>
    <parent link='base_link'/>
    <child link='head'/>
    <axis xyz='0 0 1'/>
    <origin xyz='0 0 0.3'/>
  </joint>

  <link name='box'>
    <visual>
      <geometry>
        <box size='.08 .08 .08'/>
      </geometry>
      <material name='blue'/>
      <origin xyz='-0.04 0 0'/>
    </visual>
    <collision>
      <geometry>
        <box size='.08 .08 .08'/>
      </geometry>
    </collision>
    <inertial>
      <mass value='1'/>
      <inertia ixx='1.0' ixy='0.0' ixz='0.0' iyy='1.0' iyz='0.0' izz='1.0'/>
    </inertial>
  </link>

  <joint name='tobox' type='fixed'>
    <parent link='head'/>
    <child link='box'/>
    <origin xyz='0.1814 0 0.1414'/>
  </joint>

</robot>";
    }
}
