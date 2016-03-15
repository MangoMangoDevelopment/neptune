using UrdfUnity.Util;

namespace UrdfUnity.Urdf.Models.JointElements
{
    /// <summary>
    /// Represents the behavior of a defined joint that mimics another existing joint.
    /// </summary>
    /// <remarks>
    /// The value of a joint can be computed as <c>value = multiplier * other_joint_value + offset</c>.
    /// </remarks>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    public class Mimic
    {
        private static readonly double DEFAULT_MULTIPLIER = 1d;
        private static readonly double DEFAULT_OFFSET = 0d;

        /// <summary>
        /// The name of the joint to mimic.
        /// </summary>
        /// <value>Required.</value>
        public Joint Joint { get; }

        /// <summary>
        /// The multiplicative factor used to calculate the value of this joint from the mimicked joint.
        /// </summary>
        /// <value>Optional. Default value is 1</value>
        public double Multiplier { get; }

        /// <summary>
        /// The offset to add to calculate the value of this joint from the mimicked joint.
        /// </summary>
        /// <value>Optional. Default value is 0</value>
        public double Offset { get; }


        /// <summary>
        /// Creates a new instance of Mimic with the specified joint, and default multiplier and offset values.
        /// </summary>
        /// <param name="joint">The joint to mimic. MUST NOT BE NULL</param>
        public Mimic(Joint joint) : this(joint, DEFAULT_MULTIPLIER, DEFAULT_OFFSET)
        {
            // Invoke overloaded constructor.
        }

        /// <summary>
        /// Creates a new instance of Mimic with the specified joint, multiplier and offset.
        /// </summary>
        /// <param name="joint">The joint to mimic. MUST NOT BE NULL</param>
        /// <param name="multiplier">The multiplicative factor. Default value is 1</param>
        /// <param name="offset">The offset to add. Default value is 0</param>
        public Mimic(Joint joint, double multiplier, double offset)
        {
            Preconditions.IsNotNull(joint, "joint");
            this.Joint = joint;
            this.Multiplier = multiplier;
            this.Offset = offset;
        }

        protected bool Equals(Mimic other)
        {
            return Joint.Equals(other.Joint) && Multiplier.Equals(other.Multiplier) && Offset.Equals(other.Offset);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Mimic)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Joint.GetHashCode();
                hashCode = (hashCode * 397) ^ Multiplier.GetHashCode();
                hashCode = (hashCode * 397) ^ Offset.GetHashCode();
                return hashCode;
            }
        }
    }
}
