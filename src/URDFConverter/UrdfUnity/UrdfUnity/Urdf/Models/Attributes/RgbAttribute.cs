using UrdfUnity.Util;

namespace UrdfUnity.Urdf.Models.Attributes
{
    /// <summary>
    /// Represents the RGB attributes of a colour specification of a visual element.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    public sealed class RgbAttribute
    {
        private static readonly int RGB_LOWER_BOUND = 0;
        private static readonly int RGB_UPPER_BOUND = 255;

        /// <summary>
        /// The RGB red value.
        /// </summary>
        /// <value>[0,255]</value>
        public int R { get; }

        /// <summary>
        /// The RGB green value.
        /// </summary>
        /// <value>[0,255]</value>
        public int G { get; }

        /// <summary>
        /// The RGB blue value.
        /// </summary>
        /// <value>[0,255]</value>
        public int B { get; }


        /// <summary>
        /// Creates a new instance of RgbAttribute.
        /// </summary>
        /// <param name="r">The RGB attribute's red value as a double. MUST BE WITHIN RANGE [0.0,1.0]</param>
        /// <param name="g">The RGB attribute's green value as a double. MUST BE WITHIN RANGE [0.0,1.0]</param>
        /// <param name="b">The RGB attribute's blue value as a double. MUST BE WITHIN RANGE [0.0,1.0]</param>
        public RgbAttribute(double r, double g, double b) : this((int)(r * RGB_UPPER_BOUND), (int)(g * RGB_UPPER_BOUND), (int)(b * RGB_UPPER_BOUND))
        {
            // Invoke overloaded constructor.
        }

        /// <summary>
        /// Creates a new instance of RgbAttribute.
        /// </summary>
        /// <param name="r">The RGB attribute's red value</param>
        /// <param name="g">The RGB attribute's green value</param>
        /// <param name="b">The RGB attribute's blue value</param>
        public RgbAttribute(int r, int g, int b)
        {
            Preconditions.IsWithinRange(r, RGB_LOWER_BOUND, RGB_UPPER_BOUND, "r");
            Preconditions.IsWithinRange(g, RGB_LOWER_BOUND, RGB_UPPER_BOUND, "g");
            Preconditions.IsWithinRange(b, RGB_LOWER_BOUND, RGB_UPPER_BOUND, "b");
            this.R = r;
            this.G = g;
            this.B = b;
        }

        protected bool Equals(RgbAttribute other)
        {
            return R == other.R && G == other.G && B == other.B;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((RgbAttribute)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = R;
                hashCode = (hashCode * 397) ^ G;
                hashCode = (hashCode * 397) ^ B;
                return hashCode;
            }
        }
    }
}
