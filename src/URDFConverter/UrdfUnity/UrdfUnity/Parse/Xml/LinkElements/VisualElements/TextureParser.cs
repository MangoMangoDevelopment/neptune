using System.Xml;
using UrdfUnity.Urdf.Models.LinkElements.VisualElements;
using UrdfUnity.Util;

namespace UrdfUnity.Parse.Xml.LinkElements.VisualElements
{
    /// <summary>
    /// Parses a URDF &lt;texture&gt; element from XML into a Texture object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    /// <seealso cref="Urdf.Models.LinkElements.VisualElements.Texture"/>
    public class TextureParser : XmlParser<Texture>
    {
        private static readonly string FILE_NAME_ATTRIBUTE_NAME = "filename";

        /// <summary>
        /// Parses a URDF &lt;texture&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;texture&gt; element</param>
        /// <returns>A Texture object parsed from the XML</returns>
        public Texture Parse(XmlNode node)
        {
            Preconditions.IsNotNull(node, "node");

            XmlAttribute fileNameAttribute = (node.Attributes != null) ? (XmlAttribute)node.Attributes.GetNamedItem(FILE_NAME_ATTRIBUTE_NAME) : null;
            string filename = Texture.DEFAULT_FILE_NAME;

            if (fileNameAttribute == null || fileNameAttribute.Value == null)
            {
                // TODO: Log malformed <texture> filename attribute encountered
            }
            else
            {
                filename = fileNameAttribute.Value;
            }

            return new Texture(filename);
        }
    }
}
