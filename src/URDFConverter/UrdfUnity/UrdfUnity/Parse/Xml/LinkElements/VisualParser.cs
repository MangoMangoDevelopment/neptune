using System.Xml;
using UrdfUnity.Parse.Xml.LinkElements.VisualElements;
using UrdfUnity.Urdf.Models.LinkElements;
using UrdfUnity.Util;

namespace UrdfUnity.Parse.Xml.LinkElements
{
    /// <summary>
    /// Parses a URDF &lt;visual&gt; element from XML into a Visual object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    /// <seealso cref="Urdf.Models.LinkElements.Visual"/>
    public class VisualParser : XmlParser<Visual>
    {
        private static readonly string NAME_ATTRIBUTE_NAME = "name";
        private static readonly string ORIGIN_ELEMENT_NAME = "origin";
        private static readonly string GEOMETRY_ELEMENT_NAME = "geometry";
        private static readonly string MATERIAL_ELEMENT_NAME = "material";

        private readonly OriginParser originParser = new OriginParser();
        private readonly GeometryParser geometryParser = new GeometryParser();
        private readonly MaterialParser materialParser = new MaterialParser();

        
        /// <summary>
        /// Parses a URDF &lt;visual&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;visual&gt; element</param>
        /// <returns>A Visual object parsed from the XML</returns>
        public Visual Parse(XmlNode node)
        {
            Preconditions.IsNotNull(node);

            XmlAttribute nameAttribute = (node.Attributes != null) ? (XmlAttribute)node.Attributes.GetNamedItem(NAME_ATTRIBUTE_NAME) : null;
            XmlElement originElement = (XmlElement)node.SelectSingleNode(ORIGIN_ELEMENT_NAME);
            XmlElement geometryElement = (XmlElement)node.SelectSingleNode(GEOMETRY_ELEMENT_NAME);
            XmlElement materialElement = (XmlElement)node.SelectSingleNode(MATERIAL_ELEMENT_NAME);

            Visual.Builder builder;

            if (geometryElement == null)
            {
                // TODO: Log missing required <visual> geometry sub-element
                builder = new Visual.Builder(GeometryParser.DEFAULT_GEOMETRY);
            }
            else
            {
                builder = new Visual.Builder(this.geometryParser.Parse(geometryElement));
            }

            if (nameAttribute != null)
            {
                builder.SetName(nameAttribute.Value);
            }

            if (originElement != null)
            {
                builder.SetOrigin(this.originParser.Parse(originElement));
            }

            if (materialElement != null)
            {
                builder.SetMaterial(this.materialParser.Parse(materialElement));
            }

            return builder.Build();
        }
    }
}
