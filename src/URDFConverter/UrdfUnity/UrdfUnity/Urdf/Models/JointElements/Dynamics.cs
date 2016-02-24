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
        /// <value>Optional. <c>N*s/m</c> for prismatic joints; <c>N*m*s/rad</c> for revolute joints.</value>
        public double Damping { get; }

        /// <summary>
        /// The physical static friction value of the joint.
        /// </summary>
        /// <value>Optional. <c>N</c> for prismatic joints; <c>N*m</c> for revolute joints.</value>
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
