namespace UrdfUnity.Urdf.Models.Joints
{
    /// <summary>
    /// Represents the physical limitations of the movement of a joint.
    /// </summary>
    /// <remarks>
    /// The limit is required only for revolute and prismatic joints.
    /// </remarks>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    public sealed class Limit
    {
        private const double DEFAULT_VALUE = 0d;


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
        /// Creates a new instance of Limit.  This constructor should be used with named arguments
        /// if only one of the optional lower or upper limit properties is being specified.
        /// </summary>
        /// <param name="effort">The maximum joint effort</param>
        /// <param name="velocity">The maximum joint velocity</param>
        /// <param name="lower">The lower joint limit. Default value is 0</param>
        /// <param name="upper">The upper joint limit. Default value is 0</param>
        public Limit(double effort, double velocity, double lower = DEFAULT_VALUE, double upper = DEFAULT_VALUE)
        {
            this.Effort = effort;
            this.Velocity = velocity;
            this.Lower = lower;
            this.Upper = upper;
        }

        public override string ToString()
        {
            return $"<limit effort=\"{Effort}\" velocity=\"{Velocity}\" lower=\"{Lower}\" upper=\"{Upper}\"/>";
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
