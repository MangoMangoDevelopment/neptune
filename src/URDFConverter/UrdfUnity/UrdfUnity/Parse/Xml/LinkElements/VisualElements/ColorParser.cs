using System;
using System.Xml;
using UrdfUnity.Urdf.Models.LinkElements.VisualElements;
using UrdfUnity.Util;

namespace UrdfUnity.Parse.Xml.LinkElements.VisualElements
{
    /// <summary>
    /// Parses a URDF &lt;color&gt; element from XML into a Color object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    /// <seealso cref="Urdf.Models.LinkElements.VisualElements.Color"/>
    /// <seealso cref="Urdf.Models.LinkElements.VisualElements.RgbAttribute"/>
    public class ColorParser : XmlParser<Color>
    {
        private static readonly string RGB_ATTRIBUTE_NAME = "rgb";
        private static readonly string ALPHA_ATTRIBUTE_NAME = "alpha";
        private static readonly string RGBA_ATTRIBUTE_NAME = "rgba";
        private static readonly int DEFAULT_RGB_VALUE = 0; // Black
        private static readonly double DEFAULT_ALPHA_VALUE = 1d; // No transparency


        /// <summary>
        /// Parses a URDF &lt;color&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;color&gt; element</param>
        /// <returns>A Color object parsed from the XML</returns>
        public Color Parse(XmlNode node)
        {
            Preconditions.IsNotNull(node, "node");

            XmlAttribute rgbAttribute = (node.Attributes != null ? (XmlAttribute)node.Attributes.GetNamedItem(RGB_ATTRIBUTE_NAME) : null);
            XmlAttribute alphaAttribute = (node.Attributes != null ? (XmlAttribute)node.Attributes.GetNamedItem(ALPHA_ATTRIBUTE_NAME) : null);
            XmlAttribute rgbaAttribute = (node.Attributes != null ? (XmlAttribute)node.Attributes.GetNamedItem(RGBA_ATTRIBUTE_NAME) : null);

            RgbAttribute rgb;
            double alpha;

            if (rgbaAttribute != null && rgbAttribute == null && alphaAttribute == null)
            {
                Tuple<RgbAttribute, double> rgba = ParseRgba(rgbaAttribute);
                rgb = rgba.Item1;
                alpha = rgba.Item2;
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
                // TODO: Log malformed URDF <color> element encountered
            }
            else
            {
                if (!RegexUtils.IsMatchNDoubles(rgbAttribute.Value, 3))
                {
                    // TODO: Log malformed URDF <color> rgb attribute encountered
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
                // TODO: Log malformed URDF <box> element encountered
            }
            else
            {
                if (!RegexUtils.IsMatchNDoubles(alphaAttribute.Value, 1))
                {
                    // TODO: Log malformed URDF <color> alpha attribute encountered
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
                // TODO: Log malformed URDF <color> element encountered
            }
            else
            {
                if (!RegexUtils.IsMatchNDoubles(rgbaAttribute.Value, 4))
                {
                    // TODO: Log malformed URDF <color> rgba attribute encountered
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
