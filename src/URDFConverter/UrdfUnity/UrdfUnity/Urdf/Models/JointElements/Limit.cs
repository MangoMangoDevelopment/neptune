namespace UrdfUnity.Urdf.Models.JointElements
{
    /// <summary>
    /// Represents the physical limitations of the movement of a joint.
    /// </summary>
    /// <remarks>
    /// The limit is required only for revolute and prismatic joints.
    /// </remarks>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    public class Limit
    {
        private static readonly double DEFAULT_VALUE = 0d;

        /// <summary>
        /// The lower joint limit.
        /// </summary>
        /// <value>Optional. In radians for revolute joints; In meters for prismatic joints.</value>
        public double Lower { get; }

        /// <summary>
        /// The upper joint limit.
        /// </summary>
        /// <value>Optional. In radians for revolute joints; In meters for prismatic joints.</value>
        public double Upper { get; }

        /// <summary>
        /// The maximum joint effort that should be enforced.
        /// </summary>
        /// <value>Required. <c>|applied effort| &lt; |effort|</c></value>
        public double Effort { get; }

        /// <summary>
        /// The maximum joint velocity that should be enforced.
        /// </summary>
        /// <value>Required.</value>
        public double Velocity { get; }


        /// <summary>
        /// Creates a new instance of Limit.
        /// </summary>
        /// <param name="effort">The maximum joint effort</param>
        /// <param name="velocity">The maximum joint velocity</param>
        public Limit(double effort, double velocity) : this(DEFAULT_VALUE, DEFAULT_VALUE, effort, velocity)
        {
            // Invoke overloaded constructor.
        }

        /// <summary>
        /// Creates a new instance of Limit.
        /// </summary>
        /// <param name="lower">The lower joint limit</param>
        /// <param name="upper">The upper joint limit</param>
        /// <param name="effort">The maximum joint effort</param>
        /// <param name="velocity">The maximum joint velocity</param>
        public Limit(double lower, double upper, double effort, double velocity)
        {
            this.Lower = lower;
            this.Upper = upper;
            this.Effort = effort;
            this.Velocity = velocity;
        }

        protected bool Equals(Limit other)
        {
            return Lower.Equals(other.Lower) && Upper.Equals(other.Upper) && Effort.Equals(other.Effort) && Velocity.Equals(other.Velocity);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Limit)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Lower.GetHashCode();
                hashCode = (hashCode * 397) ^ Upper.GetHashCode();
                hashCode = (hashCode * 397) ^ Effort.GetHashCode();
                hashCode = (hashCode * 397) ^ Velocity.GetHashCode();
                return hashCode;
            }
        }
    }
}
