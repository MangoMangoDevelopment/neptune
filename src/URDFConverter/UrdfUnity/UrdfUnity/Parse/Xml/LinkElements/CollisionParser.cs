using System;
using System.Xml;
using UrdfUnity.Urdf.Models.LinkElements;

namespace UrdfUnity.Parse.Xml.LinkElements
{
    /// <summary>
    /// Parses a URDF &lt;collision&gt; element from XML into a Collision object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/link"/>
    /// <seealso cref="Urdf.Models.LinkElements.Collision"/>
    class CollisionParser : XmlParser<Collision>
    {
        /// <summary>
        /// Parses a URDF &lt;collision&gt; element from XML.
        /// </summary>
        /// <param name="collisionNode">The XML node of a &lt;collision&gt; element</param>
        /// <returns>A Collision object parsed from the XML</returns>
        public Collision Parse(XmlNode collisionNode)
        {
            // TODO: Implement...!
            throw new NotImplementedException();
        }
    }
}
