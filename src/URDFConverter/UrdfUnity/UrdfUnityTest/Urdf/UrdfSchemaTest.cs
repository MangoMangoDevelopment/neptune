using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrdfUnity.Urdf;

namespace UrdfUnityTest.Urdf
{
    [TestClass]
    public class UrdfSchemaTest
    {
        [TestMethod]
        public void SchemaDefinitions()
        {
            Assert.AreEqual("robot", UrdfSchema.ROBOT_ELEMENT_NAME);
            Assert.AreEqual("name", UrdfSchema.NAME_ATTRIBUTE_NAME);
            Assert.AreEqual("link", UrdfSchema.LINK_ELEMENT_NAME);
            Assert.AreEqual("joint", UrdfSchema.JOINT_ELEMENT_NAME);
            Assert.AreEqual("type", UrdfSchema.JOINT_TYPE_ATTRIBUTE_NAME);
            Assert.AreEqual("filename", UrdfSchema.FILE_NAME_ATTRIBUTE_NAME);
            Assert.AreEqual("origin", UrdfSchema.ORIGIN_ELEMENT_NAME);
            Assert.AreEqual("xyz", UrdfSchema.XYZ_ATTRIBUTE_NAME);
            Assert.AreEqual("rpy", UrdfSchema.RPY_ATTRIBUTE_NAME);

            Assert.AreEqual("parent", UrdfSchema.PARENT_ELEMENT_NAME);
            Assert.AreEqual("child", UrdfSchema.CHILD_ELEMENT_NAME);
            Assert.AreEqual("link", UrdfSchema.LINK_ATTRIBUTE_NAME);
            Assert.AreEqual("axis", UrdfSchema.AXIS_ELEMENT_NAME);
            Assert.AreEqual("calibration", UrdfSchema.CALIBRATION_ELEMENT_NAME);
            Assert.AreEqual("rising", UrdfSchema.RISING_ATTRIBUTE_NAME);
            Assert.AreEqual("falling", UrdfSchema.FALLING_ATTRIBUTE_NAME);
            Assert.AreEqual("dynamics", UrdfSchema.DYNAMICS_ELEMENT_NAME);
            Assert.AreEqual("damping", UrdfSchema.DAMPING_ATTRIBUTE_NAME);
            Assert.AreEqual("friction", UrdfSchema.FRICTION_ATTRIBUTE_NAME);
            Assert.AreEqual("limit", UrdfSchema.LIMIT_ELEMENT_NAME);
            Assert.AreEqual("lower", UrdfSchema.LOWER_ATTRIBUTE_NAME);
            Assert.AreEqual("upper", UrdfSchema.UPPER_ATTRIBUTE_NAME);
            Assert.AreEqual("effort", UrdfSchema.EFFORT_ATTRIBUTE_NAME);
            Assert.AreEqual("velocity", UrdfSchema.VELOCITY_ATTRIBUTE_NAME);
            Assert.AreEqual("mimic", UrdfSchema.MIMIC_ELEMENT_NAME);
            Assert.AreEqual("joint", UrdfSchema.JOINT_ATTRIBUTE_NAME);
            Assert.AreEqual("multiplier", UrdfSchema.MULTIPLIER_ATTRIBUTE_NAME);
            Assert.AreEqual("offset", UrdfSchema.OFFSET_ATTRIBUTE_NAME);
            Assert.AreEqual("safety_controller", UrdfSchema.SAFETY_CONTROLLER_ELEMENT_NAME);
            Assert.AreEqual("soft_lower_limit", UrdfSchema.LOWER_LIMIT_ATTRIBUTE_NAME);
            Assert.AreEqual("soft_upper_limit", UrdfSchema.UPPER_LIMIT_ATTRIBUTE_NAME);
            Assert.AreEqual("k_position", UrdfSchema.K_POSITION_ATTRIBUTE_NAME);
            Assert.AreEqual("k_velocity", UrdfSchema.K_VELOCITY_ATTRIBUTE_NAME);

            Assert.AreEqual("collision", UrdfSchema.COLLISION_ELEMENT_NAME);
            Assert.AreEqual("inertial", UrdfSchema.INERTIAL_ELEMENT_NAME);
            Assert.AreEqual("mass", UrdfSchema.MASS_ELEMENT_NAME);
            Assert.AreEqual("value", UrdfSchema.MASS_VALUE_ATTRIBUTE_NAME);
            Assert.AreEqual("inertia", UrdfSchema.INERTIA_ELEMENT_NAME);
            Assert.AreEqual("ixx", UrdfSchema.IXX_ATTRIBUTE_NAME);
            Assert.AreEqual("ixy", UrdfSchema.IXY_ATTRIBUTE_NAME);
            Assert.AreEqual("ixz", UrdfSchema.IXZ_ATTRIBUTE_NAME);
            Assert.AreEqual("iyy", UrdfSchema.IYY_ATTRIBUTE_NAME);
            Assert.AreEqual("iyz", UrdfSchema.IYZ_ATTRIBUTE_NAME);
            Assert.AreEqual("izz", UrdfSchema.IZZ_ATTRIBUTE_NAME);
            Assert.AreEqual("visual", UrdfSchema.VISUAL_ELEMENT_NAME);
            Assert.AreEqual("material", UrdfSchema.MATERIAL_ELEMENT_NAME);
            Assert.AreEqual("color", UrdfSchema.COLOR_ELEMENT_NAME);
            Assert.AreEqual("texture", UrdfSchema.TEXTURE_ELEMENT_NAME);
            Assert.AreEqual("rgb", UrdfSchema.RGB_ATTRIBUTE_NAME);
            Assert.AreEqual("alpha", UrdfSchema.ALPHA_ATTRIBUTE_NAME);
            Assert.AreEqual("rgba", UrdfSchema.RGBA_ATTRIBUTE_NAME);
            Assert.AreEqual("geometry", UrdfSchema.GEOMETRY_ELEMENT_NAME);
            Assert.AreEqual("size", UrdfSchema.SIZE_ATTRIBUTE_NAME);
            Assert.AreEqual("radius", UrdfSchema.RADIUS_ATTRIBUTE_NAME);
            Assert.AreEqual("box", UrdfSchema.BOX_ELEMENT_NAME);
            Assert.AreEqual("cylinder", UrdfSchema.CYLINDER_ELEMENT_NAME);
            Assert.AreEqual("length", UrdfSchema.LENGTH_ATTRIBUTE_NAME);
            Assert.AreEqual("sphere", UrdfSchema.SPHERE_ELEMENT_NAME);
            Assert.AreEqual("mesh", UrdfSchema.MESH_ELEMENT_NAME);
            Assert.AreEqual("scale", UrdfSchema.SCALE_ATTRIBUTE_NAME);
        }
    }
}
