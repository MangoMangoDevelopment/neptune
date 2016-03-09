using System;
using System.Xml;
using UrdfUnity.Urdf.Models.LinkElements;

namespace UrdfUnity.Parse.Xml.LinkElements
{
    /// <summary>
    /// Parses a URDF &lt;geometry&gt; element from XML into a Geometry object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    /// <seealso cref="Urdf.Models.LinkElements.Geometry"/>
    class GeometryParser : XmlParser<Geometry>
    {
        private static readonly string BOX_ATTRIBUTE_NAME = "box";
        private static readonly string CYLINDER_ATTRIBUTE_NAME = "cylinder";
        private static readonly string SPHERE_ATTRIBUTE_NAME = "sphere";
        private static readonly string MESH_ATTRIBUTE_NAME = "mesh";
        private static readonly string MATERIAL_ATTRIBUTE_NAME = "material";


        /// <summary>
        /// Parses a URDF &lt;geometry&gt; element from XML.
        /// </summary>
        /// <param name="node">The XML node of a &lt;geometry&gt; element</param>
        /// <returns>A Geometry object parsed from the XML</returns>
        public Geometry Parse(XmlNode node)
        {
            // TODO: Implement...!
            throw new NotImplementedException();
        }
    }
}
