namespace UrdfToUnity.Urdf
{
    /// <summary>
    /// Stores the string names of all URDF elements and attributes.
    /// </summary>
    public static class UrdfSchema
    {
        public static readonly string ROBOT_ELEMENT_NAME = "robot";
        public static readonly string LINK_ELEMENT_NAME = "link";
        public static readonly string JOINT_ELEMENT_NAME = "joint";
        public static readonly string JOINT_TYPE_ATTRIBUTE_NAME = "type";

        #region Common element and attribute names

        public static readonly string NAME_ATTRIBUTE_NAME = "name";
        public static readonly string FILE_NAME_ATTRIBUTE_NAME = "filename";

        public static readonly string ORIGIN_ELEMENT_NAME = "origin";
        public static readonly string XYZ_ATTRIBUTE_NAME = "xyz";
        public static readonly string RPY_ATTRIBUTE_NAME = "rpy";

        #endregion

        #region Joint specific sub-elements

        public static readonly string PARENT_ELEMENT_NAME = "parent";
        public static readonly string CHILD_ELEMENT_NAME = "child";
        public static readonly string LINK_ATTRIBUTE_NAME = "link";

        public static readonly string AXIS_ELEMENT_NAME = "axis";

        public static readonly string CALIBRATION_ELEMENT_NAME = "calibration";
        public static readonly string RISING_ATTRIBUTE_NAME = "rising";
        public static readonly string FALLING_ATTRIBUTE_NAME = "falling";

        public static readonly string DYNAMICS_ELEMENT_NAME = "dynamics";
        public static readonly string DAMPING_ATTRIBUTE_NAME = "damping";
        public static readonly string FRICTION_ATTRIBUTE_NAME = "friction";

        public static readonly string LIMIT_ELEMENT_NAME = "limit";
        public static readonly string LOWER_ATTRIBUTE_NAME = "lower";
        public static readonly string UPPER_ATTRIBUTE_NAME = "upper";
        public static readonly string EFFORT_ATTRIBUTE_NAME = "effort";
        public static readonly string VELOCITY_ATTRIBUTE_NAME = "velocity";

        public static readonly string MIMIC_ELEMENT_NAME = "mimic";
        public static readonly string JOINT_ATTRIBUTE_NAME = "joint";
        public static readonly string MULTIPLIER_ATTRIBUTE_NAME = "multiplier";
        public static readonly string OFFSET_ATTRIBUTE_NAME = "offset";

        public static readonly string SAFETY_CONTROLLER_ELEMENT_NAME = "safety_controller";
        public static readonly string LOWER_LIMIT_ATTRIBUTE_NAME = "soft_lower_limit";
        public static readonly string UPPER_LIMIT_ATTRIBUTE_NAME = "soft_upper_limit";
        public static readonly string K_POSITION_ATTRIBUTE_NAME = "k_position";
        public static readonly string K_VELOCITY_ATTRIBUTE_NAME = "k_velocity";

        #endregion

        #region Link specific sub-elements

        public static readonly string COLLISION_ELEMENT_NAME = "collision";

        public static readonly string INERTIAL_ELEMENT_NAME = "inertial";
        public static readonly string MASS_ELEMENT_NAME = "mass";
        public static readonly string MASS_VALUE_ATTRIBUTE_NAME = "value";

        public static readonly string INERTIA_ELEMENT_NAME = "inertia";
        public static readonly string IXX_ATTRIBUTE_NAME = "ixx";
        public static readonly string IXY_ATTRIBUTE_NAME = "ixy";
        public static readonly string IXZ_ATTRIBUTE_NAME = "ixz";
        public static readonly string IYY_ATTRIBUTE_NAME = "iyy";
        public static readonly string IYZ_ATTRIBUTE_NAME = "iyz";
        public static readonly string IZZ_ATTRIBUTE_NAME = "izz";

        public static readonly string VISUAL_ELEMENT_NAME = "visual";

        public static readonly string MATERIAL_ELEMENT_NAME = "material";
        public static readonly string COLOR_ELEMENT_NAME = "color";
        public static readonly string TEXTURE_ELEMENT_NAME = "texture";

        public static readonly string RGB_ATTRIBUTE_NAME = "rgb";
        public static readonly string ALPHA_ATTRIBUTE_NAME = "alpha";
        public static readonly string RGBA_ATTRIBUTE_NAME = "rgba";

        public static readonly string GEOMETRY_ELEMENT_NAME = "geometry";

        public static readonly string SIZE_ATTRIBUTE_NAME = "size";
        public static readonly string RADIUS_ATTRIBUTE_NAME = "radius";

        public static readonly string BOX_ELEMENT_NAME = "box";

        public static readonly string CYLINDER_ELEMENT_NAME = "cylinder";
        public static readonly string LENGTH_ATTRIBUTE_NAME = "length";

        public static readonly string SPHERE_ELEMENT_NAME = "sphere";

        public static readonly string MESH_ELEMENT_NAME = "mesh";
        public static readonly string SCALE_ATTRIBUTE_NAME = "scale";

        #endregion
    }
}
