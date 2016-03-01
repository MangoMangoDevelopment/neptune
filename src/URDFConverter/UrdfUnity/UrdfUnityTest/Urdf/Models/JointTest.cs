using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf.Models;
using UrdfUnity.Urdf.Models.JointElements;

namespace UrdfUnityTest.Urdf.Models
{
    [TestClass]
    public class JointTest
    {
        private static readonly string TEST_JOINT_NAME = "joint";
        private static readonly Joint.JointType TEST_JOINT_TYPE = Joint.JointType.Fixed;
        private static readonly Link TEST_PARENT_LINK = new Link.Builder("parent").Build();
        private static readonly Link TEST_CHILD_LINK = new Link.Builder("child").Build();
        private static readonly Joint.Builder TEST_BUILDER = new Joint.Builder(TEST_JOINT_NAME,
            TEST_JOINT_TYPE, TEST_PARENT_LINK, TEST_CHILD_LINK);


        [TestMethod]
        public void ConstructJointRequiredProperties()
        {
            Joint.Builder builder = new Joint.Builder(TEST_JOINT_NAME, TEST_JOINT_TYPE, TEST_PARENT_LINK, TEST_CHILD_LINK);
            Joint joint = builder.Build();

            Assert.AreEqual(TEST_JOINT_NAME, joint.Name);
            Assert.AreEqual(TEST_JOINT_TYPE, joint.Type);
            Assert.AreEqual(TEST_PARENT_LINK, joint.Parent);
            Assert.AreEqual(TEST_CHILD_LINK, joint.Child);

            Assert.IsNotNull(joint.Origin);
            Assert.AreEqual(0, joint.Origin.Xyz.X);
            Assert.AreEqual(0, joint.Origin.Xyz.Y);
            Assert.AreEqual(0, joint.Origin.Xyz.Z);
            Assert.AreEqual(0, joint.Origin.Rpy.R);
            Assert.AreEqual(0, joint.Origin.Rpy.P);
            Assert.AreEqual(0, joint.Origin.Rpy.Y);
            Assert.IsNotNull(joint.Axis);
            Assert.AreEqual(1, joint.Axis.Xyz.X);
            Assert.AreEqual(0, joint.Axis.Xyz.Z);
            Assert.AreEqual(0, joint.Axis.Xyz.Z);

            Assert.IsNull(joint.Calibration);
            Assert.IsNull(joint.Dynamics);
            Assert.IsNull(joint.Limit);
            Assert.IsNull(joint.Mimic);
            Assert.IsNull(joint.SafetyController);
        }

        [TestMethod]
        public void ConstructJointAllProperties()
        {
            Axis axis = new Axis(new XyzAttribute(1, 2, 3));
            Calibration calibration = new Calibration(1, 2);
            Dynamics dynamics = new Dynamics(1, 2);
            Limit limit = new Limit(1, 2);
            Mimic mimic = new Mimic(new Joint.Builder("mimic", TEST_JOINT_TYPE, TEST_PARENT_LINK, TEST_CHILD_LINK).Build());
            Origin origin = new Origin.Builder().SetXyz(new XyzAttribute(1, 2, 3)).SetRpy(new RpyAttribute(1, 2, 3)).Build();
            SafetyController safetyController = new SafetyController(1);

            Joint.Builder builder = new Joint.Builder(TEST_JOINT_NAME, TEST_JOINT_TYPE, TEST_PARENT_LINK, TEST_CHILD_LINK);
            builder.SetAxis(axis);
            builder.SetCalibration(calibration);
            builder.SetDynamics(dynamics);
            builder.SetLimit(limit);
            builder.SetMimic(mimic);
            builder.SetOrigin(origin);
            builder.SetSafetyController(safetyController);

            Joint joint = builder.Build();

            Assert.AreEqual(TEST_JOINT_NAME, joint.Name);
            Assert.AreEqual(TEST_JOINT_TYPE, joint.Type);
            Assert.AreEqual(TEST_PARENT_LINK, joint.Parent);
            Assert.AreEqual(TEST_CHILD_LINK, joint.Child);
            
            Assert.AreEqual(axis, joint.Axis);
            Assert.AreEqual(axis.Xyz.X, joint.Axis.Xyz.X);
            Assert.AreEqual(axis.Xyz.Y, joint.Axis.Xyz.Y);
            Assert.AreEqual(axis.Xyz.Z, joint.Axis.Xyz.Z);

            Assert.AreEqual(calibration, joint.Calibration);
            Assert.AreEqual(calibration.Rising, joint.Calibration.Rising);
            Assert.AreEqual(calibration.Falling, joint.Calibration.Falling);

            Assert.AreEqual(dynamics, joint.Dynamics);
            Assert.AreEqual(dynamics.Damping, joint.Dynamics.Damping);
            Assert.AreEqual(dynamics.Friction, joint.Dynamics.Friction);

            Assert.AreEqual(limit, joint.Limit);
            Assert.AreEqual(limit.Lower, joint.Limit.Lower);
            Assert.AreEqual(limit.Upper, joint.Limit.Upper);
            Assert.AreEqual(limit.Effort, joint.Limit.Effort);
            Assert.AreEqual(limit.Velocity, joint.Limit.Velocity);

            Assert.AreEqual(mimic, joint.Mimic);
            Assert.AreEqual(mimic.Joint, joint.Mimic.Joint);
            Assert.AreEqual(mimic.Multiplier, joint.Mimic.Multiplier);
            Assert.AreEqual(mimic.Offset, joint.Mimic.Offset);

            Assert.AreEqual(origin, joint.Origin);
            Assert.AreEqual(origin.Xyz.X, joint.Origin.Xyz.X);
            Assert.AreEqual(origin.Xyz.Y, joint.Origin.Xyz.Y);
            Assert.AreEqual(origin.Xyz.Z, joint.Origin.Xyz.Z);
            Assert.AreEqual(origin.Rpy.R, joint.Origin.Rpy.R);
            Assert.AreEqual(origin.Rpy.P, joint.Origin.Rpy.P);
            Assert.AreEqual(origin.Rpy.Y, joint.Origin.Rpy.Y);

            Assert.AreEqual(safetyController, joint.SafetyController);
            Assert.AreEqual(safetyController.SoftLowerLimit, joint.SafetyController.SoftLowerLimit);
            Assert.AreEqual(safetyController.SoftUpperLimit, joint.SafetyController.SoftUpperLimit);
            Assert.AreEqual(safetyController.KPostition, joint.SafetyController.KPostition);
            Assert.AreEqual(safetyController.KVelocity, joint.SafetyController.KVelocity);
        }

        [TestMethod]
        public void ConstructJointChainBuilderSetters()
        {
            Axis axis = new Axis(new XyzAttribute(1, 2, 3));
            Origin origin = new Origin.Builder().SetXyz(new XyzAttribute(1, 2, 3)).SetRpy(new RpyAttribute(1, 2, 3)).Build();
            SafetyController safetyController = new SafetyController(1);

            Joint.Builder builder = new Joint.Builder(TEST_JOINT_NAME, TEST_JOINT_TYPE, TEST_PARENT_LINK, TEST_CHILD_LINK);
            builder.SetAxis(axis).SetOrigin(origin).SetSafetyController(safetyController);
            Joint joint = builder.Build();

            Assert.AreEqual(TEST_JOINT_NAME, joint.Name);
            Assert.AreEqual(TEST_JOINT_TYPE, joint.Type);
            Assert.AreEqual(TEST_PARENT_LINK, joint.Parent);
            Assert.AreEqual(TEST_CHILD_LINK, joint.Child);
            Assert.AreEqual(axis, joint.Axis);
            Assert.AreEqual(origin, joint.Origin);
            Assert.AreEqual(safetyController, joint.SafetyController);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructBuilderNoName()
        {
            Joint.Builder builder = new Joint.Builder("", TEST_JOINT_TYPE, TEST_PARENT_LINK, TEST_CHILD_LINK);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructBuilderNoParent()
        {
            Joint.Builder builder = new Joint.Builder(TEST_JOINT_NAME, TEST_JOINT_TYPE, null, TEST_CHILD_LINK);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructBuilderNoChild()
        {
            Joint.Builder builder = new Joint.Builder(TEST_JOINT_NAME, TEST_JOINT_TYPE, TEST_PARENT_LINK, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BuilderSetNullAxis()
        {
            TEST_BUILDER.SetAxis(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BuilderSetNullCalibration()
        {
            TEST_BUILDER.SetCalibration(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BuilderSetNullDynamics()
        {
            TEST_BUILDER.SetDynamics(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BuilderSetNullLimit()
        {
            TEST_BUILDER.SetLimit(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BuilderSetNullMimic()
        {
            TEST_BUILDER.SetMimic(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BuilderSetNullOrigin()
        {
            TEST_BUILDER.SetOrigin(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BuilderSetNullSafetyController()
        {
            TEST_BUILDER.SetSafetyController(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BuilderNullLimitPrismaticType()
        {
            Joint.Builder builder = new Joint.Builder(TEST_JOINT_NAME, Joint.JointType.Prismatic, TEST_PARENT_LINK, TEST_CHILD_LINK);
            builder.Build();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BuilderNullLimitRevoluteType()
        {
            Joint.Builder builder = new Joint.Builder(TEST_JOINT_NAME, Joint.JointType.Revolute, TEST_PARENT_LINK, TEST_CHILD_LINK);
            builder.Build();
        }
    }
}
