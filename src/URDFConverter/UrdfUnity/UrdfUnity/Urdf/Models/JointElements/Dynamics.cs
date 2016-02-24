namespace UrdfUnity.Urdf.Models.JointElements
{
    /// <summary>
    /// Represents the physical properties used to specify modeling properties of the joint.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    public class Dynamics
    {
        /// <summary>
        /// The physical damping value of the joint.
        /// </summary>
        /// <value>Optional. (N*s/m) for prismatic joints; (N*m*s/rad) for revolute joints.</value>
        public double Damping { get; }

        /// <summary>
        /// The physical static friction value of the joint.
        /// </summary>
        /// <value>Optional. (N) for prismatic joints; (N*m) for revolute joints.</value>
        public double Friction { get; }


        /// <summary>
        /// Creates a new instance of Dynamics.
        /// </summary>
        /// <param name="damping">The physical damping value of the joint. Default value should be 0</param>
        /// <param name="friction">The physical static friction value of the joint. Default value should be 0</param>
        public Dynamics(double damping, double friction)
        {
            this.Damping = damping;
            this.Friction = friction;
        }
    }
}
