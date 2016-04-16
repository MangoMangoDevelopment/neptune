namespace UrdfUnity.Urdf.Models.JointElements
{
    /// <summary>
    /// Represents the joint property values wherein the safety controller limits the joint position.
    /// </summary>
    /// <remarks>
    /// The upper bound on effort is <c>-KVelocity * (velocity - velocity limit)</c>.
    /// The upper bound on velocity is <c>-KPosition * (position - SoftUpperLimit)</c>.
    /// </remarks>
    /// <seealso cref="Limit"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="http://wiki.ros.org/pr2_controller_manager/safety_limits"/>
    public sealed class SafetyController
    {
        private const double DEFAULT_LOWER_LIMIT = 0d;
        private const double DEFAULT_UPPER_LIMIT = 0d;
        private const double DEFAULT_POSITION = 0d;

        /// <summary>
        /// The lower joint boundary where the safety controller starts limiting the position of the joint.
        /// </summary>
        /// <value>Optional. Default value is 0. This limit needs to be larger than the joint's <c>Limit.Lower</c></value>
        public double SoftLowerLimit { get; }

        /// <summary>
        /// The upper joint boundary where the safety controller starts limiting the position of the joint.
        /// </summary>
        /// <value>Optional. Default value is 0. This limit needs to be larger than the joint's <c>Limit.Upper</c></value>
        public double SoftUpperLimit { get; }

        /// <summary>
        /// The scale of the velocity boundary specifying the relation between position and velocity limits.
        /// </summary>
        /// <value>Optional. Default value is 0</value>
        public double KPostition { get; }

        /// <summary>
        /// The scale of the effort boundary specifying the relationship between effort and velocity limits.
        /// </summary>
        /// <value>Required.</value>
        public double KVelocity { get; }


        /// <summary>
        /// Creates a new instance of SafetyController with the specified limits, k_position and k_velocity.  This 
        /// constructor should be used with named arguments if not all of the optional properties are being specified.
        /// </summary>
        /// <param name="kVelocity">The scale of the bound on effort</param>
        /// <param name="kPosition">The scale of the bound on velocity. Default value is 0</param>
        /// <param name="lowerLimit">The soft lower limit of the joint position. Default value is 0</param>
        /// <param name="upperLimit">The soft upper limit of the joint position. Default value is 0</param>
        public SafetyController(double kVelocity, double kPosition = DEFAULT_POSITION,
            double lowerLimit = DEFAULT_LOWER_LIMIT, double upperLimit = DEFAULT_UPPER_LIMIT)
        {
            this.KVelocity = kVelocity;
            this.KPostition = kPosition;
            this.SoftLowerLimit = lowerLimit;
            this.SoftUpperLimit = upperLimit;
        }

        public override string ToString()
        {
            return $"<safety_controller k_velocity=\"{KVelocity}\" k_position=\"{KPostition}\" soft_lower_limit=\"{SoftLowerLimit}\" soft_upper_limit=\"{SoftUpperLimit}\"/>";
        }

        protected bool Equals(SafetyController other)
        {
            return SoftLowerLimit.Equals(other.SoftLowerLimit) && SoftUpperLimit.Equals(other.SoftUpperLimit)
                && KPostition.Equals(other.KPostition) && KVelocity.Equals(other.KVelocity);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SafetyController)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = SoftLowerLimit.GetHashCode();
                hashCode = (hashCode * 397) ^ SoftUpperLimit.GetHashCode();
                hashCode = (hashCode * 397) ^ KPostition.GetHashCode();
                hashCode = (hashCode * 397) ^ KVelocity.GetHashCode();
                return hashCode;
            }
        }
    }
}
