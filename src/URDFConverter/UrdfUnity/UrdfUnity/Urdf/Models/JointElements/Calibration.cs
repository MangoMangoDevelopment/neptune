namespace UrdfUnity.Urdf.Models.JointElements
{
    /// <summary>
    /// Represents the reference positions of the joint, used to calibrate the absolute position of the joint. 
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    public sealed class Calibration
    {
        /// <summary>
        /// The default value used for the rising and falling properties if not specified.
        /// </summary>
        private const double DEFAULT_VALUE = 0d;


        /// <summary>
        /// The reference position that will trigger a rising edge when the joint moves in a positive direction.
        /// </summary>
        /// <value>Optional. Default value is 0</value>
        public double Rising { get; }

        /// <summary>
        /// The reference position that will trigger a falling edge when the joint moves in a positive direction.
        /// </summary>
        /// <value>Optional. Default value is 0</value>
        public double Falling { get; }


        /// <summary>
        /// Creates a new instance of Calibration.  This constructor should be used with named arguments
        /// if only one of the optional rising or falling properties is being specified.
        /// </summary>
        /// <param name="rising">The reference position that triggers a rising edge. Default value is 0</param>
        /// <param name="falling">The reference position that triggers a falling edge. Default value is 0</param>
        public Calibration(double rising = DEFAULT_VALUE, double falling = DEFAULT_VALUE)
        {
            this.Rising = rising;
            this.Falling = falling;
        }

        public override string ToString()
        {
            return $"<calibration rising=\"{Rising}\" falling=\"{Falling}\"/>";
        }

        protected bool Equals(Calibration other)
        {
            return Rising.Equals(other.Rising) && Falling.Equals(other.Falling);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Calibration)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Rising.GetHashCode() * 397) ^ Falling.GetHashCode();
            }
        }
    }
}
