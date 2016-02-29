using System;
using System.Xml;
using UrdfUnity.Urdf.Models.LinkElements.InertialElements;

namespace UrdfUnity.Parse.Xml.LinkElements.InertialElements
{
    /// <summary>
    /// Parses a URDF &lt;inertia&gt; element from XML into a Inertia object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="Urdf.Models.LinkElements.InertialElements.Inertia"/>
    class InertiaParser : XmlParser<Inertia>
    {
        /// <summary>
        /// Parses a URDF &lt;inertia&gt; element from XML.
        /// </summary>
        /// <param name="inertiaNode">The XML node of a &lt;inertia&gt; element</param>
        /// <returns>A Inertia object parsed from the XML</returns>
        public Inertia Parse(XmlNode inertiaNode)
        {
            // TODO: Implement...!
            throw new NotImplementedException();
        }
    }
}
