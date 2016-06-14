using System.Collections.Generic;
using System.Xml;
//using NLog;
using UrdfToUnity.Parse.Xml.Links.Visuals;
using UrdfToUnity.Urdf;
using UrdfToUnity.Urdf.Models.Links;
using UrdfToUnity.Urdf.Models.Links.Visuals;

namespace UrdfToUnity.Parse.Xml.Links
{
    /// <summary>
    /// Parses a URDF &lt;visual&gt; element from XML into a Visual object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    /// <seealso cref="Urdf.Models.Links.Visual"/>
    public sealed class VisualParser : AbstractUrdfXmlParser<Visual>
    {
        private readonly OriginParser originParser = new OriginParser();
        private readonly GeometryParser geometryParser = new GeometryParser();
        private readonly MaterialParser materialParser;


        //protected override Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The name of the URDF XML element that this class parses.
        /// </summary>
        protected override string ElementName { get; } = UrdfSchema.VISUAL_ELEMENT_NAME;


        /// <summary>
        /// Creates a new instance of VisualParser.
        /// </summary>
        /// <param name="materialDictionary"></param>
        public VisualParser(Dictionary<string, Material> materialDictionary)
        {
            this.materialParser = new MaterialParser(materialDictionary);
        }

        /// <summary>
        /// Parses a URDF &lt;visual&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;visual&gt; element</param>
        /// <returns>A Visual object parsed from the XML</returns>
        public override Visual Parse(XmlNode node)
        {
            ValidateXmlNode(node);

            XmlAttribute nameAttribute = GetAttributeFromNode(node, UrdfSchema.NAME_ATTRIBUTE_NAME);
            XmlElement originElement = GetElementFromNode(node, UrdfSchema.ORIGIN_ELEMENT_NAME);
            XmlElement geometryElement = GetElementFromNode(node, UrdfSchema.GEOMETRY_ELEMENT_NAME);
            XmlElement materialElement = GetElementFromNode(node, UrdfSchema.MATERIAL_ELEMENT_NAME);

            Visual.Builder builder;

            if (geometryElement == null)
            {
                LogMissingRequiredElement(UrdfSchema.GEOMETRY_ELEMENT_NAME);
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
