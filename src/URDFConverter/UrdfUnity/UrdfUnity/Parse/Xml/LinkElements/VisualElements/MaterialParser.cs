using System;
using System.Xml;
using UrdfUnity.Urdf.Models.LinkElements.VisualElements;

namespace UrdfUnity.Parse.Xml.LinkElements.VisualElements
{
    /// <summary>
    /// Parses a URDF &lt;material&gt; element from XML into a Material object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/visual"/>
    /// <seealso cref="Urdf.Models.LinkElements.VisualElements.Material"/>
    class MaterialParser : XmlParser<Material>
    {
        /// <summary>
        /// Parses a URDF &lt;material&gt; element from XML.
        /// </summary>
        /// <param name="materialNode">The XML node of a &lt;material&gt; element</param>
        /// <returns>A Material object parsed from the XML</returns>
        public Material Parse(XmlNode materialNode)
        {
            // TODO: Implement...!
            throw new NotImplementedException();
        }
    }
}
