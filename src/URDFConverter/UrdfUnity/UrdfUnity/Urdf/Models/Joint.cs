using UrdfUnity.Urdf.Models.JointElements;
using UrdfUnity.Util;

namespace UrdfUnity.Urdf.Models
{
    /// <summary>
    /// Represents a joint connecting two links in a robot structure enabling movement.
    /// </summary>
    /// <remarks>The joint is located at the origin of the child link.</remarks>
    /// <seealso cref="Link"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    public sealed class Joint
    {
        /// <summary>
        /// The joint type can be one of the following:
        /// <li>revolute - A hinge joint that rotates along the axis and has a limited range specified by the upper and lower limits.</li>
        /// <li>continuous - A continuous hinge joint that rotates around the axis and has no upper and lower limits.</li>
        /// <li>prismatic - A sliding joint that slides along the axis, and has a limited range specified by the upper and lower limits.</li>
        /// <li>fixed - This is not really a joint because it cannot move. All degrees of freedom are locked. This type of joint does not require the axis, calibration, dynamics, limits or safety_controller.</li>
        /// <li>floating - This joint allows motion for all 6 degrees of freedom.</li>
        /// <li>planar - This joint allows motion in a plane perpendicular to the axis.</li>
        /// <li>unknown - The joint type is not specified.</li>
        /// </summary>
        public enum JointType { Revolute, Continuous, Prismatic, Fixed, Floating, Planar, Unknown };

        /// <summary>
        /// The default name used when a Joint needs to be instantiated without a name.
        /// </summary>
        public static readonly string DEFAULT_NAME = "missing_name";


        /// <summary>
        /// The unique name of the joint.
        /// </summary>
        /// <value>Required. Must be unique</value>
        public string Name { get; }

        /// <summary>
        /// The type of joint.
        /// </summary>
        /// <value>Required.</value>
        public JointType Type { get; }

        /// <summary>
        /// The parent link of this joint in the robot structure.
        /// </summary>
        /// <value>Required.</value>
        public Link Parent { get; }

        /// <summary>
        /// The child link of this joint in the robot structure.
        /// </summary>
        /// <value>Required.</value>
        public Link Child { get; }

        /// <summary>
        /// The transform from the parent link to the child link.
        /// </summary>
        /// <value>Optional. Defaults to identity</value>
        public Origin Origin { get; }

        /// <summary>
        /// The joint axis in the joint frame.
        /// </summary>
        /// <value>Optional. Used for revolute, prismatic and planar joints</value>
        public Axis Axis { get; }

        /// <summary>
        /// The reference positions of the joint, used to calibrate the absolute position of the joint.
        /// </summary>
        /// <value>Optional. MAY BE NULL</value>
        public Calibration Calibration { get; }

        /// <summary>
        /// The physical damping and friction properties of the joint used for modeling properties of the joint.
        /// </summary>
        /// <value>Optional. Used for revolute and prismatic joints. MAY BE NULL</value>
        public Dynamics Dynamics { get; }

        /// <summary>
        /// The physical limits of movement of the joint.
        /// </summary>
        /// <value>Required for revolute and prismatic joints. MAY BE NULL for other joint types</value>
        public Limit Limit { get; }

        /// <summary>
        /// Specification for this joint to mimic an other defined joint.
        /// </summary>
        /// <value>Optional. MAY BE NULL</value>
        public Mimic Mimic { get; }

        /// <summary>
        /// The joint property values where the joint position becomes limited by the safety controller.
        /// </summary>
        /// <value>Optional. MAY BE NULL</value>
        public SafetyController SafetyController { get; }


        /// <summary>
        /// Creates a new instance of Joint. 
        /// A Joint.Builder must be used to create a joint to enforce required and default properties.
        /// </summary>
        /// <param name="name">The unique name of the joint</param>
        /// <param name="type">The type of the joint</param>
        /// <param name="parent">The parent link of the joint</param>
        /// <param name="child">The child link of the joint</param>
        /// <param name="origin">The transform from parent link to child link</param>
        /// <param name="axis">The joint axis in the joint frame of reference</param>
        /// <param name="calibration">The reference positions of the joint used to calibrate its absolute position</param>
        /// <param name="dynamics">The physical properties of the joint</param>
        /// <param name="limit">The physical limitations of the joint's movement</param>
        /// <param name="mimic">The specification of a joint to mimic</param>
        /// <param name="safetyController">The joint property values where its position becomes limited</param>
        private Joint(string name, JointType type, Link parent, Link child, Origin origin, Axis axis,
            Calibration calibration, Dynamics dynamics, Limit limit, Mimic mimic, SafetyController safetyController)
        {
            Preconditions.IsNotEmpty(name, "Joint name property must not be null or empty");
            Preconditions.IsNotNull(parent, "Joint parent property must not be null");
            Preconditions.IsNotNull(child, "Joint child property must not be null");
            Preconditions.IsNotNull(origin, "Joint origin property must not be null"); // Default is identity
            Preconditions.IsNotNull(axis, "Joint axis property must not be null"); // Default is (1,0,0)
            this.Name = name;
            this.Type = type;
            this.Parent = parent;
            this.Child = child;
            this.Origin = origin;
            this.Axis = axis;
            this.Calibration = calibration;
            this.Dynamics = dynamics;
            this.Limit = limit;
            this.Mimic = mimic;
            this.SafetyController = safetyController;
        }


        /// <summary>
        /// Helper class to build a new instance of joint with its defined properties.
        /// </summary>
        public class Builder
        {
            private string name;
            private JointType type;
            private Link parent;
            private Link child;
            private Origin origin = new Origin(); // Default is identity
            private Axis axis = new Axis(new XyzAttribute(1, 0, 0)); // Default is (1, 0, 0)
            private Calibration calibration;
            private Dynamics dynamics;
            private Limit limit;
            private Mimic mimic;
            private SafetyController safteyController;


            /// <summary>
            /// Creates a new instance of Builder with the required Joint properties.
            /// </summary>
            /// <param name="name">The unique name of the joint</param>
            /// <param name="type">The type of the joint</param>
            /// <param name="parent">The parent link of the joint</param>
            /// <param name="child">The child link of the joint</param>
            public Builder(string name, JointType type, Link parent, Link child)
            {
                Preconditions.IsNotEmpty(name, "Joint name property must not be null or empty");
                Preconditions.IsNotNull(parent, "Joint parent property must not be null");
                Preconditions.IsNotNull(child, "Joint child property must not be null");
                this.name = name;
                this.type = type;
                this.parent = parent;
                this.child = child;
            }

            /// <summary>
            /// Creates a new instance of Joint with the specified properties.
            /// </summary>
            /// <returns>A joint object with the properties set</returns>
            public Joint Build()
            {
                if (this.type == JointType.Prismatic || this.type == JointType.Revolute)
                {
                    Preconditions.IsNotNull(this.limit, "Joint limit property must not be null for prismatic and revolute joint types");
                }

                return new Joint(this.name, this.type, this.parent, this.child, this.origin, this.axis,
                    this.calibration, this.dynamics, this.limit, this.mimic, this.safteyController);
            }

            public Builder SetOrigin(Origin origin)
            {
                Preconditions.IsNotNull(origin, "Joint origin property cannot be set to null");
                this.origin = origin;
                return this;
            }

            public Builder SetAxis(Axis axis)
            {
                Preconditions.IsNotNull(axis, "Joint axis property cannot be set to null");
                this.axis = axis;
                return this;
            }

            public Builder SetCalibration(Calibration calibration)
            {
                Preconditions.IsNotNull(calibration, "Joint calibration property cannot be set to null");
                this.calibration = calibration;
                return this;
            }

            public Builder SetDynamics(Dynamics dynamics)
            {
                Preconditions.IsNotNull(dynamics, "Joint dynamics property cannot be set to null");
                this.dynamics = dynamics;
                return this;
            }

            public Builder SetLimit(Limit limit)
            {
                Preconditions.IsNotNull(limit, "Joint limit property cannot be set to null");
                this.limit = limit;
                return this;
            }

            public Builder SetMimic(Mimic mimic)
            {
                Preconditions.IsNotNull(mimic, "Joint mimic property cannot be set to null");
                this.mimic = mimic;
                return this;
            }

            public Builder SetSafetyController(SafetyController safteyController)
            {
                Preconditions.IsNotNull(safteyController, "Joint safety controller property cannot be set to null");
                this.safteyController = safteyController;
                return this;
            }
        }

        protected bool Equals(Joint other)
        {
            return string.Equals(Name, other.Name) && Type == other.Type && Equals(Parent, other.Parent)
                && Equals(Child, other.Child) && Equals(Origin, other.Origin) && Equals(Axis, other.Axis)
                && Equals(Calibration, other.Calibration) && Equals(Dynamics, other.Dynamics) && Equals(Limit, other.Limit)
                && Equals(Mimic, other.Mimic) && Equals(SafetyController, other.SafetyController);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Joint)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int)Type;
                hashCode = (hashCode * 397) ^ (Parent != null ? Parent.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Child != null ? Child.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Origin != null ? Origin.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Axis != null ? Axis.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Calibration != null ? Calibration.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Dynamics != null ? Dynamics.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Limit != null ? Limit.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Mimic != null ? Mimic.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SafetyController != null ? SafetyController.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
