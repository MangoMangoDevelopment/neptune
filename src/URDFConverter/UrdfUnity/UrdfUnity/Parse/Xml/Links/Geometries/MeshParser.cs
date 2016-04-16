using System;
using System.Xml;
using NLog;
using UrdfUnity.Urdf.Models.Attributes;
using UrdfUnity.Urdf.Models.Links.Geometries;
using UrdfUnity.Util;

namespace UrdfUnity.Parse.Xml.Links.Geometries
{
    /// <summary>
    /// Parses a URDF &lt;mesh&gt; element from XML into a Mesh object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    /// <seealso cref="Urdf.Models.Links.Geometries.Mesh"/>
    public sealed class MeshParser : AbstractUrdfXmlParser<Mesh>
    {
        private static readonly string FILE_NAME_ATTRIBUTE_NAME = "filename";
        private static readonly string SCALE_ATTRIBUTE_NAME = "scale";
        private static readonly string SIZE_ATTRIBUTE_NAME = "size";


        protected override Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The name of the URDF XML element that this class parses.
        /// </summary>
        protected override string ElementName { get; } = "mesh";


        /// <summary>
        /// Parses a URDF &lt;mesh&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;mesh&gt; element</param>
        /// <returns>A Mesh object parsed from the XML</returns>
        public override Mesh Parse(XmlNode node)
        {
            ValidateXmlNode(node);

            XmlAttribute fileNameAttribute = GetAttributeFromNode(node, FILE_NAME_ATTRIBUTE_NAME);
            XmlAttribute scaleAttribute = GetAttributeFromNode(node, SCALE_ATTRIBUTE_NAME);
            XmlAttribute sizeAttribute = GetAttributeFromNode(node, SIZE_ATTRIBUTE_NAME);

            string fileName = ParseFileName(fileNameAttribute);
            ScaleAttribute scale = ParseScaleAttribute(scaleAttribute);
            SizeAttribute size = ParseSizeAttribute(sizeAttribute);
            Mesh.Builder builder = new Mesh.Builder(fileName);

            if (scale != null)
            {
                builder.SetScale(scale);
            }
            if (size != null)
            {
                builder.SetSize(size);
            }

            return builder.Build();
        }

        private string ParseFileName(XmlAttribute fileNameAttribute)
        {
            if (fileNameAttribute == null)
            {
                LogMissingRequiredAttribute(FILE_NAME_ATTRIBUTE_NAME);
                return Mesh.DEFAULT_FILE_NAME;
            }

            return fileNameAttribute.Value;
        }

        private ScaleAttribute ParseScaleAttribute(XmlAttribute scaleAttribute)
        {
            ScaleAttribute scale = null;

            if (scaleAttribute == null)
            {
                LogMissingOptionalAttribute(SCALE_ATTRIBUTE_NAME);
            }
            else
            {
                if (!RegexUtils.IsMatchNDoubles(scaleAttribute.Value, 3))
                {
                    LogMalformedAttribute(SCALE_ATTRIBUTE_NAME);
                }
                else
                {
                    double[] values = RegexUtils.MatchDoubles(scaleAttribute.Value);
                    scale = new ScaleAttribute(values[0], values[1], values[2]);
                }
            }

            return scale;
        }

        private SizeAttribute ParseSizeAttribute(XmlAttribute sizeAttribute)
        {
            SizeAttribute size = null;

            if (sizeAttribute == null)
            {
                LogMissingOptionalAttribute(SIZE_ATTRIBUTE_NAME);
            }
            else
            {
                if (!RegexUtils.IsMatchNDoubles(sizeAttribute.Value, 3))
                {
                    LogMalformedAttribute(SIZE_ATTRIBUTE_NAME);
                }
                else
                {
                    double[] values = RegexUtils.MatchDoubles(sizeAttribute.Value);
                    size = new SizeAttribute(values[0], values[1], values[2]);
                }
            }

            return size;
        }
    }
}
