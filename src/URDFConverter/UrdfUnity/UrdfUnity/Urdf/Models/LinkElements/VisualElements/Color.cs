using UrdfUnity.Util;

namespace UrdfUnity.Urdf.Models.LinkElements.VisualElements
{
    /// <summary>
    /// Represents the colour specification of a visual element.
    /// </summary>
    public class Color
    {
        private static readonly double MIN_ALPHA = 0d;
        private static readonly double MAX_ALPHA = 1d;
        private static readonly double DEFAULT_ALPHA = 1d;

        /// <summary>
        /// The RGB value of the visual element's colour.
        /// </summary>
        public RgbAttribute Rgb { get; }

        /// <summary>
        /// The alpha value of the visual element's colour.
        /// </summary>
        /// <value>Optional. [0,1]</value>
        public double Alpha { get; }


        /// <summary>
        /// Creates a new instance of RgbAtribute with the specified RGB value.
        /// </summary>
        /// <param name="rgb">The colour's RGB value</param>
        public Color(RgbAttribute rgb) : this(rgb, DEFAULT_ALPHA)
        {
            // Invoke overloaded constructor.
        }

        /// <summary>
        /// Creates a new instance of RgbAttribute with the specified RGB and alpha values.
        /// </summary>
        /// <param name="rgb">The colour's RGB value</param>
        /// <param name="alpha">The colour's alpha value</param>
        public Color(RgbAttribute rgb, double alpha)
        {
            Preconditions.IsWithinRange(alpha, MIN_ALPHA, MAX_ALPHA, "alpha");
            this.Rgb = rgb;
            this.Alpha = alpha;
        }
    }
}
