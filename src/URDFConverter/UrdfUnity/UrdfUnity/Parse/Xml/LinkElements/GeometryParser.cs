using System;
using System.Xml;
using NLog;
using UrdfUnity.Parse.Xml.LinkElements.GeometryElements;
using UrdfUnity.Urdf.Models.LinkElements;
using UrdfUnity.Urdf.Models.LinkElements.GeometryElements;
using UrdfUnity.Util;

namespace UrdfUnity.Parse.Xml.LinkElements
{
    /// <summary>
    /// Parses a URDF &lt;geometry&gt; element from XML into a Geometry object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    /// <seealso cref="Urdf.Models.LinkElements.Geometry"/>
    public sealed class GeometryParser : AbstractUrdfXmlParser<Geometry>
    {
        public static readonly Geometry DEFAULT_GEOMETRY = new Geometry(new Box(new SizeAttribute(1, 1, 1)));

        private static readonly string BOX_ELEMENT_NAME = "box";
        private static readonly string CYLINDER_ELEMENT_NAME = "cylinder";
        private static readonly string SPHERE_ELEMENT_NAME = "sphere";
        private static readonly string MESH_ELEMENT_NAME = "mesh";


        protected override Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The name of the URDF XML element that this class parses.
        /// </summary>
        protected override string ElementName { get; } = "geometry";


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

            XmlElement boxElement = GetElementFromNode(node, BOX_ELEMENT_NAME);
            XmlElement cylinderElement = GetElementFromNode(node, CYLINDER_ELEMENT_NAME);
            XmlElement sphereElement = GetElementFromNode(node, SPHERE_ELEMENT_NAME);
            XmlElement meshElement = GetElementFromNode(node, MESH_ELEMENT_NAME);

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
                Logger.Warn("Parsing {0} element failed to due missing sub-element of valid type (box, cylinder, sphere, mesh)", ElementName);
                geometry = DEFAULT_GEOMETRY;
            }

            return geometry;
        }
    }
}
