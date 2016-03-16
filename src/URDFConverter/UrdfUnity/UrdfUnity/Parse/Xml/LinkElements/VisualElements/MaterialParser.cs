using System.Collections.Generic;
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
        private readonly Dictionary<string, Material> materialDictionary;


        /// <summary>
        /// Creates a new instance of MaterialParser.
        /// </summary>
        /// <param name="materialDictionary">A dictionary of defined materials with material names as keys</param>
        public MaterialParser(Dictionary<string, Material> materialDictionary)
        {
            this.materialDictionary = materialDictionary;
        }

        /// <summary>
        /// Parses a URDF &lt;material&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;material&gt; element</param>
        /// <returns>A Material object parsed from the XML</returns>
        public Material Parse(XmlNode node)
        {
            Preconditions.IsNotNull(node, "node");

            XmlAttribute nameAttribute = XmlParsingUtils.GetAttributeFromNode(node, NAME_ATTRIBUTE_NAME);
            XmlElement colorElement = XmlParsingUtils.GetElementFromNode(node, COLOR_ELEMENT_NAME);
            XmlElement textureElement = XmlParsingUtils.GetElementFromNode(node, TEXTURE_ELEMENT_NAME);

            string name = ParseName(nameAttribute);
            Color color = ParseColor(colorElement);
            Texture texture = ParseTexture(textureElement);

            if (!name.Equals(Material.DEFAULT_NAME) && color == null && texture == null)
            {
                if (!this.materialDictionary.ContainsKey(name))
                {
                    // TODO: Log unknown material referenced by <material> name attribute
                    return new Material(name);
                }
                else
                {
                    return this.materialDictionary[name];
                }
            }

            if (colorElement != null && textureElement != null)
            {
                return new Material(name, color, texture);
            }
            if (textureElement != null)
            {
                return new Material(name, texture);
            }
            if (colorElement != null)
            {
                return new Material(name, color);
            }

            // TODO: Log malformed <material> element
            return new Material(name, new Color(new RgbAttribute(DEFAULT_COLOR_VALUE, DEFAULT_COLOR_VALUE, DEFAULT_COLOR_VALUE)));
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
