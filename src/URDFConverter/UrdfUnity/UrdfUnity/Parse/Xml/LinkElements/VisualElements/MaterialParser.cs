using System.Xml;
using UrdfUnity.Urdf.Models.LinkElements.VisualElements;
using UrdfUnity.Util;

namespace UrdfUnity.Parse.Xml.LinkElements.VisualElements
{
    /// <summary>
    /// Parses a URDF &lt;material&gt; element from XML into a Material object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    /// <seealso cref="Urdf.Models.LinkElements.VisualElements.Material"/>
    public class MaterialParser : XmlParser<Material>
    {
        private static readonly string NAME_ATTRIBUTE_NAME = "name";
        private static readonly string COLOR_ELEMENT_NAME = "color";
        private static readonly string TEXTURE_ELEMENT_NAME = "texture";
        private static readonly int DEFAULT_COLOR_VALUE = 0; // Black

        private readonly ColorParser colorParser = new ColorParser();
        private readonly TextureParser textureParser = new TextureParser();


        /// <summary>
        /// Parses a URDF &lt;material&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;material&gt; element</param>
        /// <returns>A Material object parsed from the XML</returns>
        public Material Parse(XmlNode node)
        {
            Preconditions.IsNotNull(node, "node");

            XmlAttribute nameAttribute = (node.Attributes != null) ? (XmlAttribute)node.Attributes.GetNamedItem(NAME_ATTRIBUTE_NAME) : null;
            XmlElement colorElement = (XmlElement)node.SelectSingleNode(COLOR_ELEMENT_NAME);
            XmlElement textureElement = (XmlElement)node.SelectSingleNode(TEXTURE_ELEMENT_NAME);

            string name = ParseName(nameAttribute);
            Color color = ParseColor(colorElement);
            Texture texture = ParseTexture(textureElement);

            if (textureElement != null)
            {
                return new Material(name, texture);
            }
            else if (colorElement != null)
            {
                return new Material(name, color);
            }
            else
            {
                // TODO: Log malformed <material> element
                return new Material(name, new Color(new RgbAttribute(DEFAULT_COLOR_VALUE, DEFAULT_COLOR_VALUE, DEFAULT_COLOR_VALUE)));
            }
        }

        private string ParseName(XmlAttribute nameAttribute)
        {
            if (nameAttribute == null || nameAttribute.Value == null)
            {
                // TODO: Log malformed <material> name attribute encountered
                return Material.DEFAULT_NAME;
            }

            return nameAttribute.Value;
        }

        private Color ParseColor(XmlElement colorElement)
        {
            if (colorElement == null)
            {
                // TODO: Log missing optional <material> color element.
                return null;
            }

            return this.colorParser.Parse(colorElement);
        }

        private Texture ParseTexture(XmlElement textureElement)
        {
            if (textureElement == null)
            {
                // TODO: Log missing optional <material> texture element.
                return null;
            }

            return this.textureParser.Parse(textureElement);
        }
    }
}
