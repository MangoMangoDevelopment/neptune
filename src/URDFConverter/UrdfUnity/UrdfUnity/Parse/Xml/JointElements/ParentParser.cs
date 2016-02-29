using System;
using System.Xml;

namespace UrdfUnity.Parse.Xml.JointElements
{
    /// <summary>
    /// Parses a URDF &lt;parent&gt; element from XML into a string object.
    /// </summary>
    /// <seealso cref="http://wiki.ros.org/urdf/XML/joint"/>
    /// <seealso cref="Urdf.Models.Joint"/>
    class ParentParser : XmlParser<string>
    {
        /// <summary>
        /// Parses a URDF &lt;parent&gt; element from XML.
        /// </summary>
        /// <param name="parentNode">The XML node of a &lt;parent&gt; element</param>
        /// <returns>A string object with the name of the parent link parsed from the XML</returns>
        public string Parse(XmlNode parentNode)
        {
            // TODO: Implement...!
            throw new NotImplementedException();
        }
    }
}
