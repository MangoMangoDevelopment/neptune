using UrdfUnity.Util;

namespace UrdfUnity.Urdf.Models.LinkElements.VisualElements
{
    /// <summary>
    /// Represents the RGB attributes of a colour specification of a visual element.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    public class RgbAttribute
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
    }
}
