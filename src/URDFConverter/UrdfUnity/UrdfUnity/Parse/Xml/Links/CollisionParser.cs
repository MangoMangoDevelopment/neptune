using System.Xml;
using NLog;
using UrdfUnity.Urdf;
using UrdfUnity.Urdf.Models.Attributes;
using UrdfUnity.Urdf.Models.Links;
using UrdfUnity.Urdf.Models.Links.Geometries;

namespace UrdfUnity.Parse.Xml.Links
{
    /// <summary>
    /// Parses a URDF &lt;collision&gt; element from XML into a Collision object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="Urdf.Models.Links.Collision"/>
    public sealed class CollisionParser : AbstractUrdfXmlParser<Collision>
    {
        private static readonly Geometry DEFAULT_GEOMETRY = new Geometry(new Box(new SizeAttribute(1, 1, 1)));


        protected override Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The name of the URDF XML element that this class parses.
        /// </summary>
        protected override string ElementName { get; } = UrdfSchema.COLLISION_ELEMENT_NAME;


        private readonly OriginParser originParser = new OriginParser();
        private readonly GeometryParser geometryParser = new GeometryParser();


        /// <summary>
        /// Parses a URDF &lt;collision&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;collision&gt; element</param>
        /// <returns>A Collision object parsed from the XML</returns>
        public override Collision Parse(XmlNode node)
        {
            ValidateXmlNode(node);

            XmlAttribute nameAttribute = GetAttributeFromNode(node, UrdfSchema.NAME_ATTRIBUTE_NAME);
            XmlElement originElement = GetElementFromNode(node, UrdfSchema.ORIGIN_ELEMENT_NAME);
            XmlElement geometryElement = GetElementFromNode(node, UrdfSchema.GEOMETRY_ELEMENT_NAME);

            Collision.Builder builder;

            if (nameAttribute != null)
            {
                builder = new Collision.Builder(nameAttribute.Value);
            }
            else
            {
                builder = new Collision.Builder();
            }

            if (originElement != null)
            {
                builder.SetOrigin(this.originParser.Parse(originElement));
            }

            if (geometryElement != null)
            {
                builder.SetGeometry(this.geometryParser.Parse(geometryElement));
            }
            else
            {
                LogMalformedAttribute(UrdfSchema.GEOMETRY_ELEMENT_NAME);
                builder.SetGeometry(DEFAULT_GEOMETRY);
            }

            return builder.Build();
        }
    }
}
