using System;
using System.Xml;
using NLog;
using UrdfUnity.Urdf.Models.LinkElements.VisualElements;
using UrdfUnity.Util;
using UrdfUnity.Object;

namespace UrdfUnity.Parse.Xml.LinkElements.VisualElements
{
    /// <summary>
    /// Parses a URDF &lt;color&gt; element from XML into a Color object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    /// <seealso cref="Urdf.Models.LinkElements.VisualElements.Color"/>
    /// <seealso cref="Urdf.Models.LinkElements.VisualElements.RgbAttribute"/>
    public class ColorParser : AbstractUrdfXmlParser<Color>
    {
        private static readonly string RGB_ATTRIBUTE_NAME = "rgb";
        private static readonly string ALPHA_ATTRIBUTE_NAME = "alpha";
        private static readonly string RGBA_ATTRIBUTE_NAME = "rgba";
        private static readonly int DEFAULT_RGB_VALUE = 0; // Black
        private static readonly double DEFAULT_ALPHA_VALUE = 1d; // No transparency


        protected override Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The name of the URDF XML element that this class parses.
        /// </summary>
        protected override string ElementName { get; } = "color";


        /// <summary>
        /// Parses a URDF &lt;color&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;color&gt; element</param>
        /// <returns>A Color object parsed from the XML</returns>
        public override Color Parse(XmlNode node)
        {
            ValidateXmlNode(node);

            XmlAttribute rgbAttribute = GetAttributeFromNode(node, RGB_ATTRIBUTE_NAME);
            XmlAttribute alphaAttribute = GetAttributeFromNode(node, ALPHA_ATTRIBUTE_NAME);
            XmlAttribute rgbaAttribute = GetAttributeFromNode(node, RGBA_ATTRIBUTE_NAME);

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
                Logger.Warn("Parsing {0} failed due to missing both {1} and {2} attributes", ElementName, RGBA_ATTRIBUTE_NAME, RGB_ATTRIBUTE_NAME);
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
                LogMissingOptionalAttribute(RGB_ATTRIBUTE_NAME);
            }
            else
            {
                if (!RegexUtils.IsMatchNDoubles(rgbAttribute.Value, 3))
                {
                    LogMalformedAttribute(RGB_ATTRIBUTE_NAME);
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
                LogMissingOptionalAttribute(ALPHA_ATTRIBUTE_NAME);
            }
            else
            {
                if (!RegexUtils.IsMatchNDoubles(alphaAttribute.Value, 1))
                {
                    LogMalformedAttribute(ALPHA_ATTRIBUTE_NAME);
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
                LogMissingOptionalAttribute(RGBA_ATTRIBUTE_NAME);
            }
            else
            {
                if (!RegexUtils.IsMatchNDoubles(rgbaAttribute.Value, 4))
                {
                    LogMalformedAttribute(RGBA_ATTRIBUTE_NAME);
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
