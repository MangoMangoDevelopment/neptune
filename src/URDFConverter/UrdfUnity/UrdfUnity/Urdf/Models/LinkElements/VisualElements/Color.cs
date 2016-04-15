using UrdfUnity.Urdf.Models.Attributes;
using UrdfUnity.Util;

namespace UrdfUnity.Urdf.Models.LinkElements.VisualElements
{
    /// <summary>
    /// Represents the colour specification of a visual element.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    public sealed class Color
    {
        private static readonly double MIN_ALPHA = 0d;
        private static readonly double MAX_ALPHA = 1d;
        private static readonly double DEFAULT_ALPHA = 1d;

        /// <summary>
        /// The RGB value of the visual element's colour.
        /// </summary>
        /// <value>Required.</value>
        public RgbAttribute Rgb { get; }

        /// <summary>
        /// The alpha value of the visual element's colour.
        /// </summary>
        /// <value>Optional. MUST BE IN RANGE [0,1]. Default value is 1.</value>
        public double Alpha { get; }


        /// <summary>
        /// Creates a new instance of RgbAtribute with the specified RGB value.
        /// </summary>
        /// <param name="rgb">The colour's RGB value. MUST NOT BE NULL</param>
        public Color(RgbAttribute rgb) : this(rgb, DEFAULT_ALPHA)
        {
            // Invoke overloaded constructor.
        }

        /// <summary>
        /// Creates a new instance of RgbAttribute with the specified RGB and alpha values.
        /// </summary>
        /// <param name="rgb">The colour's RGB value. MUST NOT BE NULL</param>
        /// <param name="alpha">The colour's alpha value. MUST BE WITHIN RANGE [0,1]</param>
        public Color(RgbAttribute rgb, double alpha)
        {
            Preconditions.IsNotNull(rgb, "Color rgb property must not be null");
            Preconditions.IsWithinRange(alpha, MIN_ALPHA, MAX_ALPHA, "alpha");
            this.Rgb = rgb;
            this.Alpha = alpha;
        }

        protected bool Equals(Color other)
        {
            return Rgb.Equals(other.Rgb) && Alpha.Equals(other.Alpha);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Color)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Rgb.GetHashCode() * 397) ^ Alpha.GetHashCode();
            }
        }
    }
}
