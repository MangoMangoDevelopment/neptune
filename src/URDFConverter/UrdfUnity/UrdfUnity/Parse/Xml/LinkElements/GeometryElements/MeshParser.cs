using System;
using System.Xml;
using UrdfUnity.Urdf.Models.LinkElements.GeometryElements;
using UrdfUnity.Util;

namespace UrdfUnity.Parse.Xml.LinkElements.GeometryElements
{
    /// <summary>
    /// Parses a URDF &lt;mesh&gt; element from XML into a Mesh object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    /// <seealso cref="Urdf.Models.LinkElements.GeometryElements.Mesh"/>
    public class MeshParser : XmlParser<Mesh>
    {
        private static readonly string FILE_NAME_ATTRIBUTE_NAME = "filename";
        private static readonly string SCALE_ATTRIBUTE_NAME = "scale";
        private static readonly string SIZE_ATTRIBUTE_NAME = "size";


        /// <summary>
        /// Parses a URDF &lt;mesh&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;mesh&gt; element</param>
        /// <returns>A Mesh object parsed from the XML</returns>
        public Mesh Parse(XmlNode node)
        {
            Preconditions.IsNotNull(node, "node");

            XmlAttribute fileNameAttribute = XmlParsingUtils.GetAttributeFromNode(node, FILE_NAME_ATTRIBUTE_NAME);
            XmlAttribute scaleAttribute = XmlParsingUtils.GetAttributeFromNode(node, SCALE_ATTRIBUTE_NAME);
            XmlAttribute sizeAttribute = XmlParsingUtils.GetAttributeFromNode(node, SIZE_ATTRIBUTE_NAME);

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
                // TODO: Log malformed <mesh> filename attribute encountered
                return Mesh.DEFAULT_FILE_NAME;
            }

            return fileNameAttribute.Value;
        }

        private ScaleAttribute ParseScaleAttribute(XmlAttribute scaleAttribute)
        {
            ScaleAttribute scale = null;

            if (scaleAttribute == null)
            {
                // TODO: Log malformed <mesh> scale attribute encountered
            }
            else
            {
                if (!RegexUtils.IsMatchNDoubles(scaleAttribute.Value, 3))
                {
                    // TODO: Log malformed URDF <mesh> scale attribute encountered
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
                // TODO: Log malformed URDF <mesh> size attribute encountered
            }
            else
            {
                if (!RegexUtils.IsMatchNDoubles(sizeAttribute.Value, 3))
                {
                    // TODO: Log malformed URDF <mesh> size attribute encountered
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
