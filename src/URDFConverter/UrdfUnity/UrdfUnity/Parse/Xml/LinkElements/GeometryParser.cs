using System;
using System.Xml;
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
    public class GeometryParser : XmlParser<Geometry>
    {
        private static readonly string BOX_ELEMENT_NAME = "box";
        private static readonly string CYLINDER_ELEMENT_NAME = "cylinder";
        private static readonly string SPHERE_ELEMENT_NAME = "sphere";
        private static readonly string MESH_ELEMENT_NAME = "mesh";
        private static readonly Geometry DEFAULT_GEOMETRY = new Geometry(new Box(new SizeAttribute(1, 1, 1)));

        private readonly BoxParser boxParser = new BoxParser();
        private readonly CylinderParser cylinderParser = new CylinderParser();
        private readonly SphereParser sphereParser = new SphereParser();
        private readonly MeshParser meshParser = new MeshParser();


        /// <summary>
        /// Parses a URDF &lt;geometry&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;geometry&gt; element</param>
        /// <returns>A Geometry object parsed from the XML</returns>
        public Geometry Parse(XmlNode node)
        {
            Preconditions.IsNotNull(node);

            XmlElement boxElement = (XmlElement)node.SelectSingleNode(BOX_ELEMENT_NAME);
            XmlElement cylinderElement = (XmlElement)node.SelectSingleNode(CYLINDER_ELEMENT_NAME);
            XmlElement sphereElement = (XmlElement)node.SelectSingleNode(SPHERE_ELEMENT_NAME);
            XmlElement meshElement = (XmlElement)node.SelectSingleNode(MESH_ELEMENT_NAME);

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
                // TODO: Log malformed <geometry> element encountered
                geometry = DEFAULT_GEOMETRY;
            }

            return geometry;
        }
    }
}
