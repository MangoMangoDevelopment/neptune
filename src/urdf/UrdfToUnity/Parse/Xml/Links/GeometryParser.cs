using System.Xml;
//using NLog;
using UrdfToUnity.Parse.Xml.Links.Geometries;
using UrdfToUnity.Urdf;
using UrdfToUnity.Urdf.Models.Attributes;
using UrdfToUnity.Urdf.Models.Links;
using UrdfToUnity.Urdf.Models.Links.Geometries;

namespace UrdfToUnity.Parse.Xml.Links
{
    /// <summary>
    /// Parses a URDF &lt;geometry&gt; element from XML into a Geometry object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    /// <seealso cref="Urdf.Models.Links.Geometry"/>
    public sealed class GeometryParser : AbstractUrdfXmlParser<Geometry>
    {
        public static readonly Geometry DEFAULT_GEOMETRY = new Geometry(new Box(new SizeAttribute(1, 1, 1)));


        //protected override Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The name of the URDF XML element that this class parses.
        /// </summary>
        protected override string ElementName { get; } = UrdfSchema.GEOMETRY_ELEMENT_NAME;


        private readonly BoxParser boxParser = new BoxParser();
        private readonly CylinderParser cylinderParser = new CylinderParser();
        private readonly SphereParser sphereParser = new SphereParser();
        private readonly MeshParser meshParser = new MeshParser();


        /// <summary>
        /// Parses a URDF &lt;geometry&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;geometry&gt; element</param>
        /// <returns>A Geometry object parsed from the XML</returns>
        public override Geometry Parse(XmlNode node)
        {
            ValidateXmlNode(node);

            XmlElement boxElement = GetElementFromNode(node, UrdfSchema.BOX_ELEMENT_NAME);
            XmlElement cylinderElement = GetElementFromNode(node, UrdfSchema.CYLINDER_ELEMENT_NAME);
            XmlElement sphereElement = GetElementFromNode(node, UrdfSchema.SPHERE_ELEMENT_NAME);
            XmlElement meshElement = GetElementFromNode(node, UrdfSchema.MESH_ELEMENT_NAME);

            Geometry geometry = null;

            if (boxElement != null)
            {
                geometry = new Geometry(this.boxParser.Parse(boxElement));
            }
            else if (cylinderElement != null)
            {
                geometry = new Geometry(this.cylinderParser.Parse(cylinderElement));
            }
            else if (sphereElement != null)
            {
                geometry = new Geometry(this.sphereParser.Parse(sphereElement));
            }
            else if (meshElement != null)
            {
                geometry = new Geometry(this.meshParser.Parse(meshElement));
            }
            else
            {
                //Logger.Warn("Parsing {0} element failed to due missing sub-element of valid type (box, cylinder, sphere, mesh)", ElementName);
                geometry = DEFAULT_GEOMETRY;
            }

            return geometry;
        }
    }
}
