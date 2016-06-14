using System.Xml;
//using NLog;
using UrdfToUnity.Urdf;
using UrdfToUnity.Urdf.Models.Attributes;
using UrdfToUnity.Urdf.Models.Links.Visuals;
using UrdfToUnity.Util;
using UrdfToUnity.Object;

namespace UrdfToUnity.Parse.Xml.Links.Visuals
{
    /// <summary>
    /// Parses a URDF &lt;color&gt; element from XML into a Color object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    /// <seealso cref="Urdf.Models.Links.Visuals.Color"/>
    /// <seealso cref="Urdf.Models.Links.Visuals.RgbAttribute"/>
    public class ColorParser : AbstractUrdfXmlParser<Color>
    {
        private static readonly int DEFAULT_RGB_VALUE = 0; // Black
        private static readonly double DEFAULT_ALPHA_VALUE = 1d; // No transparency


        //protected override Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The name of the URDF XML element that this class parses.
        /// </summary>
        protected override string ElementName { get; } = UrdfSchema.COLOR_ELEMENT_NAME;


        /// <summary>
        /// Parses a URDF &lt;color&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;color&gt; element</param>
        /// <returns>A Color object parsed from the XML</returns>
        public override Color Parse(XmlNode node)
        {
            ValidateXmlNode(node);

            XmlAttribute rgbAttribute = GetAttributeFromNode(node, UrdfSchema.RGB_ATTRIBUTE_NAME);
            XmlAttribute alphaAttribute = GetAttributeFromNode(node, UrdfSchema.ALPHA_ATTRIBUTE_NAME);
            XmlAttribute rgbaAttribute = GetAttributeFromNode(node, UrdfSchema.RGBA_ATTRIBUTE_NAME);

            RgbAttribute rgb;
            double alpha;

            if (rgbaAttribute != null && rgbAttribute == null && alphaAttribute == null)
            {
                Tuple<RgbAttribute, double> rgba = ParseRgba(rgbaAttribute);
                rgb = rgba.Item1;
                alpha = rgba.Item2;
            }
            else if (rgbAttribute == null)
            {
                //Logger.Warn("Parsing {0} failed due to missing both {1} and {2} attributes", ElementName, UrdfSchema.RGBA_ATTRIBUTE_NAME, UrdfSchema.RGB_ATTRIBUTE_NAME);
                rgb = new RgbAttribute(DEFAULT_RGB_VALUE, DEFAULT_RGB_VALUE, DEFAULT_RGB_VALUE);
                alpha = DEFAULT_ALPHA_VALUE;
            }
            else
            {
                rgb = ParseRgb(rgbAttribute);
                alpha = ParseAlpha(alphaAttribute);
            }

            return new Color(rgb, alpha);
        }

        private RgbAttribute ParseRgb(XmlAttribute rgbAttribute)
        {
            RgbAttribute rgb = new RgbAttribute(DEFAULT_RGB_VALUE, DEFAULT_RGB_VALUE, DEFAULT_RGB_VALUE);

            if (rgbAttribute == null)
            {
                LogMissingOptionalAttribute(UrdfSchema.RGB_ATTRIBUTE_NAME);
            }
            else
            {
                if (!RegexUtils.IsMatchNDoubles(rgbAttribute.Value, 3))
                {
                    LogMalformedAttribute(UrdfSchema.RGB_ATTRIBUTE_NAME);
                }
                else
                {
                    int[] values = RegexUtils.MatchInts(rgbAttribute.Value);
                    rgb = new RgbAttribute(values[0], values[1], values[2]);
                }
            }

            return rgb;
        }

        private double ParseAlpha(XmlAttribute alphaAttribute)
        {
            double alpha = DEFAULT_ALPHA_VALUE;

            if (alphaAttribute == null)
            {
                LogMissingOptionalAttribute(UrdfSchema.ALPHA_ATTRIBUTE_NAME);
            }
            else
            {
                if (!RegexUtils.IsMatchNDoubles(alphaAttribute.Value, 1))
                {
                    LogMalformedAttribute(UrdfSchema.ALPHA_ATTRIBUTE_NAME);
                }
                else
                {
                    alpha = RegexUtils.MatchDouble(alphaAttribute.Value, DEFAULT_ALPHA_VALUE);
                }
            }

            return alpha;
        }


        private Tuple<RgbAttribute, double> ParseRgba(XmlAttribute rgbaAttribute)
        {
            RgbAttribute rgb = new RgbAttribute(DEFAULT_RGB_VALUE, DEFAULT_RGB_VALUE, DEFAULT_RGB_VALUE);
            double alpha = DEFAULT_ALPHA_VALUE;

            if (rgbaAttribute == null)
            {
                LogMissingOptionalAttribute(UrdfSchema.RGBA_ATTRIBUTE_NAME);
            }
            else
            {
                if (!RegexUtils.IsMatchNDoubles(rgbaAttribute.Value, 4))
                {
                    LogMalformedAttribute(UrdfSchema.RGBA_ATTRIBUTE_NAME);
                }
                else
                {
                    double[] values = RegexUtils.MatchDoubles(rgbaAttribute.Value);
                    rgb = new RgbAttribute(values[0], values[1], values[2]);
                    alpha = values[3];
                }
            }

            return new Tuple<RgbAttribute, double>(rgb, alpha);
        }
    }
}
